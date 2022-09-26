using AvvaMobile.Core.Business;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace AvvaMobile.Core
{
    public class HttpResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }

    public class HttpResponse<T> : HttpResponse
    {
        public T Data { get; set; }
    }

    public interface INetworkManager
    {
        public void SetBaseAddress();
        public void ClearHeaders();
        public void AddHeader(string name, string value);
        public void AddContentTypeJSONHeader();
        public Task<HttpResponse> GetAsync(string uri);
        public Task<HttpResponse> GetAsync(string uri, Dictionary<string, string> parameters);
        public Task<HttpResponse> PostAsync(string uri, dynamic data);
    }

    public class NetworkManager
    {
        readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public NetworkManager()
        {

        }

        public NetworkManager(string baseAddress)
        {
            SetBaseAddress(baseAddress);
        }

        /// <summary>
        /// Updates the base address of http client.
        /// </summary>
        /// <param name="baseAddress"></param>
        public void SetBaseAddress(string baseAddress)
        {
            client.BaseAddress = new Uri(baseAddress);
        }

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
        /// Adds "ContentType:application/json" header to current request.
        /// </summary>
        public void AddContentTypeJSONHeader()
        {
            AddHeader("ContentType", "application/json");
        }

        /// <summary>
        /// Sends a GET request and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> GetAsync<T>(string uri)
        {
            return await GetAsync<T>(uri, null);
        }

        /// <summary>
        /// Sends a GET request with url parameters and returns data as String value.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> GetAsync<T>(string uri, Dictionary<string, string> parameters)
        {
            var response = new HttpResponse<T>();

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

                var resp = await client.GetAsync(client.BaseAddress + uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
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
        public async Task<HttpResponse<T>> PostAsync<T>(string uri, dynamic data)
        {
            var response = new HttpResponse<T>();

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(client.BaseAddress + uri, content);
                response.IsSuccess = resp.IsSuccessStatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }                
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
        public async Task<HttpResponse<T>> PostAsFormDataAsync<T>(string uri, MultipartFormDataContent content)
        {
            var response = new HttpResponse<T>();
            try
            {
                var resp = await client.PostAsync(client.BaseAddress + uri, content);
                response.IsSuccess = resp.IsSuccessStatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
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
        public async Task<HttpResponse<T>> DeleteAsync<T>(string uri)
        {
            var response = new HttpResponse<T>();

            try
            {
                var resp = await client.DeleteAsync(uri);
                response.IsSuccess = resp.IsSuccessStatusCode;
                var responseString = await resp.Content.ReadAsStringAsync();
                if (response.IsSuccess)
                {
                    response.Data = JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    response.Message = responseString;
                }
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