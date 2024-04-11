using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Services.Common.Services;
using OpenIddict.Validation.AspNetCore;

namespace MyPeople.Services.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCors(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var corsConfiguration =
            configuration.GetSection("Cors").Get<CorsConfiguration>()
            ?? throw new ConfigurationException("Cors");

        services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(
                        corsConfiguration.Origins
                            ?? throw new ConfigurationException(nameof(corsConfiguration.Origins))
                    );
            })
        );

        return services;
    }

    public static IServiceCollection ConfigureOpenIddict(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var oidcConfiguration =
            configuration.GetSection("Oidc").Get<OidcConfiguration>()
            ?? throw new ConfigurationException("Oidc");

        services
            .AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(
                    oidcConfiguration.Issuer
                        ?? throw new ConfigurationException(nameof(oidcConfiguration.Issuer))
                );
                options.AddAudiences(
                    oidcConfiguration.Audience
                        ?? throw new ConfigurationException(nameof(oidcConfiguration.Audience))
                );

                options
                    .UseIntrospection()
                    .SetClientId(
                        oidcConfiguration.ClientId
                            ?? throw new ConfigurationException(nameof(oidcConfiguration.ClientId))
                    )
                    .SetClientSecret(
                        oidcConfiguration.ClientSecret
                            ?? throw new ConfigurationException(
                                nameof(oidcConfiguration.ClientSecret)
                            )
                    );

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        return services;
    }

    public static IServiceCollection ConfigureConsul(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var serviceDiscoveryConfiguration =
            configuration.GetSection("ServiceDiscovery").Get<ServiceDiscoveryConfiguration>()
            ?? throw new ConfigurationException("ServiceDiscovery");

        var consulClient = new ConsulClient(config =>
        {
            config.Address = new Uri(
                serviceDiscoveryConfiguration?.DiscoveryAddress
                    ?? throw new ConfigurationException("ServiceDiscovery:DiscoveryAddress")
            );
        });

        services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);
        services.AddHostedService<ServiceDiscoveryHostedService>();

        return services;
    }
}
