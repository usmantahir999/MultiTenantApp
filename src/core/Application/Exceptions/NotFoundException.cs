using System.Net;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public List<string> Errors { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public NotFoundException(List<string> errorMessages = default!, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            StatusCode = statusCode;
            Errors = errorMessages;
        }
    }
}
