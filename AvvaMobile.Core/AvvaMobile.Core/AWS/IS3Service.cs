using AvvaMobile.Core.Business;
using Microsoft.AspNetCore.Http;

namespace AvvaMobile.Core.AWS
{
    public interface IS3Service
    {
        Task<S3UploadResult> Upload(IFormFile file, string bucketName);
    }
}