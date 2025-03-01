using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyPeople.Services.Images.API.Controllers;
using MyPeople.Services.Images.Application.Services;
using NSubstitute;

namespace MyPeople.Services.Images.API.Tests.Fixtures;

public class ImagesControllerFixture : ImageServiceFixture
{
    public ImagesController ImagesController { get; set; }

    public ImagesControllerFixture()
    {
        var services = new ServiceCollection();
        var assembly = Assembly.Load("MyPeople.Services.Images.Application");

        var imageQueueServiceMock = Substitute.For<IImageQueueService>();

        services.AddScoped(_ => ImageService);
        services.AddScoped(_ => imageQueueServiceMock);
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        var serviceProvider = services.BuildServiceProvider();

        var imagesControllerLoggerMock = Substitute.For<ILogger<ImagesController>>();
        var mediatorMock = serviceProvider.GetRequiredService<IMediator>();

        ImagesController = new ImagesController(imagesControllerLoggerMock, mediatorMock);
    }
}
