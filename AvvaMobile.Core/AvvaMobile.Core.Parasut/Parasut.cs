using AvvaMobile.Core.Business;
using AvvaMobile.Core.Parasut.Models;
using AvvaMobile.Core.Parasut.Models.Requests;
using AvvaMobile.Core.Parasut.Models.Responses;
using Newtonsoft.Json;

namespace AvvaMobile.Core.Parasut
{
    public class Parasut
    {
        private NetworkManager _networkManager;

        private string AuthURL { get; } = "https://api.parasut.com";
        private string BaseURL { get { return $"https://api.parasut.com/v4/{CompanyID}"; } }

        public string CompanyID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }

        public Parasut()
        {
            _networkManager = new NetworkManager(BaseURL);
        }

        /// <summary>
        /// Paraşüt'te herhangi bir işlem yapmadan önce Token alınmasını sağlar.
        /// </summary>
        /// <returns>ServiceResult<GetTokenResponse> tipinde dönüş objesi iletilir.</returns>
        public async Task<ServiceResult<GetTokenResponse>> GetTokenAsync()
        {
            // Bu service tarafından dönülecek olan obje yaratılır.
            var serviceResult = new ServiceResult<GetTokenResponse>();

            _networkManager.SetBaseAddress(AuthURL);

            // Paraşüt API bağlanmadan önce tüm headerlar silinerek temizlenir ve gerekli default headerlar eklenir.
            _networkManager.ClearHeaders();
            //_networkManager.AddContentTypeJSONHeader();

            // Paraşüt API'den Token alınması için gerekli olaran request body objesi hazırlanır.
            var getTokenRequest = new GetTokenRequest
            {
                grant_type = "password",
                username = Username,
                password = Password,
                client_id = ClientID,
                client_secret = ClientSecret
            };

            // Paraşüt API Token almak için request gönderilir.
            var httpResponse = await _networkManager.PostAsync<GetTokenResponse>("/oauth/token", getTokenRequest);

            // API'den dönen sonuçlar dönüp tipine göre ayarlanır.
            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }
    }
}