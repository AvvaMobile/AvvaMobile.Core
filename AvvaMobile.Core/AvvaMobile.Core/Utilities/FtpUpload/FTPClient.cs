using AvvaMobile.Core.Caching;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net;

namespace AvvaMobile.Core.Utilities.FtpUpload
{
    public class FTPClient : IFTPClient
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AppSettingsKeys _appSettingsKeys;
        public static string WebRootPath = string.Empty;
        public FTPClient(IHostingEnvironment hostingEnvironment, AppSettingsKeys appSettingsKeys)
        {
            _hostingEnvironment = hostingEnvironment;
            _appSettingsKeys = appSettingsKeys;
        }


        // TODO: burası yeniden yapılandırılacak .net 6 da bu yok
        public void Upload(FileInfo fileInfo, string uploadFolder)
        {
            var CDNFtpHost = _appSettingsKeys.CDN_FTP_Url;
            var CDNFtpUsername = _appSettingsKeys.CDN_FTP_Username;
            var CDNFtpPassword = _appSettingsKeys.CDN_FTP_Password;
            using (var client = new WebClient())
            {
                client.Proxy = null;
                client.Credentials = new NetworkCredential(CDNFtpUsername, CDNFtpPassword);
                client.UploadFile(CDNFtpHost + uploadFolder + fileInfo.Name, "STOR", fileInfo.FullName);
            }
        }

        public string UploadFromExternalUrl(string fileUrl, string cdnUploadFolder)
        {
            try
            {
                var uri = new Uri(fileUrl);
                fileUrl = $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Authority}{uri.AbsolutePath}";
                var ext = fileUrl[fileUrl.LastIndexOf('.')..];
                var extension = ext.ToLower();
                var fileName = Guid.NewGuid() + extension;

                var fileBytes = DownloadExternalFile(fileUrl);
                var fileFullPath = SaveFile(fileName, fileBytes, cdnUploadFolder);
                //Upload(new FileInfo(fileFullPath), cdnUploadFolder);
                return fileName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void UploadSavedFile(string fileFullUrl, string cdnUploadFolder)
        {
            var fileInfo = new FileInfo(fileFullUrl);
            if (!string.IsNullOrEmpty(cdnUploadFolder))
            {
                Upload(fileInfo, cdnUploadFolder);
            }
            fileInfo.Delete();
        }

        private string SaveFile(string fileName, byte[] fileBytes, string cdnUploadFolder)
        {
            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Temp");
            var fileFullPath = uploadFolder + "\\" + fileName;
            var fs = new FileStream(fileFullPath, FileMode.Create);
            var bw = new BinaryWriter(fs);
            try
            {
                bw.Write(fileBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }

            return fileFullPath;
        }

        public byte[] DownloadExternalFile(string fileUrl)
        {
            byte[] imageBytes;
            var imageRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
            var imageResponse = imageRequest.GetResponse();

            var responseStream = imageResponse.GetResponseStream();

            using (var br = new BinaryReader(responseStream))
            {
                imageBytes = ReadAllBytes(br);
                br.Close();
            }

            responseStream.Close();
            imageResponse.Close();
            return imageBytes;
        }

        public static byte[] ReadAllBytes(BinaryReader reader)
        {
            const int bufferSize = 4096;
            using var ms = new MemoryStream();
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
            {
                ms.Write(buffer, 0, count);
            }
            return ms.ToArray();
        }
        public void Remove(string fileName, string folder)
        {
            var CDNFtpHost = _appSettingsKeys.CDN_FTP_Url;
            var CDNFtpUsername = _appSettingsKeys.CDN_FTP_Username;
            var CDNFtpPassword = _appSettingsKeys.CDN_FTP_Password;

            var removedFilePath = "ftp://" + CDNFtpHost + folder + fileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(removedFilePath);
            request.Credentials = new NetworkCredential(CDNFtpUsername, CDNFtpPassword);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //Console.WriteLine("Delete status: {0}", response.StatusDescription);
            //response.Close();
        }
    }

    public interface IFTPClient
    {
        void Upload(FileInfo fileInfo, string uploadFolder);
        void UploadSavedFile(string fileFullUrl, string cdnUploadFolder);
        string UploadFromExternalUrl(string fileUrl, string uploadFolder);
        byte[] DownloadExternalFile(string fileUrl);
        void Remove(string fileName, string folder);
    }
}