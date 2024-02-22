namespace AvvaMobile.Core.Business
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public ServiceResultType Type { get; set; } = ServiceResultType.Success;

        public void SetError(string message)
        {
            IsSuccess = false;
            Type = ServiceResultType.Error;
            Message = message;
        }
        public void SetSuccess(string message = "")
        {
            IsSuccess = true;
            Type = ServiceResultType.Success;
            Message = message;
        }
        
        public void SetWarning(string message)
        {
            IsSuccess = true;
            Type = ServiceResultType.Warning;
            Message = message;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
    
    public enum ServiceResultType
    {
        Success,
        Error,
        Warning
    }
}