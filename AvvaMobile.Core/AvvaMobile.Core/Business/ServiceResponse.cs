namespace AvvaMobile.Core.Business
{
    [Obsolete("This method is deprecated. Please use 'ServiceResult' class. (Öcal Esmer)", true)]
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = null;
        public dynamic Data { get; set; } = null;
    }
}