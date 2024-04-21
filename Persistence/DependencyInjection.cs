using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Repositories;
using Persistence.Repositories;

namespace Persistences;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var azureStorageConnectionString = config.GetValue<string>("AzureStorage:connectionString");

        services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(azureStorageConnectionString);
        });

        services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
        
        return services;
    }
}