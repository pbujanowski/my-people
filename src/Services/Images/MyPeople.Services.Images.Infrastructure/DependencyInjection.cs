using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Application.Wrappers;
using MyPeople.Services.Images.Infrastructure.Data;
using MyPeople.Services.Images.Infrastructure.Repositories;
using MyPeople.Services.Images.Infrastructure.Services;
using MyPeople.Services.Images.Infrastructure.Wrappers;

namespace MyPeople.Services.Images.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.ConfigureServices();
        services.ConfigureWrappers();

        return services;
    }

    public static async Task<IApplicationBuilder> UseInfrastructureAsync(
        this IApplicationBuilder app
    )
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
                "Sqlite" => options.UseSqlite(
                    connectionString,
                    x =>
                        x.MigrationsAssembly(
                            "MyPeople.Services.Images.Infrastructure.Migrations.Sqlite"
                        )
                ),
                "SqlServer" => options.UseSqlServer(
                    connectionString,
                    x =>
                        x.MigrationsAssembly(
                            "MyPeople.Services.Images.Infrastructure.Migrations.SqlServer"
                        )
                ),
                _ => throw new Exception($"Unsupported provider: {databaseProvider}."),
            }
        );

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IImageRepository, ImageRepository>();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();

        return services;
    }

    private static IServiceCollection ConfigureWrappers(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

        return services;
    }
}
