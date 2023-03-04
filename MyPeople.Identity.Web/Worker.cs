using MyPeople.Identity.Application.Constants;
using MyPeople.Identity.Infrastructure.Data;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyPeople.Identity.Web;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await applicationManager.FindByClientIdAsync("postman", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Postman application",
                Type = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:12345/logout-callback")
                },
                RedirectUris =
                {
                    new Uri("http://localhost:12345/login-callback")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.Prefixes.Scope + AppScopes.Services.Posts
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                },
            }, cancellationToken);
        }

        if (await applicationManager.FindByClientIdAsync("my-people-services-posts", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "my-people-services-posts",
                ClientSecret = "SjqTkBjo3CoVUQcunJZO",
                Permissions =
                {
                    Permissions.Endpoints.Introspection
                }
            }, cancellationToken);
        }

        if (await applicationManager.FindByClientIdAsync("my-people-client", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "my-people-client",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "My People web application",
                Type = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:3000/authentication/logout-callback")
                },
                RedirectUris =
                {
                    new Uri("http://localhost:3000/authentication/login-callback")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.Prefixes.Scope + AppScopes.Services.Posts
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                },
            }, cancellationToken);
        }

        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        if (await scopeManager.FindByNameAsync(AppScopes.Services.Posts, cancellationToken) is null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = AppScopes.Services.Posts,
                Resources =
                {
                    "my-people-services-posts"
                }
            }, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}