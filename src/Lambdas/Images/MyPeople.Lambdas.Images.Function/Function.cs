using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Application.Dtos;
using MyPeople.Lambdas.Images.Application.Services;
using MyPeople.Lambdas.Images.Infrastructure.Services;

[assembly: LambdaSerializer(
    typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer)
)]

namespace MyPeople.Lambdas.Images.Function;

public class Function
{
    private readonly IServiceProvider _serviceProvider;

    public Function(
        IConfiguration? configuration = null,
        AwsConfiguration? awsConfiguration = null,
        IStorageService? storageService = null,
        IImageStorageService? imageStorageService = null
    )
    {
        configuration ??= new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        awsConfiguration ??=
            configuration.GetSection("AWS").Get<AwsConfiguration>()
            ?? throw new ConfigurationException("AWS");

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped(_ => configuration);
        serviceCollection.AddScoped(_ => awsConfiguration);

        if (storageService is null)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }
        else
        {
            serviceCollection.AddScoped(_ => storageService);
        }

        if (imageStorageService is null)
        {
            serviceCollection.AddScoped<IImageStorageService, ImageStorageService>();
        }
        else
        {
            serviceCollection.AddScoped(_ => imageStorageService);
        }

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public async Task<FunctionResponseDto> FunctionHandler(string input, ILambdaContext context)
    {
        var response = new FunctionResponseDto();

        try
        {
            using var scope = _serviceProvider.CreateScope();

            var imageStorageService =
                scope.ServiceProvider.GetRequiredService<IImageStorageService>();

            var imageUploadResponse =
                await imageStorageService.UploadImageAsync(input)
                ?? throw new InvalidOperationException("Image upload response is null.");

            response.StatusCode = imageUploadResponse.StatusCode;
            response.Message = imageUploadResponse.Message;
        }
        catch (Exception exception)
        {
            context.Logger.LogError(exception, "Function execution failed.");
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        return response;
    }
}
