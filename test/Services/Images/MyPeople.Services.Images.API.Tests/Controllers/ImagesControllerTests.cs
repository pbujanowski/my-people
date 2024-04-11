using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.API.Tests.Fixtures;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Tests.Generators;

namespace MyPeople.Services.Images.API.Tests.Controllers;

public class ImagesControllerTests(ImagesControllerFixture fixture)
    : IClassFixture<ImagesControllerFixture>
{
    private readonly ImagesControllerFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(ImageDataGenerator))]
    public async Task ShouldGetImageByIdReturnOkObjectResult(Image image)
    {
        var createdImageDto = await CreateImageAndAssertAsync(image);
        var result = await _fixture.ImagesController.GetImageById((Guid)createdImageDto.Id!);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task ShouldGetImageByIdReturnNotFoundResult()
    {
        var imageId = Guid.NewGuid();
        var result = await _fixture.ImagesController.GetImageById(imageId);
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(ImageDataGenerator))]
    public async Task ShouldBrowseImageByIdReturnFileContentResult(Image image)
    {
        var createdImageDto = await CreateImageAndAssertAsync(image);
        var result = await _fixture.ImagesController.BrowseImageById((Guid)createdImageDto.Id!);
        result.Should().BeOfType<FileContentResult>();
    }

    [Fact]
    public async Task ShouldBrowseImageByIdReturnNotFoundResult()
    {
        var imageId = Guid.NewGuid();
        var result = await _fixture.ImagesController.BrowseImageById(imageId);
        result.Should().BeOfType<NotFoundResult>();
    }

    // TODO: Should be fixed, because there is an exception from EF Core.
    // [Theory]
    // [MemberData(nameof(ImageCollectionDataGenerator.GetImages), MemberType = typeof(ImageCollectionDataGenerator))]
    // public async Task ShouldGetImagesByIdsReturnOkObjectResult(IEnumerable<Image> images)
    // {
    //     var createdImagesDtos = await CreateImagesAndAssertAsync(images);
    //     var createdImagesDtosList = createdImagesDtos?.ToList();
    //     createdImagesDtosList?.Should().NotBeNull();
    //
    //     var result = await _fixture.ImagesController.GetImagesByIds(createdImagesDtosList!.Select(x => (Guid)x.Id!));
    //     result.Should().BeOfType<OkObjectResult>();
    // }

    [Fact]
    public async Task ShouldGetImagesByIdsReturnNotFoundResult()
    {
        var imagesIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };

        var result = await _fixture.ImagesController.GetImagesByIds(imagesIds);
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(ImageCollectionDataGenerator))]
    public async Task ShouldCreateImagesReturnOkObjectResult(IEnumerable<Image> images)
    {
        var createImageDtos = MapImagesToCreateImageDtos(images);
        var result = await _fixture.ImagesController.CreateImages(createImageDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImageCollectionDataGenerator))]
    public async Task ShouldDeleteImagesReturnOkObjectResult(IEnumerable<Image> images)
    {
        var createdImagesDtos = await CreateImagesAndAssertAsync(images);
        var createdImagesDtosList = createdImagesDtos?.ToList();
        createdImagesDtosList.Should().NotBeNull();

        var deleteImageDtos = MapImageDtosToDeleteImageDtos(createdImagesDtosList!);
        var result = await _fixture.ImagesController.DeleteImages(deleteImageDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImageCollectionDataGenerator))]
    public async Task ShouldDeleteImagesReturnStatusCodeResult(IEnumerable<Image> images)
    {
        var deleteImageDtos = MapImagesToDeleteImageDtos(images);
        var result = await _fixture.ImagesController.DeleteImages(deleteImageDtos);
        result.Should().BeOfType<StatusCodeResult>();

        var statusCodeResult = result as StatusCodeResult;
        statusCodeResult?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    private async Task<ImageDto> CreateImageAndAssertAsync(Image image)
    {
        var createImageDto = MapImageToCreateImageDto(image);

        var createdImageDto = await _fixture.ImageService.CreateImageAsync(createImageDto);

        createdImageDto.Should().NotBeNull();

        createdImageDto!.Id.Should().NotBeNull().And.NotBe(Guid.Empty);

        return createdImageDto;
    }

    private async Task<IEnumerable<ImageDto>?> CreateImagesAndAssertAsync(IEnumerable<Image> images)
    {
        var createImageDtos = MapImagesToCreateImageDtos(images);

        var createdImagesDtos = await _fixture.ImageService.CreateImagesAsync(createImageDtos);
        var createdImagesDtosList = createdImagesDtos?.ToList();

        createdImagesDtosList?.Should().AllSatisfy(image => image.Should().NotBeNull());

        createdImagesDtosList
            ?.Should()
            .AllSatisfy(image => image.Id.Should().NotBeNull().And.NotBe(Guid.Empty));

        return createdImagesDtosList;
    }

    private CreateImageDto MapImageToCreateImageDto(Image image)
    {
        return new CreateImageDto
        {
            Name = image.Name,
            Content = image.Content,
            ContentType = image.ContentType
        };
    }

    private IEnumerable<CreateImageDto> MapImagesToCreateImageDtos(IEnumerable<Image> images)
    {
        return images.Select(image => new CreateImageDto
        {
            Name = image.Name,
            Content = image.Content,
            ContentType = image.ContentType
        });
    }

    private IEnumerable<DeleteImageDto> MapImagesToDeleteImageDtos(IEnumerable<Image> images)
    {
        return images.Select(image => new DeleteImageDto { Id = image.Id });
    }

    private IEnumerable<DeleteImageDto> MapImageDtosToDeleteImageDtos(
        IEnumerable<ImageDto> imageDtos
    )
    {
        return imageDtos.Select(image => new DeleteImageDto { Id = image.Id });
    }
}
