using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastucture.Services;

namespace Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<ChatService>((serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(config.GetValue<string>("ChatService:url"));
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            };
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }
}