using System.Net;

namespace FT_ProviderSys.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public BadRequestException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

    }
}
