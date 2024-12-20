using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Posts.Infrastructure.Tests.Generators;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Data;

public class ApplicationDbContextTests(ApplicationDbContextFixture fixture)
    : IClassFixture<ApplicationDbContextFixture>
{
    private readonly ApplicationDbContextFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldCreatePostEntity(Post post)
    {
        await CreatePostEntityAsync(post);

        var createdPost = await GetPostEntityByIdAsync(post.Id);
        var compareResult =
            createdPost is not null && _fixture.PostComparer.Compare(post, createdPost);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldCreatePostImageEntity(PostImage postImage)
    {
        await CreatePostImageEntityAsync(postImage);

        var createdPostImage = await GetPostImageEntityByIdAsync(postImage.Id);
        var compareResult =
            createdPostImage is not null
            && _fixture.PostImageComparer.Compare(postImage, createdPostImage);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldUpdatePostEntity(Post post)
    {
        var postNewContent = $"{Guid.NewGuid()}";
        var existingPost = await CreatePostEntityAsync(post);
        existingPost.Content = postNewContent;

        _fixture.DbContext.Posts.Update(existingPost);
        await _fixture.DbContext.SaveChangesAsync();

        var updatedPost = await GetPostEntityByIdAsync(existingPost.Id);
        updatedPost?.Content.Should().Be(postNewContent);
    }

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldUpdatePostImageEntity(PostImage postImage)
    {
        var postImageNewImageId = Guid.NewGuid();
        var existingPostImage = await CreatePostImageEntityAsync(postImage);
        existingPostImage.ImageId = postImageNewImageId;

        _fixture.DbContext.PostImages.Update(existingPostImage);
        await _fixture.DbContext.SaveChangesAsync();

        var updatedPostImage = await GetPostImageEntityByIdAsync(existingPostImage.Id);
        updatedPostImage?.ImageId.Should().Be(postImageNewImageId);
    }

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldDeletePostEntity(Post post)
    {
        var existingPost = await CreatePostEntityAsync(post);
        _fixture.DbContext.Posts.Remove(post);
        await _fixture.DbContext.SaveChangesAsync();

        var deletedPost = await GetPostEntityByIdAsync(existingPost.Id);
        deletedPost.Should().BeNull();
    }

    [Theory]
    [ClassData(typeof(PostImageDataGenerator))]
    public async Task ShouldDeletePostImageEntity(PostImage postImage)
    {
        var existingPostImage = await CreatePostImageEntityAsync(postImage);
        _fixture.DbContext.PostImages.Remove(postImage);
        await _fixture.DbContext.SaveChangesAsync();

        var deletedPostmage = await GetPostImageEntityByIdAsync(existingPostImage.Id);
        deletedPostmage.Should().BeNull();
    }

    private async Task<Post> CreatePostEntityAsync(Post post)
    {
        var createdPost = await _fixture.DbContext.Posts.AddAsync(post);
        await _fixture.DbContext.SaveChangesAsync();

        return createdPost.Entity;
    }

    private async Task<PostImage> CreatePostImageEntityAsync(PostImage postImage)
    {
        var createdPostImage = await _fixture.DbContext.PostImages.AddAsync(postImage);
        await _fixture.DbContext.SaveChangesAsync();

        return createdPostImage.Entity;
    }

    private async Task<Post?> GetPostEntityByIdAsync(Guid? id)
    {
        return await _fixture.DbContext.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    private async Task<PostImage?> GetPostImageEntityByIdAsync(Guid? id)
    {
        return await _fixture
            .DbContext.PostImages.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
