using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyPeople.Services.Images.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.ConfigureAutoMapper();
        services.ConfigureMediatR();

        return services;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);

        return services;
    }

    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        return services;
    }
}