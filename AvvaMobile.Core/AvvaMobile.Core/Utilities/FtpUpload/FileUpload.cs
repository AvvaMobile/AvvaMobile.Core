using Microsoft.AspNetCore.Http;

namespace AvvaMobile.Core.Utilities.FtpUpload
{
    public class CDNFileUploadManager
    {
        private readonly IFTPClient _ftpClient;
        public CDNFileUploadManager(IFTPClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        public FileUploadResult Upload(IFormFile file, string folder)
        {
            var result = new FileUploadResult();

            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var newFileName = CodeGenerator.GenerateFileName(extension);
                var uploadFolder = Path.Combine(FTPClient.WebRootPath, "Temp");
                var filePath = Path.Combine(uploadFolder, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _ftpClient.Upload(new FileInfo(filePath), folder);

                result.IsSuccess = true;
                result.FileName = newFileName;
                return result;
            }

            result.IsSuccess = false;
            return result;
        }

        public FileUploadResult UploadLocal(IFormFile file, string folder, bool generateNewName, string newFileName)
        {
            var result = new FileUploadResult();

            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);

                string fileName = file.FileName;
                if (generateNewName)
                {
                    fileName = CodeGenerator.GenerateFileName(extension);
                }
                else if (!string.IsNullOrWhiteSpace(newFileName))
                {
                    fileName = Path.Combine(newFileName, extension);
                }
                var uploadFolder = Path.Combine(FTPClient.WebRootPath, folder);
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                result.IsSuccess = true;
                result.FileName = fileName;
                return result;
            }

            result.IsSuccess = false;
            return result;
        }

        public FileUploadResult RemoveFile(string fileName, string folder)
        {
            var result = new FileUploadResult();
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(folder))
            {
                result.IsSuccess = false;
                return result;
            }
            var uploadFolder = Path.Combine(FTPClient.WebRootPath, "Temp");
            var filePath = Path.Combine(uploadFolder, fileName);

            File.Delete(filePath);

            _ftpClient.Remove(fileName, folder);


            result.IsSuccess = true;
            return result;
        }
        public class FileUploadResult
        {
            public string FileName { get; set; }
            public bool IsSuccess { get; set; }
            public Exception Exception { get; set; }
        }
    }
    public static class CodeGenerator
    {
        public static string GenerateFileName(string extension)
        {
            var name = Guid.NewGuid() + extension;
            return name;
        }
    }
}