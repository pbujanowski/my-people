using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Abstractions.Services;

public interface IPostService
{
    Task<PostDto?> CreatePostAsync(CreatePostDto postDto);

    Task<PostDto?> DeletePostAsync(DeletePostDto postDto);

    Task<IEnumerable<PostDto>> GetAllPostsAsync();

    Task<PostDto?> GetPostByIdAsync(Guid id);

    Task<PostDto?> UpdatePostAsync(UpdatePostDto postDto);
}