namespace AvvaMobile.Core.SMSSender
{
    public interface ISMSSender
    {
        public Task<SMSSentResult> SendAsync(string receiverPhoneNumber, string message);
    }

    public class SMSSentResult
    {
        public bool IsSuccess { get; set; } = true;
        public Exception Exception { get; set; }
    }
}