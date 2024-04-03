using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Images.Infrastructure.Tests.TestData;

namespace MyPeople.Services.Images.Infrastructure.Tests.Repositories;

public class ImageRepositoryTests(ImageRepositoryFixture fixture) : IClassFixture<ImageRepositoryFixture>
{
    private readonly ImageRepositoryFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldCreateImageEntity(Image image)
    {
        var createdImage = await CreateImageEntityAsync(image);

        var existingImage = await GetImageEntityByIdAsync(createdImage.Id);
        var compareResult = existingImage is not null && _fixture.ImageComparer.Compare(image, existingImage);
        Assert.True(compareResult);
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldUpdateImageEntity(Image image)
    {
        var imageNewName = $"{Guid.NewGuid()}.png";
        var existingImage = await CreateImageEntityAsync(image);
        existingImage.Name = imageNewName;

        _fixture.ImageRepository.Update(existingImage);
        await _fixture.ImageRepository.SaveChangesAsync();
        
        var updatedImage = await GetImageEntityByIdAsync(existingImage.Id);
        Assert.Equal(imageNewName, updatedImage?.Name);
    }
    
    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldDeleteImageEntity(Image image)
    {
        var existingImage = await CreateImageEntityAsync(image);
        _fixture.ImageRepository.Delete(image);
        await _fixture.ImageRepository.SaveChangesAsync();
    
        var deletedImage = await GetImageEntityByIdAsync(existingImage.Id);
        Assert.Null(deletedImage);
    }

    private async Task<Image> CreateImageEntityAsync(Image image)
    {
        var createdImage = _fixture.ImageRepository.Create(image);
        await _fixture.ImageRepository.SaveChangesAsync();
                
        return createdImage;
    }

    private async Task<Image?> GetImageEntityByIdAsync(Guid? id)
    {
        return await _fixture.ImageRepository
            .FindByCondition(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}