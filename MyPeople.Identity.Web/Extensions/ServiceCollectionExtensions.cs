using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPeople.Identity.Web.Constants;
using MyPeople.Identity.Web.Data;
using MyPeople.Identity.Web.Entities;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyPeople.Identity.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCookiePolicy(this IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Application");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.UseOpenIddict<ApplicationClient, ApplicationAuthorization, ApplicationScope, ApplicationToken, Guid>();
        });

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedEmail = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
        });

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000");
            }));

        return services;
    }

    public static IServiceCollection ConfigureQuartz(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }

    public static IServiceCollection ConfigureOpenIddict(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddCore(options =>
             {
                 options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>()
                        .ReplaceDefaultEntities<ApplicationClient, ApplicationAuthorization, ApplicationScope, ApplicationToken, Guid>();

                 options.UseQuartz();
             })
            .AddServer(options =>
            {
                options.SetAuthorizationEndpointUris("authorize")
                       .SetLogoutEndpointUris("logout")
                       .SetIntrospectionEndpointUris("introspection")
                       .SetTokenEndpointUris("token")
                       .SetUserinfoEndpointUris("userinfo");

                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, AppScopes.Services.Posts);

                options.AllowAuthorizationCodeFlow()
                       .AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableStatusCodePagesIntegration()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .DisableTransportSecurityRequirement();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return services;
    }
}