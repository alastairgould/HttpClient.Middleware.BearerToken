using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using HttpClient.Middleware.BearerToken.TestServer;
using NSubstitute;
using Xunit;

namespace HttpClient.Middleware.BearerToken.Tests
{
    public class OutgoingMiddlewareTests : IClassFixture<WebApplicationFactory<HttpClient.Middleware.BearerToken.TestServer.Startup>>
    {
        private readonly WebApplicationFactory<HttpClient.Middleware.BearerToken.TestServer.Startup> _factory;
        private readonly CaptureAuthorizationHeader captureHeader;

        public OutgoingMiddlewareTests(WebApplicationFactory<HttpClient.Middleware.BearerToken.TestServer.Startup> factory)
        {
            captureHeader = new CaptureAuthorizationHeader();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICaptureAuthorizationHeader>(captureHeader);
                });
            });
        }

        [Fact]
        public async Task Given_A_HttpClient_With_Bearer_Token_Middlewear_When_The_Request_Is_Made_Then_The_Server_Should_Receive_A_Authorization_Header()
        {
            const string token = @"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var generator = Substitute.For<IBearerTokenGenerator>();
            generator.Generate(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(token);

            var client = CreateClient(generator);

            await client.GetAsync("api/test");

            Assert.Single(captureHeader.LastValue, $"Bearer {token}");
        }

        private System.Net.Http.HttpClient CreateClient(IBearerTokenGenerator generator)
        {
            var authMiddleware = new BearerTokenHeaderHandler(generator);
            return _factory.CreateDefaultClient(authMiddleware);
        }
    }
}
