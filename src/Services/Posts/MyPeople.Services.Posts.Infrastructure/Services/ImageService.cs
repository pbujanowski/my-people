using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Posts.Infrastructure;

public class ImageService(HttpClient httpClient) : IImageService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ImageDto?> CreateImageAsync(CreateImageDto imageDto)
    {
        var content = JsonSerializer.Serialize(imageDto);
        using var response = await _httpClient.PostAsync(
            "/images",
            new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
        );
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ImageDto>()
            : null;
    }

    public async Task<IEnumerable<ImageDto>?> CreateImagesAsync(
        IEnumerable<CreateImageDto> imagesDtos
    )
    {
        var content = JsonSerializer.Serialize(imagesDtos);
        using var response = await _httpClient.PostAsync(
            "/images",
            new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
        );
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<IEnumerable<ImageDto>>()
            : null;
    }

    public async Task<IEnumerable<ImageDto>?> DeleteImagesAsync(
        IEnumerable<DeleteImageDto> imagesDtos
    )
    {
        var content = JsonSerializer.Serialize(imagesDtos);
        using var response = await _httpClient.PostAsync(
            "/images/delete",
            new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
        );
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<IEnumerable<ImageDto>>()
            : null;
    }

    public async Task<ImageDto?> GetImageByIdAsync(Guid id)
    {
        using var response = await _httpClient.GetAsync($"/images/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<ImageDto>()
            : null;
    }

    public async Task<IEnumerable<ImageDto>?> GetImagesByIdsAsync(IEnumerable<Guid> ids)
    {
        var content = JsonSerializer.Serialize(ids);
        using var response = await _httpClient.PostAsync(
            "/images/many",
            new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
        );
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<IEnumerable<ImageDto>>()
            : null;
    }
}