using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;

namespace MyPeople.Gateways.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCors(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddCors(options =>
        {
            var corsConfiguration =
                configuration.GetSection("Cors").Get<CorsConfiguration>()
                ?? throw new ConfigurationException("Cors");

            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(
                        corsConfiguration.Origins
                        ?? throw new ConfigurationException(nameof(corsConfiguration.Origins))
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}