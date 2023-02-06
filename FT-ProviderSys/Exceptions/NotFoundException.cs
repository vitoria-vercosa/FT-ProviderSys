using System.Net;

namespace FT_ProviderSys.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public NotFoundException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.NotFound;
        }

    }
}
