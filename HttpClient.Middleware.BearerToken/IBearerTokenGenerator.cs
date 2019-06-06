using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClient.Middleware.BearerToken
{
    public interface IBearerTokenGenerator
    {
        Task<string> Generate(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}