using System.Net.Mail;

namespace AvvaMobile.Core.Utilities.Mail
{
    public interface IMailService
    {
        Task<EmailResult> Send(string to, string subject, string filePath, Dictionary<string, string> parameterList = null, AttachmentCollection attachments = null, string sender = null);
        Task<EmailResult> Send(List<string> toList, string subject, string filePath, Dictionary<string, string> parameterList = null, AttachmentCollection attachments = null, string sender = null);
    }
}