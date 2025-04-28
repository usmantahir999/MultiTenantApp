using System.Net;

namespace Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public List<string> Errors { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public ForbiddenException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.Forbidden)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
        }
    }
}
