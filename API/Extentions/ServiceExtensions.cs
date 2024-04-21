using Application;
using Infrastucture;
using Persistences;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var sqlliteConnectionString = config.GetConnectionString("DefaultConnection");

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            // Registering the dependency injections
            services.AddApplication().AddInfrastucture(config).AddPersistence(config);
        
            return services;
        }
    }
}
