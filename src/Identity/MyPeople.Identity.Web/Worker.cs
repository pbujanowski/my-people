using Microsoft.AspNetCore.Identity;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Identity.Application;
using MyPeople.Identity.Application.Constants;
using MyPeople.Identity.Domain.Entities;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyPeople.Identity.Web;

public class Worker(IServiceProvider serviceProvider, IConfiguration configuration) : IHostedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var applicationManager =
            scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (await applicationManager.FindByClientIdAsync("postman", cancellationToken) is null)
            await applicationManager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Postman application",
                    ClientType = ClientTypes.Public,
                    PostLogoutRedirectUris = { new Uri("http://localhost:12345/logout-callback") },
                    RedirectUris = { new Uri("http://localhost:12345/login-callback") },
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
                    Requirements = { Requirements.Features.ProofKeyForCodeExchange }
                },
                cancellationToken
            );

        if (
            await applicationManager.FindByClientIdAsync(
                "my-people-services-posts",
                cancellationToken
            )
            is null
        )
            await applicationManager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "my-people-services-posts",
                    ClientSecret = "SjqTkBjo3CoVUQcunJZO",
                    Permissions = { Permissions.Endpoints.Introspection }
                },
                cancellationToken
            );

        if (
            await applicationManager.FindByClientIdAsync(
                "my-people-services-images",
                cancellationToken
            )
            is null
        )
            await applicationManager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "my-people-services-images",
                    ClientSecret = "8Z9Owkb4RZuhI7icUzGV",
                    Permissions = { Permissions.Endpoints.Introspection }
                },
                cancellationToken
            );

        if (
            await applicationManager.FindByClientIdAsync("my-people-client", cancellationToken)
            is null
        )
            await applicationManager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "my-people-client",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "My People web application",
                    ClientType = ClientTypes.Public,
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
                    Requirements = { Requirements.Features.ProofKeyForCodeExchange }
                },
                cancellationToken
            );

        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        if (await scopeManager.FindByNameAsync(AppScopes.Services.Posts, cancellationToken) is null)
            await scopeManager.CreateAsync(
                new OpenIddictScopeDescriptor
                {
                    Name = AppScopes.Services.Posts,
                    Resources = { "my-people-services-posts" }
                },
                cancellationToken
            );

        if (!await roleManager.RoleExistsAsync(AppRoles.Administrator))
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.Administrator));

        if (!await roleManager.RoleExistsAsync(AppRoles.User))
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.User));

        var administratorEmail =
            _configuration.GetSection("Administrator:Email").Get<string>()
            ?? throw new ConfigurationException("Administrator:Email");

        var administratorPassword =
            _configuration.GetSection("Administrator:Password").Get<string>()
            ?? throw new ConfigurationException("Administrator:Password");

        if (await userManager.FindByEmailAsync(administratorEmail) is null)
        {
            var result = await userManager.CreateAsync(
                new ApplicationUser { Email = administratorEmail, UserName = administratorEmail },
                administratorPassword
            );

            var administratorUser =
                await userManager.FindByEmailAsync(administratorEmail)
                ?? throw new Exception(result.Errors.Select(x => x.Description).ToString());

            await userManager.AddToRoleAsync(administratorUser, AppRoles.Administrator);
        }

        var userEmail =
            _configuration.GetSection("User:Email").Get<string>()
            ?? throw new ConfigurationException("User:Email");

        var userPassword =
            _configuration.GetSection("User:Password").Get<string>()
            ?? throw new ConfigurationException("User:Password");

        if (await userManager.FindByEmailAsync(userEmail) is null)
        {
            var result = await userManager.CreateAsync(
                new ApplicationUser { Email = userEmail, UserName = userEmail },
                userPassword
            );

            var user =
                await userManager.FindByEmailAsync(userEmail)
                ?? throw new Exception(result.Errors.Select(x => x.Description).ToString());

            await userManager.AddToRoleAsync(user, AppRoles.User);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
