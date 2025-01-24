using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Images.Upload.Application.Services;

namespace MyPeople.Lambdas.Images.Upload.Infrastructure.Services;

public class ImageFetchService(ILogger<IImageFetchService> logger, HttpClient httpClient)
    : IImageFetchService
{
    public async Task<IEnumerable<ImageDto>?> FetchImagesByIds(IEnumerable<Guid> imagesIds)
    {
        logger.LogInformation("Images fetching started.");

        try
        {
            logger.LogDebug("FetchImagesByIds request: {@Request}", imagesIds);

            var content = JsonSerializer.Serialize(imagesIds);
            using var response = await httpClient.PostAsync(
                "/images/many",
                new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
            );

            logger.LogInformation("Images fetching finished. Response: {@Response}.", response);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<IEnumerable<ImageDto>>()
                : null;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Images fetching failed.");
            return null;
        }
    }
}
