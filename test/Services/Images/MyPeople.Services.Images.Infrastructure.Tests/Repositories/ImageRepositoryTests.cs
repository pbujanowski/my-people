using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Tests.Common.Fixtures;
using MyPeople.Services.Images.Tests.Common.Generators;

namespace MyPeople.Services.Images.Infrastructure.Tests.Repositories;

public class ImageRepositoryTests(ImageRepositoryFixture fixture) : IClassFixture<ImageRepositoryFixture>
{
    private readonly ImageRepositoryFixture _fixture = fixture;

    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldCreateImageEntity(Image image)
    {
        var createdImage = await CreateImageEntityAsync(image);

        var existingImage = await GetImageEntityByIdAsync(createdImage.Id);
        var compareResult = existingImage is not null && _fixture.ImageComparer.Compare(image, existingImage);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldUpdateImageEntity(Image image)
    {
        var imageNewName = $"{Guid.NewGuid()}.png";
        var existingImage = await CreateImageEntityAsync(image);
        existingImage.Name = imageNewName;

        _fixture.ImageRepository.Update(existingImage);
        await _fixture.ImageRepository.SaveChangesAsync();
        
        var updatedImage = await GetImageEntityByIdAsync(existingImage.Id);
        updatedImage?.Name.Should().Be(imageNewName);
    }
    
    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldDeleteImageEntity(Image image)
    {
        var existingImage = await CreateImageEntityAsync(image);
        _fixture.ImageRepository.Delete(image);
        await _fixture.ImageRepository.SaveChangesAsync();
    
        var deletedImage = await GetImageEntityByIdAsync(existingImage.Id);
        deletedImage.Should().BeNull();
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