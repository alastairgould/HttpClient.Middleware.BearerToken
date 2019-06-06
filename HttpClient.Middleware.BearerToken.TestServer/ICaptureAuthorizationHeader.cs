using Microsoft.Extensions.Primitives;

namespace HttpClient.Middleware.BearerToken.TestServer
{
    public interface ICaptureAuthorizationHeader
    {
        void AuthorizationHeader(StringValues header);
    }
}