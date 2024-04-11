using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Tests.Common.Fixtures;
using MyPeople.Services.Images.Tests.Common.Generators;

namespace MyPeople.Services.Images.Infrastructure.Tests.Data;

public class ApplicationDbContextTests(ApplicationDbContextFixture fixture)
    : IClassFixture<ApplicationDbContextFixture>
{
    private readonly ApplicationDbContextFixture _fixture = fixture;

    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldCreateImageEntity(Image image)
    {
        await CreateImageEntityAsync(image);

        var createdImage = await GetImageEntityByIdAsync(image.Id);
        var compareResult =
            createdImage is not null && _fixture.ImageComparer.Compare(image, createdImage);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldUpdateImageEntity(Image image)
    {
        var imageNewName = $"{Guid.NewGuid()}.png";
        var existingImage = await CreateImageEntityAsync(image);
        existingImage.Name = imageNewName;

        _fixture.DbContext.Images.Update(existingImage);
        await _fixture.DbContext.SaveChangesAsync();

        var updatedImage = await GetImageEntityByIdAsync(existingImage.Id);
        updatedImage?.Name.Should().Be(imageNewName);
    }

    [Theory]
    [MemberData(nameof(ImageDataGenerator.GetImage), MemberType = typeof(ImageDataGenerator))]
    public async Task ShouldDeleteImageEntity(Image image)
    {
        var existingImage = await CreateImageEntityAsync(image);
        _fixture.DbContext.Images.Remove(image);
        await _fixture.DbContext.SaveChangesAsync();

        var deletedImage = await GetImageEntityByIdAsync(existingImage.Id);
        deletedImage.Should().BeNull();
    }

    private async Task<Image> CreateImageEntityAsync(Image image)
    {
        var createdImage = await _fixture.DbContext.Images.AddAsync(image);
        await _fixture.DbContext.SaveChangesAsync();

        return createdImage.Entity;
    }

    private async Task<Image?> GetImageEntityByIdAsync(Guid? id)
    {
        return await _fixture.DbContext.Images.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}
