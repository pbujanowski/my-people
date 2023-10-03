using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyPeople.Client.Services;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Services.Posts.Application.Services;

namespace MyPeople.Client.Web.Extensions;

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

        services
            .AddHttpClient("services.posts", cl => cl.BaseAddress = new Uri(gatewayWebUrl))
            .AddHttpMessageHandler(
                sp =>
                    sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { gatewayWebUrl },
                            scopes: new[] { "services.posts" }
                        )
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
