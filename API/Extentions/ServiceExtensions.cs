using Microsoft.Extensions.Azure;
using Domain.Repositories;
using Persistance.Repositories;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var sqlliteConnectionString = config.GetConnectionString("DefaultConnection");
            var azureStorageConnectionString = config.GetValue<string>("AzureStorage:connectionString");

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAzureClients(azureBuilder =>
            {
                azureBuilder.AddBlobServiceClient(azureStorageConnectionString);
            });

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

/*            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
*/            services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

            return services;
        }
    }
}
