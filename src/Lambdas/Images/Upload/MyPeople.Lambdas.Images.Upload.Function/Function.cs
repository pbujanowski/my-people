using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;
using MyPeople.Lambdas.Common.Extensions;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Upload.Application.Dtos;
using MyPeople.Lambdas.Images.Upload.Application.Services;
using MyPeople.Lambdas.Images.Upload.Infrastructure.Configurations;
using MyPeople.Lambdas.Images.Upload.Infrastructure.Services;

[assembly: LambdaSerializer(
    typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer)
)]

namespace MyPeople.Lambdas.Images.Upload.Function;

public class Function
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public Function()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var imagesAwsConfiguration =
            _configuration.GetSection("AWS").Get<ImagesAwsConfiguration>()
            ?? throw new ConfigurationException("AWS");

        var gatewaysWebUrl =
            _configuration.GetValue<string>("Gateways:Web:Url")
            ?? throw new ConfigurationException("Gateways:Web:Url");

        var services = new ServiceCollection();

        services.ConfigureLambdaLogging(_configuration);

        services.AddScoped(_ => _configuration);
        services.AddScoped<AwsConfiguration>(_ => imagesAwsConfiguration);
        services.AddScoped(_ => imagesAwsConfiguration);
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IImageStorageService, ImageStorageService>();

        services.AddHttpClient("services.images", cl => cl.BaseAddress = new Uri(gatewaysWebUrl));
        services.AddScoped<IImageFetchService>(sp => new ImageFetchService(
            sp.GetRequiredService<ILogger<IImageFetchService>>(),
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("services.images")
        ));

        _serviceProvider = services.BuildServiceProvider();
    }

    public Function(
        IConfiguration configuration,
        ImagesAwsConfiguration awsConfiguration,
        IStorageService storageService,
        IImageStorageService imageStorageService,
        IImageFetchService imageFetchService
    )
    {
        _configuration = configuration;

        var services = new ServiceCollection();

        services.ConfigureLambdaLogging(_configuration);

        services.AddScoped(_ => configuration);
        services.AddScoped<AwsConfiguration>(_ => awsConfiguration);
        services.AddScoped(_ => imageStorageService);
        services.AddScoped(_ => storageService);
        services.AddScoped(_ => imageStorageService);
        services.AddScoped(_ => imageFetchService);

        _serviceProvider = services.BuildServiceProvider();
    }

    public async Task<FunctionResponseDto> FunctionHandler(SQSEvent sqsEvent, ILambdaContext _)
    {
        var response = new FunctionResponseDto();
        using var scope = _serviceProvider.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Function>>();

        logger.LogInformation("Function invoked.");

        try
        {
            var imageStorageService =
                scope.ServiceProvider.GetRequiredService<IImageStorageService>();

            var imageFetchService = scope.ServiceProvider.GetRequiredService<IImageFetchService>();

            foreach (var record in sqsEvent.Records)
            {
                var request =
                    JsonSerializer.Deserialize<FunctionRequestDto>(record.Body)
                    ?? throw new InvalidOperationException("Function request is null.");

                logger.LogInformation("Function request: {@Request}.", request);

                var imageFetchResponse = await imageFetchService.FetchImagesByIds(
                    request.ImagesIds
                );

                if (imageFetchResponse is null)
                {
                    logger.LogError("Image fetch response is null.");
                    continue;
                }

                foreach (var image in imageFetchResponse)
                {
                    var imageUploadResponse = await imageStorageService.UploadImageAsync(image);
                    response.ImageUploadResponse.Add(imageUploadResponse);
                }
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Function execution failed.");
            response.StatusCode = 500;
            response.Message = exception.Message;
        }

        logger.LogInformation("Function response: {@Response}", response);

        return response;
    }
}
