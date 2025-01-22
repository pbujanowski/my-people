using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Application.Dtos;

namespace MyPeople.Services.Images.Application.Services;

public interface IImageQueueService
{
    Task<ImageQueueResponseDto> QueueImagesAsync(IEnumerable<ImageDto> imageDtos);
}
