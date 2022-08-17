using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AvvaMobile.Core.Utilities.FtpUpload
{
    public class FileUpload
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFTPClient _ftpClient;

        public FileUpload(IWebHostEnvironment hostingEnvironment, IFTPClient ftpClient)
        {
            _hostingEnvironment = hostingEnvironment;
            _ftpClient = ftpClient;
        }

        public FileUploadResult Upload(IFormFile file, string folder)
        {
            var result = new FileUploadResult();

            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var newFileName = GenerateFileName(extension);
                var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Temp");
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
                    fileName = GenerateFileName(extension);
                }
                else if (!string.IsNullOrWhiteSpace(newFileName))
                {
                    fileName = Path.Combine(newFileName, extension);
                }
                var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
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
        public string GenerateFileName(string extension)
        {
            var name = Guid.NewGuid() + extension;
            return name;
        }

        public class FileUploadResult
        {
            public string FileName { get; set; }
            public bool IsSuccess { get; set; }
            public Exception Exception { get; set; }
        }
    }
}