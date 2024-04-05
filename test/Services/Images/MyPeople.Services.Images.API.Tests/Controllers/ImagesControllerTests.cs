using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.API.Tests.Fixtures;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Tests.Common.TestData;
using NSubstitute.Extensions;

namespace MyPeople.Services.Images.API.Tests.Controllers;

public class ImagesControllerTests(ImagesControllerFixture fixture) : IClassFixture<ImagesControllerFixture>
{
    private readonly ImagesControllerFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldGetImageByIdReturnOkObjectResult(Image image)
    {
        var createdImageDto = await CreateImageAndAssertAsync(image);
        var result = await _fixture.ImagesController.GetImageById((Guid)createdImageDto.Id!);
        result.Should().BeOfType<OkObjectResult>();
    }
    
    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldGetImageByIdReturnNotFoundResult(Image image)
    {
        image.Id
            .Should().NotBeNull()
            .And
            .NotBe(Guid.Empty);

        var result = await _fixture.ImagesController.GetImageById((Guid)image.Id!);
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldBrowseImageByIdReturnOkObjectResult(Image image)
    {
        var createdImageDto = await CreateImageAndAssertAsync(image);
        var result = await _fixture.ImagesController.BrowseImageById((Guid)createdImageDto.Id!);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldBrowseImageByIdReturnNotFoundResult(Image image)
    {
        image.Id
            .Should().NotBeNull()
            .And
            .NotBe(Guid.Empty);

        var result = await _fixture.ImagesController.BrowseImageById((Guid)image.Id!);
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldGetImagesByIdsReturnOkObjectResult(IEnumerable<Image> images)
    {
        var createdImagesDtos = await CreateImagesAndAssertAsync(images);
        var result = await _fixture.ImagesController.GetImagesByIds(createdImagesDtos.Select(x => (Guid)x.Id!));
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldGetImagesByIdsReturnNotFoundResult(IEnumerable<Image> images)
    {
        var imagesList = images.ToList();
        imagesList.Should().AllSatisfy(image =>
            image.Id
                .Should().NotBeNull()
                .And
                .NotBe(Guid.Empty));

        var result = await _fixture.ImagesController.GetImagesByIds(imagesList.Select(x => (Guid)x.Id!));
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldCreateImagesReturnOkObjectResult(IEnumerable<Image> images)
    {
        var createImageDtos = MapImagesToCreateImageDtos(images);
        var result = await _fixture.ImagesController.CreateImages(createImageDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldDeleteImagesReturnOkObjectResult(IEnumerable<Image> images)
    {
        var createdImagesDtos = await CreateImagesAndAssertAsync(images);
        var deleteImageDtos = MapImageDtosToDeleteImageDtos(createdImagesDtos);
        var result = await _fixture.ImagesController.DeleteImages(deleteImageDtos);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(ImagesTestData))]
    public async Task ShouldDeleteImagesReturnStatusCodeResult(IEnumerable<Image> images)
    {
        var deleteImageDtos = MapImagesToDeleteImageDtos(images);
        var result = await _fixture.ImagesController.DeleteImages(deleteImageDtos);
        result.Should().BeOfType<StatusCodeResult>();
        
        var statusCodeResult = result as StatusCodeResult;
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    private async Task<ImageDto> CreateImageAndAssertAsync(Image image)
    {
        var createImageDto = MapImageToCreateImageDto(image);
        
        var createdImageDto = await _fixture.ImageService.CreateImageAsync(createImageDto);
        
        createdImageDto
            .Should().NotBeNull();
        
        createdImageDto!.Id
            .Should().NotBeNull()
            .And
            .NotBe(Guid.Empty);
                
        return createdImageDto;
    }

    private async Task<IEnumerable<ImageDto>> CreateImagesAndAssertAsync(IEnumerable<Image> images)
    {
        var createImageDtos = MapImagesToCreateImageDtos(images);

        var createdImagesDtos = await _fixture.ImageService.CreateImagesAsync(createImageDtos);

        createdImagesDtos.Should().AllSatisfy(image =>
            image
                .Should().NotBeNull());

        createdImagesDtos.Should().AllSatisfy(image =>
            image.Id
                .Should().NotBeNull()
                .And
                .NotBe(Guid.Empty));
        
        return createdImagesDtos;
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
        return images.Select(image => new DeleteImageDto
        {
            Id = image.Id
        });
    }

    private IEnumerable<DeleteImageDto> MapImageDtosToDeleteImageDtos(IEnumerable<ImageDto> imageDtos)
    {
        return imageDtos.Select(image => new DeleteImageDto
        {
            Id = image.Id
        });
    }
}