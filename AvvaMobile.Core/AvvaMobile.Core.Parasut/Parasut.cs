using AvvaMobile.Core.Business;
using AvvaMobile.Core.Parasut.Models;
using AvvaMobile.Core.Parasut.Models.Common;
using AvvaMobile.Core.Parasut.Models.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AvvaMobile.Core.Parasut
{
    public class Parasut
    {
        private string AuthURL { get; } = "https://api.parasut.com";
        private string BaseURL { get { return $"https://api.parasut.com/v4/{CompanyID}"; } }

        public string CompanyID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }

        // Her oturumda bu bilgi saklanarak sürekli aynı token kullanılması sağlanır.
        private string access_token { get; set; }

        /// <summary>
        /// Paraşüt'te herhangi bir işlem yapmadan önce Token alınmasını sağlar.
        /// </summary>
        /// <returns>ServiceResult&lt;GetTokenResponse&gt; tipinde dönüş objesi döner.</returns>
        public async Task<ServiceResult<Token>> GetTokenAsync()
        {
            var serviceResult = new ServiceResult<Token>();

            var networkManager = new NetworkManager(AuthURL);
            networkManager.AddContentTypeJSONHeader();

            var getTokenRequest = new GetTokenRequest
            {
                grant_type = "password",
                username = Username,
                password = Password,
                client_id = ClientID,
                client_secret = ClientSecret
            };

            var httpResponse = await networkManager.PostAsync<Token>("/oauth/token", getTokenRequest);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            access_token = httpResponse.Data.access_token;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'te yeni müşteri yaratır.
        /// </summary>
        /// <param name="request">Customer tipinde müşteri bilgilerini alır.</param>
        /// <returns>Envelope&lt;Customer&gt; tipinde yaratılmış olan müşterinin tüm bilgilerini döner.</returns>
        public async Task<ServiceResult<Envelope<Customer>>> CustomerCreate(Customer request)
        {
            var serviceResult = new ServiceResult<Envelope<Customer>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<Customer>();
            envelope.data = request;

            var httpResponse = await networkManager.PostAsync<Envelope<Customer>>("/contacts", envelope);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'teki müşterileri, filtreleme kriterlerine göre listeler.
        /// </summary>
        /// <param name="request">CustomerListRequest tipinde filtreleme özelliklerini alır.</param>
        /// <returns>Envelope&lt;List&lt;Customer&gt;&gt; tipinde filtrelenmiş müşterileri döner.</returns>
        public async Task<ServiceResult<Envelope<List<Customer>>>> CustomerList(CustomerListRequest request)
        {
            var serviceResult = new ServiceResult<Envelope<List<Customer>>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var parameters = new Dictionary<string, string>();
            parameters.Add("page[size]", "25");
            parameters.Add("page[number]", request.page_number.ToString());
            if (!string.IsNullOrEmpty(request.name))
            {
                parameters.Add("filter[name]", (request.name));
            }
            if (!string.IsNullOrEmpty(request.email))
            {
                parameters.Add("filter[email]", (request.email));
            }
            if (!string.IsNullOrEmpty(request.tax_office))
            {
                parameters.Add("filter[tax_office]", (request.tax_office));
            }
            if (!string.IsNullOrEmpty(request.tax_number))
            {
                parameters.Add("filter[tax_number]", (request.tax_number));
            }

            var httpResponse = await networkManager.GetAsync<Envelope<List<Customer>>>("/contacts", parameters);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }
    }
}