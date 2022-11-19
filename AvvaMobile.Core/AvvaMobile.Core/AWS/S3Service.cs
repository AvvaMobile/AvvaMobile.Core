using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using AvvaMobile.Core.Business;
using AvvaMobile.Core.Caching;
using Microsoft.AspNetCore.Http;

namespace Quark.Business.Common.AWS
{
    public class S3Service : IS3Service
    {
        private readonly AppSettingsKeys _appSettingsKeys;

        public S3Service(AppSettingsKeys appSettingsKeys)
        {
            _appSettingsKeys = appSettingsKeys;
        }

        public async Task<ServiceResult<string>> Upload(IFormFile file, string bucketName)
        {
            return await Upload(file, bucketName, null);
        }
        public async Task<ServiceResult<string>> Upload(IFormFile file, string bucketName, string folder)
        {
            var result = new ServiceResult<string>();

            using (var client = new AmazonS3Client(_appSettingsKeys.AwsAccessKeyID, _appSettingsKeys.AwsSecretAccessKey, RegionEndpoint.EUCentral1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);
                    var ext = Path.GetExtension(file.FileName);
                    var extension = ext.ToLower();
                    var fileName = Guid.NewGuid() + extension;
                    var fullName = fileName;

                    if (folder is not null)
                    {
                        fullName = folder + fileName;
                    }

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = fullName,
                        BucketName = bucketName,
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                    result.Data = file.FileName;
                    return result;
                }
            }
        }
    }
}