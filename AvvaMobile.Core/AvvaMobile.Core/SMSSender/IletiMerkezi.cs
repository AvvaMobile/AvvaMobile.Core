using AvvaMobile.Core.Caching;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Web;

namespace AvvaMobile.Core.SMSSender
{
    public class IletiMerkezi : ISMSSender
    {
        private readonly AppSettingsKeys _appSettingsKeys;

        public IletiMerkezi(AppSettingsKeys appSettingsKeys)
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

                var url = $"https://api.iletimerkezi.com/v1/send-sms/get/?username={username}&password={password}&text={message}&receipents={receiver}&sender={sender}&iys=0";

                NetworkManager client = new NetworkManager();
                var httpResponse = await client.GetXMLStringAsync(url, null);
                result.IsSuccess = httpResponse.IsSuccess;
                result.Exception = new Exception($"{httpResponse.Exception.Message} | {httpResponse.Message}", result.Exception);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsSuccess = false;
            }

            return result;
        }
    }

    public class SMSProviderHTTPResponse
    {

    }
}