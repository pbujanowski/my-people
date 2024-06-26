﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyPeople.Services.Posts.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.ConfigureAutoMapper();

        return services;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);

        return services;
    }
}
