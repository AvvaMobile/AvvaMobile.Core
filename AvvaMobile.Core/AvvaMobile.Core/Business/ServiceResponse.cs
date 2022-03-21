namespace AvvaMobile.Core.Business
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = null;
        public dynamic? Data { get; set; } = null;
    }
}