using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Images.Infrastructure.Tests.TestData;

namespace MyPeople.Services.Images.Infrastructure.Tests.Data;

public class ApplicationDbContextTests(ApplicationDbContextFixture fixture)
    : IClassFixture<ApplicationDbContextFixture>
{
    private readonly ApplicationDbContextFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldCreateImageEntity(Image image)
    {
        await CreateImageEntityIfNotExistsAsync(image);

        var createdImage = await GetImageEntityByIdAsync(image.Id);
        Assert.Equal(image, createdImage);
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldUpdateImageEntity(Image image)
    {
        var imageNewName = Guid.NewGuid() + ".png";
        var existingImage = await CreateImageEntityIfNotExistsAsync(image);
        existingImage.Name = imageNewName;

        _fixture.DbContext.Images.Update(existingImage);
        await _fixture.DbContext.SaveChangesAsync();

        var updatedImage = await GetImageEntityByIdAsync(existingImage.Id);
        Assert.Equal(imageNewName, updatedImage?.Name);
    }

    [Theory]
    [ClassData(typeof(ImageTestData))]
    public async Task ShouldDeleteImageEntity(Image image)
    {
        var existingImage = await CreateImageEntityIfNotExistsAsync(image);
        _fixture.DbContext.Images.Remove(image);
        await _fixture.DbContext.SaveChangesAsync();

        var deletedImage = await GetImageEntityByIdAsync(existingImage.Id);
        Assert.Null(deletedImage);
    }

    private async Task<Image> CreateImageEntityIfNotExistsAsync(Image image)
    {
        var existingImage = await GetImageEntityByIdAsync(image.Id);
        if (existingImage is not null)
        {
            return existingImage;
        }
        
        var createdImage = await _fixture.DbContext.Images.AddAsync(image);
        await _fixture.DbContext.SaveChangesAsync();
        
        return createdImage.Entity;
    }

    private async Task<Image?> GetImageEntityByIdAsync(Guid? id)
    {
        return await _fixture.DbContext.Images.FirstOrDefaultAsync(x => x.Id == id);
    }
}