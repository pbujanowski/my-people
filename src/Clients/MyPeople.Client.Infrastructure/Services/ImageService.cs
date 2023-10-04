using MyPeople.Services.Images.Application.Dtos;
using MyPeople.Services.Images.Application.Services;
using System.Net.Http.Json;

namespace MyPeople.Client.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly HttpClient _httpClient;

    public ImageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ImageDto?> CreateImageAsync(ImageDto imageDto)
    {
        using var response = await _httpClient.PostAsJsonAsync("/images", imageDto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ImageDto>()
            : throw new Exception(response.ReasonPhrase);
    }

    public async Task<ImageDto?> GetImageByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<ImageDto?>($"/images/{id}");
    }
}
