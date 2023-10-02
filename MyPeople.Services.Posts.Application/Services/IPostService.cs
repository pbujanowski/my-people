using MyPeople.Services.Posts.Application.Dtos;

namespace MyPeople.Services.Posts.Application.Services;

public interface IPostService
{
    Task<PostDto?> CreatePostAsync(PostDto postDto);

    Task<PostDto?> DeletePostAsync(PostDto postDto);

    Task<IEnumerable<PostDto>> GetAllPostsAsync();

    Task<PostDto?> GetPostByIdAsync(Guid id);

    Task<PostDto?> UpdatePostAsync(PostDto postDto);
}
