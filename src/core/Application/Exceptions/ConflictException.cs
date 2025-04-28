using System.Net;

namespace Application.Exceptions
{
    public class ConflictException : Exception
    {
        public List<string> Errors { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public ConflictException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.Conflict)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
        }
    }
}
