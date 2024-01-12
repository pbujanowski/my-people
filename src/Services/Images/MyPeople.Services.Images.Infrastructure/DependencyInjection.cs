using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Application.Services;
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
        services.ConfigureAutoMapper();

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
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlite(configuration.GetConnectionString("Application"))
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

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);

        return services;
    }
}
