using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Application.Services;
using System.Net.Http.Json;

namespace MyPeople.Client.Services;

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PostDto>>("/posts")
            ?? Enumerable.Empty<PostDto>();
    }
}