using System.Net.Http.Json;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Client.Infrastructure.Services;

public class PostService(HttpClient httpClient) : IPostService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PostDto?> CreatePostAsync(CreatePostDto postDto)
    {
        using var response = await _httpClient.PostAsJsonAsync("/posts", postDto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }

    public async Task<PostDto?> DeletePostAsync(DeletePostDto postDto)
    {
        using var response = await _httpClient.DeleteAsync($"/posts/{postDto.Id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PostDto>>("/posts") ?? [];
    }

    public async Task<PostDto?> GetPostByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PostDto?>($"/posts/{id}");
    }

    public async Task<PostDto?> UpdatePostAsync(UpdatePostDto postDto)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/posts/{postDto.Id}", postDto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<PostDto>()
            : throw new Exception(response.ReasonPhrase);
    }
}