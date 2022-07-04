using Newtonsoft.Json;
using System.Text;

namespace AvvaMobile.Core
{
    public class HttpResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = null;
        public dynamic Data { get; set; } = null;

    }

    public interface INetworkManager
    {
        public void ClearHeaders();
        public void AddHeader(string name, string value);
        public Task<HttpResponse> GetAsync(string uri);
        public Task<HttpResponse> GetAsync(string uri, Dictionary<string, string> parameters);
        public Task<HttpResponse> PostAsync(string uri, dynamic data);
    }

    public class NetworkManager
    {
        public NetworkManager()
        {

        }

        public NetworkManager(string baseAddress)
        {
            client.BaseAddress = new Uri(baseAddress);
        }

        readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        /// <summary>
        /// Clears all existing header from the client.
        /// </summary>
        public void ClearHeaders()
        {
            client.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Adds new header value to the client request.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddHeader(string name, string value)
        {
            client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Sends a GET request and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponse> GetAsync(string uri)
        {
            return await GetAsync(uri, null);
        }

        /// <summary>
        /// Sends a GET request with url parameters and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<HttpResponse> GetAsync(string uri, Dictionary<string, string> parameters)
        {
            var response = new HttpResponse();

            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    uri += "?";

                    foreach (var param in parameters)
                    {
                        uri += param.Key + "=" + param.Value + "&";
                    }
                }

                response.Data = await client.GetStringAsync(client.BaseAddress + uri);
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.GetAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a POST request with data object. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpResponse> PostAsync(string uri, dynamic data)
        {
            var response = new HttpResponse();

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(client.BaseAddress + uri, content);
                response.Data = await resp.Content.ReadAsStringAsync();
                response.IsSuccess = resp.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.PostAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a POST request with form data. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponse> PostAsFormDataAsync(string uri, MultipartFormDataContent content)
        {
            var response = new HttpResponse();
            try
            {
                var resp = await client.PostAsync(client.BaseAddress + uri, content);
                response.Data = await resp.Content.ReadAsStringAsync();
                response.IsSuccess = resp.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.PostAsync Error: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Sends a DELETE request with data object. Also returns http response as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpResponse> DeleteAsync(string uri)
        {
            var response = new HttpResponse();

            try
            {
                var resp = await client.DeleteAsync(uri);
                response.Data = await resp.Content.ReadAsStringAsync();
                response.IsSuccess = resp.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                response.IsSuccess = false;
                response.Message = "HttpClient.DeleteAsync Error: " + ex.Message;
            }

            return response;
        }
    }
}
