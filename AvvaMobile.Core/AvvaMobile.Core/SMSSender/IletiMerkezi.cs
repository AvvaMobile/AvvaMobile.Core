using Microsoft.Extensions.Configuration;
using System.Net;
using System.Web;

namespace AvvaMobile.Core.SMSSender
{
    public class IletiMerkezi : ISMSSender
    {
        private readonly IConfiguration _configuration;
        public IletiMerkezi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SMSSentResult> SendAsync(string receiver, string message)
        {
            var result = new SMSSentResult();

            try
            {
                var username = _configuration["SMSSender:Username"];
                var password = _configuration["SMSSender:Password"];
                var sender = _configuration["SMSSender:Sender"];
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