using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Posts.Infrastructure.Tests.Generators;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Repositories;

public class PostImageRepositoryTests(PostImageRepositoryFixture fixture)
    : IClassFixture<PostImageRepositoryFixture>
{
    private readonly PostImageRepositoryFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldCreatePostImageEntity(PostImage postImage)
    {
        var createdPostImage = await CreatePostImageEntityAsync(postImage);

        var existingPostImage = await GetPostImageEntityByIdAsync(createdPostImage.Id);
        var compareResult =
            existingPostImage is not null
            && _fixture.PostImageComparer.Compare(postImage, existingPostImage);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldUpdatePostImageEntity(PostImage postImage)
    {
        var postImageNewImageId = Guid.NewGuid();
        var existingPostImage = await CreatePostImageEntityAsync(postImage);
        existingPostImage.ImageId = postImageNewImageId;

        _fixture.PostImageRepository.Update(existingPostImage);
        await _fixture.PostImageRepository.SaveChangesAsync();

        var updatedPostImage = await GetPostImageEntityByIdAsync(existingPostImage.Id);
        updatedPostImage?.ImageId.Should().Be(postImageNewImageId);
    }

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldDeletePostImageEntity(PostImage postImage)
    {
        var existingPostImage = await CreatePostImageEntityAsync(postImage);
        _fixture.PostImageRepository.Delete(postImage);
        await _fixture.PostImageRepository.SaveChangesAsync();

        var deletedPostImage = await GetPostImageEntityByIdAsync(existingPostImage.Id);
        deletedPostImage?.Should().BeNull();
    }

    private async Task<PostImage> CreatePostImageEntityAsync(PostImage postImage)
    {
        var createdPostImage = _fixture.PostImageRepository.Create(postImage);
        await _fixture.PostImageRepository.SaveChangesAsync();

        return createdPostImage;
    }

    private async Task<PostImage?> GetPostImageEntityByIdAsync(Guid? id)
    {
        return await _fixture
            .PostImageRepository.FindByCondition(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}
