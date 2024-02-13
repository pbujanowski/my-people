using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Client.Infrastructure.Services;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Configuration.Exceptions;

namespace MyPeople.Client.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOidcAuthentication(
            options => configuration.Bind("Oidc", options.ProviderOptions)
        );

        return services;
    }

    public static IServiceCollection ConfigureHttpClients(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var gatewayWebUrl =
            configuration.GetValue<string>("Gateways:Web:Url")
            ?? throw new ConfigurationException("Gateways:Web:Url");

        var servicesPostsScopes = new[] { "services.posts" };

        services
            .AddHttpClient("services.posts", cl => cl.BaseAddress = new Uri(gatewayWebUrl))
            .AddHttpMessageHandler(
                sp =>
                    sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler([gatewayWebUrl], servicesPostsScopes)
            );

        return services;
    }

    public static IServiceCollection ConfigureScoped(this IServiceCollection services)
    {
        services.AddScoped<IPostService>(
            sp =>
                new PostService(
                    sp.GetRequiredService<IHttpClientFactory>().CreateClient("services.posts")
                )
        );

        return services;
    }
}