using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;

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

    public static IServiceCollection ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
        });

        return services;
    }

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
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(
                        corsConfiguration.Origins
                            ?? throw new ConfigurationException(nameof(corsConfiguration.Origins))
                    )
            )
        );

        return services;
    }
}
