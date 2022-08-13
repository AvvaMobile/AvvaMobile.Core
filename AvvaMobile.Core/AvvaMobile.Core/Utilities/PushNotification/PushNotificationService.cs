using AvvaMobile.Core.Caching;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Doctic.Core.PushNotification
{
    public class PushNotificationService : BasePushNotificationService, IPushNotificationService
    {
        private readonly AppSettingsKeys _appSettingsKeys;

        public PushNotificationService(AppSettingsKeys appSettingsKeys)
        {
            _appSettingsKeys = appSettingsKeys;
        }

        public async Task<PushNotificationSendResult> SendAsync(string userPushID, string message)
        {
            return  await SendAsync(new List<string> { userPushID }, null, message);
        }
        public async Task<PushNotificationSendResult> SendAsync(List<string> userPushIDs, string message)
        {
            return  await SendAsync(userPushIDs, null, message);
        }

        public async Task<PushNotificationSendResult> SendAsync(string userPushID, string title, string message)
        {
            return await SendAsync(new List<string> { userPushID }, title, message);
        }

        public async Task<PushNotificationSendResult> SendAsync(List<string> userPushIDs, string title, string message)
        {
            string appID = _appSettingsKeys.OneSignal_AppID;
            string apiKey = _appSettingsKeys.OneSignal_APIKey;
            var pusNotificationResult =  await SendNotificationAsync(appID, apiKey, userPushIDs, title, message);
            return pusNotificationResult;
        }
    }

    public class BasePushNotificationService
    {
        public async Task<PushNotificationSendResult> SendNotificationAsync(string appID, string apiKey, List<string> userPushIDs, string title, string message)
        {
            var result = new PushNotificationSendResult();

            if (userPushIDs.Count.Equals(0))
            {
                result.IsSuccess = false;
                result.Message = "Gönderilecek kullanıcı bulunamadı.";
                return result;
            }

            string[] membersString = userPushIDs.ToArray();

            var model = new
            {
                app_id = appID,
                include_player_ids = membersString,
                contents = new { en = message },
                headings = new { en = title }
            };

            try
            {

                HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serviceUrl = "https://onesignal.com/api/v1/notifications";
                var requestBody = JsonConvert.SerializeObject(model);

                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(serviceUrl);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("authorization", "Basic " + apiKey);

                var response = await client.SendAsync(request);

                var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                result.ResponseContents = responseBody;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Bildirim gönderilirken bir hata oluştu. Lütfen hata detayına bakınız. Exception: " + ex.Message;
                result.Exception = ex;
                return result;
            }
        }
    }

    public class PushNotificationSendResult
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public string RequestContents { get; set; }
        public string ResponseContents { get; set; }
    }

    public interface IPushNotificationService
    {
        Task<PushNotificationSendResult> SendAsync(List<string> playerIDs, string message);
        Task<PushNotificationSendResult> SendAsync(List<string> playerIDs, string title, string message);
        Task<PushNotificationSendResult> SendAsync(string playerID, string message);
        Task<PushNotificationSendResult> SendAsync(string playerID, string title, string message);
    }
}



