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

    public async Task<PostDto?> CreatePostAsync(PostDto postDto)
    {
        using var response = await _httpClient.PostAsJsonAsync("/posts", postDto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }

    public async Task<PostDto?> DeletePostAsync(PostDto postDto)
    {
        using var response = await _httpClient.DeleteAsync($"/posts/{postDto.Id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PostDto>>("/posts")
            ?? Enumerable.Empty<PostDto>();
    }

    public async Task<PostDto?> GetPostByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PostDto?>($"/posts/{id}");
    }

    public async Task<PostDto?> UpdatePostAsync(PostDto postDto)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/posts/{postDto.Id}", postDto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }
}