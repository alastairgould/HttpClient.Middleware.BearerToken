using Microsoft.Extensions.DependencyInjection;

namespace HttpClient.Middleware.BearerToken
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder UsingBearerTokenAuthentication<T>(this IHttpClientBuilder builder) where T : class, IBearerTokenGenerator
        {
            builder.Services.AddTransient<T>();

            builder.AddHttpMessageHandler(serviceProvider =>
            {
                var tokenGenerator = serviceProvider.GetService<T>();
                return new BearerTokenHeaderHandler(tokenGenerator);
            });

            return builder;
        }
    }
}