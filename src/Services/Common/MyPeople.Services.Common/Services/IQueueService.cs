using MyPeople.Services.Common.Dtos;

namespace MyPeople.Services.Common.Services;

public interface IQueueService
{
    Task<SQSResponseDto> QueueObjectAsync(SQSRequestDto sqsRequestDto);
}
