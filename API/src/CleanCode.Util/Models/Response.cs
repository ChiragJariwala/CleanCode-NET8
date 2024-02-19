using CleanCode.Util.Middleware;

namespace CleanCode.Util.Models
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            ItemsCount = 0;
        }

        public Response(T data, bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = null;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<ApiError> Errors { get; set; }
        public int ItemsCount { get; set; }
    }
}
