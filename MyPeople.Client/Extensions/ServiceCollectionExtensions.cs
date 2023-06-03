using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyPeople.Client.Services;
using MyPeople.Services.Posts.Application.Services;

namespace MyPeople.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOidcAuthentication(options => configuration.Bind("Oidc", options.ProviderOptions));

        return services;
    }

    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("services.posts", cl => cl.BaseAddress = new Uri("http://localhost:5000"))
            .AddHttpMessageHandler(sp =>
                sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(
                authorizedUrls: new[] { "http://localhost:5000" },
                scopes: new[] { "services.posts" }
            )
        );

        return services;
    }

    public static IServiceCollection ConfigureScoped(this IServiceCollection services)
    {
        services.AddScoped<IPostService>(sp =>
            new PostService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("services.posts")));

        return services;
    }
}
