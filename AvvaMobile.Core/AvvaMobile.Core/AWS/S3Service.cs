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

        public async Task<S3UploadResult> Upload(IFormFile file, string bucketName)
        {

            var result = new S3UploadResult();
            try
            {
                using (var client = new AmazonS3Client(_appSettingsKeys.AwsAccessKeyID, _appSettingsKeys.AwsSecretAccessKey, RegionEndpoint.EUCentral1))
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        file.CopyTo(newMemoryStream);
                        var ext = Path.GetExtension(file.FileName);
                        var fileName = Guid.NewGuid() + ext.ToLower();                      

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = fileName,
                            BucketName = bucketName,
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                        
                        result.IsSuccess = true;
                        result.FileName = fileName;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messsage = ex.Message;
                return result;
            }
           
        }
    }
    public class S3UploadResult
    {
        public bool IsSuccess { get; set; } = true;
        public string FileName { get; set; }
        public string Messsage { get; set; }
    }
}