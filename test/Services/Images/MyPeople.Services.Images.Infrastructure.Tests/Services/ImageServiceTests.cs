using FluentAssertions;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Tests.Common.Fixtures;
using MyPeople.Services.Images.Tests.Common.TestData;

namespace MyPeople.Services.Images.Infrastructure.Tests.Services;

public class ImageServiceTests(ImageServiceFixture fixture) : IClassFixture<ImageServiceFixture>
{
    private readonly ImageServiceFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldCreateImageEntity(Image image)
    {
        var createImageDto = new CreateImageDto
        {
            Name = image.Name,
            Content = image.Content,
            ContentType = image.ContentType
        };
        var createdImageDto = await _fixture.ImageService.CreateImageAsync(createImageDto);
        createdImageDto?.Id.Should().NotBeNull();
        
        var existingImageDto = await _fixture.ImageService.GetImageByIdAsync((Guid)createdImageDto!.Id!);
        var compareResult = existingImageDto is not null 
                            && _fixture.ImageDtoComparer.Compare(createdImageDto, existingImageDto);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldCreateImageEntities(IEnumerable<Image> images)
    {
        var createImageDtos = images.Select(image =>
            new CreateImageDto
            {
                Name = image.Name,
                Content = image.Content, 
                ContentType = image.ContentType
            }).ToList();

        var createdImageDtos = await _fixture.ImageService.CreateImagesAsync(createImageDtos);
        var createdImageDtosList = createdImageDtos?.ToList();
        createdImageDtosList.Should().NotBeNull();

        var existingImageDtos = await _fixture.ImageService.GetImagesByIdsAsync(createdImageDtosList!.Select(x => x.Id ?? Guid.Empty));
        var existingImageDtosList = existingImageDtos?.ToList();
        existingImageDtosList.Should().NotBeNull();
        
        foreach (var createdImageDto in existingImageDtosList!)
        {
            createdImageDto?.Id.Should().NotBeNull();
                            
            var existingImageDto = await _fixture.ImageService.GetImageByIdAsync((Guid)createdImageDto!.Id!);
            var compareResult = existingImageDto is not null && _fixture.ImageDtoComparer.Compare(createdImageDto, existingImageDto);
            compareResult.Should().BeTrue();
        }
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldDeleteImageEntities(IEnumerable<Image> images)
    {
        var createImageDtos = images.Select(image =>
            new CreateImageDto
            {
                Name = image.Name,
                Content = image.Content,
                ContentType = image.ContentType
            });

        var createdImageDtos = await _fixture.ImageService.CreateImagesAsync(createImageDtos);
        var createdImageDtosList = createdImageDtos?.ToList();
        createdImageDtosList.Should().NotBeNull();

        var existingImageDtos = await _fixture.ImageService
            .GetImagesByIdsAsync(createdImageDtosList?
                .Select(x => (Guid)x.Id!) ?? Enumerable.Empty<Guid>());

        var existingImageDtosList = existingImageDtos?.ToList();
        existingImageDtosList.Should().NotBeNull();
        
        var deleteImageDtos = existingImageDtosList!.Select(image =>
            new DeleteImageDto
            {
                Id = image.Id
            });

        var deletedImageDtos = await _fixture.ImageService.DeleteImagesAsync(deleteImageDtos);
        var deletedImageDtosList = deletedImageDtos?.ToList();
        deletedImageDtosList.Should().NotBeNull();

        existingImageDtosList!.Select(x => x.Id)
            .Should()
            .BeEquivalentTo(deletedImageDtosList!.Select(x => x.Id));
    }
}