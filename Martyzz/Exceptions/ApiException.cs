namespace Martyzz.Exceptions
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiException(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultErrorMessage(statusCode);
        }

        private string GetDefaultErrorMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => string.Empty,
            };
        }
    }
}
