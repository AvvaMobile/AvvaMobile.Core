namespace AvvaMobile.Core.Business
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }

        public void SetError(string message)
        {
            IsSuccess = false;
            Message = message;
        }
        public void SetSuccess(string message)
        {
            IsSuccess = true;
            Message = message;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
}