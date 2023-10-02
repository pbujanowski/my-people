using OpenIddict.Validation.AspNetCore;

namespace MyPeople.Services.Images.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(
            options =>
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                })
        );

        return services;
    }

    public static IServiceCollection ConfigureOpenIddict(this IServiceCollection services)
    {
        services
            .AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer("http://localhost:4000/");
                options.AddAudiences("my-people-services-images");

                options
                    .UseIntrospection()
                    .SetClientId("my-people-services-images")
                    .SetClientSecret("8Z9Owkb4RZuhI7icUzGV");

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
}
