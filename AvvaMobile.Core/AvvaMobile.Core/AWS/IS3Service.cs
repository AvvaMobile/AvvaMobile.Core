using AvvaMobile.Core.Business;
using Microsoft.AspNetCore.Http;

namespace Quark.Business.Common.AWS
{
    public interface IS3Service
    {
        Task<ServiceResult<string>> Upload(IFormFile file, string bucketName, string folder);
    }
}