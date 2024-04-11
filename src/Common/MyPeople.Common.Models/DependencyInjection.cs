using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Models.Validators.Resources;

namespace MyPeople.Common.Models;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureCommonModels(this IServiceCollection services)
    {
        services.ConfigureFluentValidation();

        return services;
    }

    private static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);

        ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();

        return services;
    }
}
