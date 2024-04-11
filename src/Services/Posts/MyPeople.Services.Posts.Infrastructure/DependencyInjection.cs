using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Application.Services;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Infrastructure.Data;
using MyPeople.Services.Posts.Infrastructure.Repositories;
using MyPeople.Services.Posts.Infrastructure.Services;
using MyPeople.Services.Posts.Infrastructure.Wrappers;

namespace MyPeople.Services.Posts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.ConfigureServices(configuration);
        services.ConfigureWrappers();

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
            _ = databaseProvider switch
            {
                "Sqlite"
                    => options.UseSqlite(
                        connectionString,
                        x =>
                            x.MigrationsAssembly(
                                "MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite"
                            )
                    ),
                "SqlServer"
                    => options.UseSqlServer(
                        connectionString,
                        x =>
                            x.MigrationsAssembly(
                                "MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer"
                            )
                    ),
                _ => throw new Exception($"Unsupported provider: {databaseProvider}.")
            }
        );

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostImageRepository, PostImageRepository>();

        return services;
    }

    private static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IPostService, PostService>();

        var oidcIssuerUrl =
            configuration.GetValue<string>("Oidc:Issuer")
            ?? throw new ConfigurationException("Oidc:Issuer");

        services.AddHttpClient("identity", cl => cl.BaseAddress = new Uri(oidcIssuerUrl));
        services.AddScoped<IUserService>(sp => new UserService(
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("identity")
        ));
        var gatewayWebUrl =
            configuration.GetValue<string>("Gateways:Web:Url")
            ?? throw new ConfigurationException("Gateways:Web:Url");

        services.AddHttpClient("services.images", cl => cl.BaseAddress = new Uri(gatewayWebUrl));
        services.AddScoped<IImageService>(sp => new ImageService(
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("services.images")
        ));

        return services;
    }

    private static IServiceCollection ConfigureWrappers(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

        return services;
    }
}
