using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(assembly);
            configurations.RegisterServicesFromAssembly(typeof(GenerateProjectQueryHandler).Assembly);
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}