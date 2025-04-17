namespace Domain.DTOs
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string ErrorCode { get; set; }
        public object Metadata { get; set; }

        public Result(T data, string message = null)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        public Result(string message, string errorCode = null, object metadata = null)
        {
            IsSuccess = false;
            Message = message;
            ErrorCode = errorCode;
            Metadata = metadata;
        }

        public static Result<T> Success(T data, string message = null)
        {
            return new Result<T>(data, message);
        }

        public static Result<T> Failure(string message, string errorCode = null, object metadata = null)
        {
            return new Result<T>(message, errorCode, metadata);
        }
    }
}
