using System.Net;

namespace Application.Exceptions
{
    public class IdentityException : Exception
    {
        public List<string> Errors { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public IdentityException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
        }
    }
}
