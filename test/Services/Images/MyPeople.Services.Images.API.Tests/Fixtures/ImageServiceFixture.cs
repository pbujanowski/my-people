using AutoMapper;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Application.Mappings;
using MyPeople.Services.Images.Infrastructure.Services;
using MyPeople.Services.Images.Infrastructure.Wrappers;
using NSubstitute;

namespace MyPeople.Services.Images.API.Tests.Fixtures;

public class ImageServiceFixture : ImageRepositoryFixture
{
    public IImageService ImageService { get; }

    public ObjectsComparer.Comparer<ImageDto> ImageDtoComparer { get; }

    public ImageServiceFixture()
    {
        var repositoryWrapper = new RepositoryWrapper(DbContext, ImageRepository);
        var configurationProvider = new MapperConfiguration(config =>
            config.AddProfile(typeof(MappingProfile))
        );

        var imageServiceLoggerMock = Substitute.For<ILogger<IImageService>>();
        var mapper = configurationProvider.CreateMapper();
        ImageService = new ImageService(imageServiceLoggerMock, repositoryWrapper, mapper);
        ImageDtoComparer = new ObjectsComparer.Comparer<ImageDto>();
    }
}
