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
        public string CompanyID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }

        private string AuthURL { get; } = "https://api.parasut.com";
        private string BaseURL { get { return $"https://api.parasut.com/v4/{CompanyID}"; } }
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
        public async Task<ServiceResult<Envelope<Contact>>> CustomerCreate(Contact request)
        {
            var serviceResult = new ServiceResult<Envelope<Contact>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<Contact>();
            envelope.data = request;

            var httpResponse = await networkManager.PostAsync<Envelope<Contact>>("/contacts", envelope);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'te müşteriyi yeni bilgiler iler günceller.
        /// </summary>
        /// <param name="request">Customer tipinde müşteri bilgilerini alır.</param>
        /// <returns>Envelope&lt;Customer&gt; tipinde yaratılmış olan müşterinin tüm bilgilerini döner.</returns>
        public async Task<ServiceResult<Envelope<Contact>>> CustomerEdit(Contact request)
        {
            var serviceResult = new ServiceResult<Envelope<Contact>>();

            if (string.IsNullOrEmpty(request.id))
            {
                serviceResult.SetError("Müşteri için id alanı boş olamaz.");
                return serviceResult;
            }

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<Contact>();
            envelope.data = request;

            var httpResponse = await networkManager.PutAsync<Envelope<Contact>>("/contacts/" + request.id, envelope);

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
        public async Task<ServiceResult<Envelope<List<Contact>>>> CustomerList(CustomerListRequest request)
        {
            var serviceResult = new ServiceResult<Envelope<List<Contact>>>();

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

            var httpResponse = await networkManager.GetAsync<Envelope<List<Contact>>>("/contacts", parameters);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'te yeni ürün yaratır.
        /// </summary>
        /// <param name="request">Product tipinde ürün bilgilerini alır.</param>
        /// <returns>Envelope&lt;Product&gt; tipinde yaratılmış olan ürünğn tüm bilgilerini döner.</returns>
        public async Task<ServiceResult<Envelope<Product>>> ProductCreate(Product request)
        {
            var serviceResult = new ServiceResult<Envelope<Product>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<Product>();
            envelope.data = request;

            var httpResponse = await networkManager.PostAsync<Envelope<Product>>("/products", envelope);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'teki ürünü günceller.
        /// </summary>
        /// <param name="request">Product tipinde ürün bilgilerini alır.</param>
        /// <returns>Envelope&lt;Product&gt; tipinde yaratılmış olan ürünğn tüm bilgilerini döner.</returns>
        public async Task<ServiceResult<Envelope<Product>>> ProductEdit(Product request)
        {
            var serviceResult = new ServiceResult<Envelope<Product>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<Product>();
            envelope.data = request;

            var httpResponse = await networkManager.PutAsync<Envelope<Product>>("/products/" + request.id, envelope);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }

        /// <summary>
        /// Paraşüt'te yeni fatura yaratır.
        /// </summary>
        /// <param name="request">SalesInvoice tipinde ürün bilgilerini alır.</param>
        /// <returns>Envelope&lt;SalesInvoice&gt; tipinde yaratılmış olan ürünğn tüm bilgilerini döner.</returns>
        public async Task<ServiceResult<Envelope<SalesInvoice>>> SalesInvoiceCreate(SalesInvoice request)
        {
            var serviceResult = new ServiceResult<Envelope<SalesInvoice>>();

            await GetTokenAsync();

            var networkManager = new NetworkManager(BaseURL);
            networkManager.AddContentTypeJSONHeader();
            networkManager.AddBearerToken(access_token);

            var envelope = new Envelope<SalesInvoice>();
            envelope.data = request;

            var httpResponse = await networkManager.PostAsync<Envelope<SalesInvoice>>("/sales_invoices", envelope);

            serviceResult.IsSuccess = httpResponse.IsSuccess;
            serviceResult.Message = httpResponse.Message;
            serviceResult.Data = httpResponse.Data;

            return serviceResult;
        }
    }
}