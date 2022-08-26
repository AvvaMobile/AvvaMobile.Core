using AvvaMobile.Core.Caching;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Web;

namespace AvvaMobile.Core.SMSSender
{
    public class IletiMerkezi : ISMSSender
    {
        private readonly IAppSettingsKeys _appSettingsKeys;

        public IletiMerkezi(IAppSettingsKeys appSettingsKeys)
        {
            _appSettingsKeys = appSettingsKeys;
        }

        public async Task<SMSSentResult> SendAsync(string receiver, string message)
        {
            var result = new SMSSentResult();

            try
            {
                var username = _appSettingsKeys.SMS_Username;
                var password = _appSettingsKeys.SMS_Password;
                var sender = _appSettingsKeys.SMS_Sender;

                message = HttpUtility.UrlEncode(message);

                var url = $"https://api.iletimerkezi.com/v1/send-sms/get/?username={username}&password={password}&text={message}&receipents={receiver}&sender={sender}";

                NetworkManager client = new NetworkManager();
                var response = await client.GetAsync(url);
                response.IsSuccess = result.IsSuccess;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccess = false;
            }

            return result;
        }
    }
}