using AvvaMobile.Core.Business;
using Microsoft.AspNetCore.Http;

namespace Quark.Business.Common.AWS
{
    public interface IS3Service
    {
        Task<S3UploadResult> Upload(IFormFile file, string bucketName);
    }
}