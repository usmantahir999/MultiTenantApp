using System.Net;

namespace Application.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public List<string> Errors { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public UnAuthorizedException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
        }
    }
}
