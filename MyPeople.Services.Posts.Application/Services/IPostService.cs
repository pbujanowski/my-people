using MyPeople.Services.Posts.Application.Dtos;

namespace MyPeople.Services.Posts.Application.Services;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPostsAsync();
}