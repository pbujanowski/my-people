using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Identity.Application.Constants;
using MyPeople.Identity.Application.Services;
using MyPeople.Identity.Domain.Entities;
using MyPeople.Identity.Infrastructure.Data;
using MyPeople.Identity.Infrastructure.Services;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyPeople.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureIdentity();
        services.ConfigureOpenIddict(configuration);
        services.ConfigureQuartz();
        services.ConfigureServices();

        return services;
    }

    public static async Task<IApplicationBuilder> UseInfrastructureAsync(
        this IApplicationBuilder app
    )
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        return app;
    }

    private static IServiceCollection ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var databaseProvider =
            configuration.GetValue<string>("DatabaseProvider")
            ?? throw new ConfigurationException("DatabaseProvider");
        var connectionString =
            configuration.GetConnectionString("Application")
            ?? throw new ConfigurationException("ConnectionStrings:Application");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            switch (databaseProvider)
            {
                case "Sqlite":
                    options.UseSqlite(
                        connectionString,
                        x =>
                            x.MigrationsAssembly(
                                "MyPeople.Identity.Infrastructure.Migrations.Sqlite"
                            )
                    );
                    break;
                case "SqlServer":
                    options.UseSqlServer(
                        connectionString,
                        x =>
                            x.MigrationsAssembly(
                                "MyPeople.Identity.Infrastructure.Migrations.SqlServer"
                            )
                    );
                    break;
                default:
                    throw new Exception($"Unsupported provider: {databaseProvider}.");
            }

            options.UseOpenIddict<
                ApplicationClient,
                ApplicationAuthorization,
                ApplicationScope,
                ApplicationToken,
                Guid
            >();
        });

        return services;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(
                options => options.SignIn.RequireConfirmedEmail = false
            )
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection ConfigureOpenIddict(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options
                    .UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<
                        ApplicationClient,
                        ApplicationAuthorization,
                        ApplicationScope,
                        ApplicationToken,
                        Guid
                    >();

                options.UseQuartz();
            })
            .AddServer(options =>
            {
                options
                    .SetAuthorizationEndpointUris("authorize")
                    .SetLogoutEndpointUris("logout")
                    .SetIntrospectionEndpointUris("introspection")
                    .SetTokenEndpointUris("token")
                    .SetUserinfoEndpointUris("userinfo");

                options.RegisterScopes(
                    Scopes.Email,
                    Scopes.Profile,
                    Scopes.Roles,
                    AppScopes.Services.Posts
                );

                options.AllowAuthorizationCodeFlow().AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .DisableTransportSecurityRequirement();

                var validIssuers = configuration.GetSection("ValidIssuers").Get<string[]>()
                    ?? throw new ConfigurationException("ValidIssuers");

                options.Configure(
                    x =>
                        x.TokenValidationParameters.ValidIssuers = validIssuers
                );
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserService, ApplicationUserService>();

        return services;
    }

    private static IServiceCollection ConfigureQuartz(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}
