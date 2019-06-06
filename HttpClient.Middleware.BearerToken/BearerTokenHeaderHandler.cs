using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClient.Middleware.BearerToken
{
    public class BearerTokenHeaderHandler : DelegatingHandler
    {
        private const string Scheme = "Bearer";

        private readonly IBearerTokenGenerator _generateToken;

        public BearerTokenHeaderHandler(IBearerTokenGenerator generateToken)
        {
            _generateToken = generateToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var bearerToken = await _generateToken.Generate(request, cancellationToken);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Scheme, bearerToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
