using AvvaMobile.Core.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AvvaMobile.Core.Utilities.Mail
{
    public class MailManager : IMailService
    {

        private IHostingEnvironment _hostingEnvironment;
        private readonly AppSettingsKeys _appSettingsKeys;
        private readonly IHttpContextAccessor _httpContext;
        public MailManager(IHostingEnvironment hostingEnvironment, AppSettingsKeys appSettingsKeys, IHttpContextAccessor httpContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _appSettingsKeys = appSettingsKeys;
            _httpContext = httpContext;
        }
        public async Task<EmailResult> Send(string to, string subject, string filePath, Dictionary<string, string> parameterList = null, AttachmentCollection attachments = null)
        {
            return await Send(new List<string> { to }, subject, filePath, parameterList, attachments);
        }

        public async Task<EmailResult> Send(List<string> toList, string subject, string filePath = null, Dictionary<string, string> parameterList = null, AttachmentCollection attachments = null)
        {
            try
            {
                var userName = _appSettingsKeys.SMTP_Username;
                var password = _appSettingsKeys.SMTP_Password;
                var smtpServer = _appSettingsKeys.SMTP_Url;
                var smptPort = _appSettingsKeys.SMTP_Port;
                var smtpSender = _appSettingsKeys.SMTP_Sender;


                var mail = new MailMessage { Subject = subject };

                var smtp = new SmtpClient(smtpServer)
                {
                    Credentials = new NetworkCredential(userName, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Port = Convert.ToInt32(smptPort)
                };

                if (!string.IsNullOrEmpty(filePath))
                {
                    //var plainMailBody = await GetMailBody(@"Plain\" + filePath + ".txt");
                    var htmlMailBody = await GetMailBody(@"Html\" + filePath + ".html");
                    var context = _httpContext.HttpContext;
                    htmlMailBody = htmlMailBody.Replace("{baseUrl}", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}");
                    foreach (var item in parameterList)
                    {
                        //plainMailBody = plainMailBody.Replace(item.Key, item.Value);
                        htmlMailBody = htmlMailBody.Replace(item.Key, item.Value);
                    }

                    //var plainView = AlternateView.CreateAlternateViewFromString(plainMailBody, null, "text/plain");
                    var htmlView = AlternateView.CreateAlternateViewFromString(htmlMailBody, null, "text/html");

                    //mail.AlternateViews.Add(plainView);
                    mail.AlternateViews.Add(htmlView);
                    mail.IsBodyHtml = true;

                    if (mail.Attachments.Count > 0)
                    {
                        foreach (var attachment in mail.Attachments)
                        {
                            mail.Attachments.Add(attachment);
                        }
                    }
                }

                foreach (var item in toList.Where(item => !string.IsNullOrEmpty(item)))
                {
                    mail.To.Add(item);
                }

                mail.From = new MailAddress(smtpSender);
                mail.Priority = MailPriority.High;
                mail.BodyEncoding = Encoding.UTF8;

                smtp.SendAsync(mail, string.Empty);

                return new EmailResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new EmailResult { IsSuccess = false, Message = ex.Message };
            }
        }

        private async Task<string> GetMailBody(string fileName)
        {
            string body;
            var templatesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "EmailTemplates");
            var filePath = Path.Combine(templatesFolder, fileName);
            var fileStream = new FileStream(filePath, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                body = await reader.ReadToEndAsync();
            }

            return body;
        }
    }
    public class EmailResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}