using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Common.Dtos;
using MyPeople.Services.Common.Services;
using MyPeople.Services.Images.Application.Dtos;
using MyPeople.Services.Images.Application.Services;
using MyPeople.Services.Images.Infrastructure.Configurations;

namespace MyPeople.Services.Images.Infrastructure.Services;

public class ImageQueueService(
    ILogger<IImageQueueService> logger,
    IQueueService queueService,
    ImagesAwsConfiguration imagesAwsConfiguration
) : IImageQueueService
{
    public async Task<ImageQueueResponseDto> QueueImagesAsync(IEnumerable<ImageDto> imageDtos)
    {
        var response = new ImageQueueResponseDto();

        try
        {
            logger.LogInformation("Images queuing started.");
            logger.LogDebug("QueueImagesAsync request: {@Request}.", imageDtos);

            var sqsMessage = new { ImagesIds = imageDtos.Select(image => image.Id) };

            var sqsRequestDto = new SQSRequestDto
            {
                Message = JsonSerializer.Serialize(sqsMessage),
                QueueUrl =
                    imagesAwsConfiguration?.ImageQueue?.QueueUrl
                    ?? throw new InvalidOperationException("Queue URL is null."),
            };

            var sqsResponse = await queueService.QueueObjectAsync(sqsRequestDto);

            logger.LogInformation("Images queuing response: {@Response}.", sqsResponse);

            response.StatusCode = sqsResponse.StatusCode;
            response.Message = sqsResponse.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Images queuing failed.");
            response.StatusCode = 500;
            response.Message = $"Images queuing failed. Message: {exception.Message}";
        }

        return response;
    }
}
