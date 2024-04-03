using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Images.Infrastructure.Tests.TestData;

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
        Assert.NotNull(createdImageDto?.Id);
        
        var existingImageDto = await _fixture.ImageService.GetImageByIdAsync((Guid)createdImageDto.Id);
        var compareResult = existingImageDto is not null 
                            && _fixture.ImageDtoComparer.Compare(createdImageDto, existingImageDto);
        Assert.True(compareResult);
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
        Assert.NotNull(createdImageDtos);

        var existingImageDtos = await _fixture.ImageService.GetImagesByIdsAsync(createdImageDtos.Select(x => x.Id ?? Guid.Empty));
        Assert.NotNull(existingImageDtos);
        
        foreach (var createdImageDto in existingImageDtos.ToList())
        {
            Assert.NotNull(createdImageDto?.Id);
                            
            var existingImageDto = await _fixture.ImageService.GetImageByIdAsync((Guid)createdImageDto.Id);
            var compareResult = existingImageDto is not null && _fixture.ImageDtoComparer.Compare(createdImageDto, existingImageDto);
            Assert.True(compareResult);
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
        Assert.NotNull(createdImageDtos);

        var existingImageDtos = await _fixture.ImageService
            .GetImagesByIdsAsync(createdImageDtos?
                .Select(x => (Guid)x.Id!) ?? Enumerable.Empty<Guid>());
        Assert.NotNull(existingImageDtos);
        
        var existingImageDtosList = existingImageDtos.ToList();
        
        var deleteImageDtos = existingImageDtosList.Select(image =>
            new DeleteImageDto
            {
                Id = image.Id
            });

        var deletedImageDtos = await _fixture.ImageService.DeleteImagesAsync(deleteImageDtos);
        Assert.NotNull(deletedImageDtos);
        
        Assert.Equal(deletedImageDtos.Select(x => x.Id), existingImageDtosList.Select(x => x.Id));
    }
}