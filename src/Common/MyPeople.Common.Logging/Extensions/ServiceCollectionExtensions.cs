using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MyPeople.Common.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLogging(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        LoggingInitializer.Initialize(configuration);
        services.AddSerilog(dispose: true);

        return services;
    }
}
