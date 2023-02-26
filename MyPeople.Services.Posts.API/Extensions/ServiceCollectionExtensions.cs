using OpenIddict.Validation.AspNetCore;

namespace MyPeople.Services.Posts.API.Extensions;

public static class ServiceCollectionExtensions
{
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

    public static IServiceCollection ConfigureOpenIddict(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer("http://localhost:4000/");
                options.AddAudiences("my-people-services-posts");

                options.UseIntrospection()
                    .SetClientId("my-people-services-posts")
                    .SetClientSecret("SjqTkBjo3CoVUQcunJZO");

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
