using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MyPeople.Common.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        services.AddSerilog();

        return services;
    }
}
