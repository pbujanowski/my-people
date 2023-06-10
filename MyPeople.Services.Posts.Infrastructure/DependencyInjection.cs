using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Application.Services;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Infrastructure.Data;
using MyPeople.Services.Posts.Infrastructure.Repositories;
using MyPeople.Services.Posts.Infrastructure.Services;
using MyPeople.Services.Posts.Infrastructure.Wrappers;
using System.Reflection;

namespace MyPeople.Services.Posts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.ConfigureServices();
        services.ConfigureWrappers();
        services.ConfigureAutoMapper();

        return services;
    }

    public static async Task<IApplicationBuilder> UseInfrastructureAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        return app;
    }

    private static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Application")));

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();

        services.AddHttpClient("identity", cl => cl.BaseAddress = new Uri("http://localhost:4000/"));
        services.AddScoped<IUserService>(sp =>
            new UserService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("identity")));

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