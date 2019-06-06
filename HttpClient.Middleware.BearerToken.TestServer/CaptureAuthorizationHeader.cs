using Microsoft.Extensions.Primitives;

namespace HttpClient.Middleware.BearerToken.TestServer
{
    public class CaptureAuthorizationHeader : ICaptureAuthorizationHeader
    {
        public StringValues LastValue { get; private set; }

        public void AuthorizationHeader(StringValues value) => LastValue = value;
    }
}