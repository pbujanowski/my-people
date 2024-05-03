using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;
using MyPeople.Services.Posts.Infrastructure.Tests.Generators;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Repositories;

public class PostRepositoryTests(PostRepositoryFixture fixture)
    : IClassFixture<PostRepositoryFixture>
{
    private readonly PostRepositoryFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldCreatePostEntity(Post post)
    {
        var createdPost = await CreatePostEntityAsync(post);

        var existingPost = await GetPostEntityByIdAsync(createdPost.Id);
        var compareResult =
            existingPost is not null && _fixture.PostComparer.Compare(post, existingPost);
        compareResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldUpdatePostEntity(Post post)
    {
        var postNewContent = $"{Guid.NewGuid()}";
        var existingPost = await CreatePostEntityAsync(post);
        existingPost.Content = postNewContent;

        _fixture.PostRepository.Update(existingPost);
        await _fixture.PostRepository.SaveChangesAsync();

        var updatedPost = await GetPostEntityByIdAsync(existingPost.Id);
        updatedPost?.Content.Should().Be(postNewContent);
    }

    [Theory]
    [ClassData(typeof(PostDataGenerator))]
    public async Task ShouldDeletePostEntity(Post post)
    {
        var existingPost = await CreatePostEntityAsync(post);
        _fixture.PostRepository.Delete(post);
        await _fixture.PostRepository.SaveChangesAsync();

        var deletedPost = await GetPostEntityByIdAsync(existingPost.Id);
        deletedPost?.Should().BeNull();
    }

    private async Task<Post> CreatePostEntityAsync(Post post)
    {
        var createdPost = _fixture.PostRepository.Create(post);
        await _fixture.PostRepository.SaveChangesAsync();

        return createdPost;
    }

    private async Task<Post?> GetPostEntityByIdAsync(Guid? id)
    {
        return await _fixture.PostRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }
}
