using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyPeople.Lambdas.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLambdaLogging(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddLogging(options =>
        {
            options.AddLambdaLogger();
        });

        return services;
    }
}
