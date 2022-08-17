using AvvaMobile.Core.Caching;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace AvvaMobile.Core.Utilities.FtpUpload
{
    public class FTPClient : IFTPClient
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppSettingsKeys _appSettingsKeys;
        public FTPClient(IWebHostEnvironment hostingEnvironment, AppSettingsKeys appSettingsKeys)
        {
            _hostingEnvironment = hostingEnvironment;
            _appSettingsKeys = appSettingsKeys;
        }


        // TODO: burası yeniden yapılandırılacak .net 6 da WebClient yok
        public void Upload(FileInfo fileInfo, string uploadFolder)
        {
            string CDNFtpHost = _appSettingsKeys.CDN_FTP_Url;
            string CDNFtpUsername = _appSettingsKeys.CDN_FTP_Username;
            string CDNFtpPassword = _appSettingsKeys.CDN_FTP_Password;

            using (var client = new WebClient())
            {
                client.Proxy = null;
                client.Credentials = new NetworkCredential(CDNFtpUsername, CDNFtpPassword);
                client.UploadFile(CDNFtpHost + "/" + uploadFolder + "/" + fileInfo.Name, "STOR", fileInfo.FullName);
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
    }

    public interface IFTPClient
    {
        void Upload(FileInfo fileInfo, string uploadFolder);
        void UploadSavedFile(string fileFullUrl, string cdnUploadFolder);
        string UploadFromExternalUrl(string fileUrl, string uploadFolder);
        byte[] DownloadExternalFile(string fileUrl);
    }
}