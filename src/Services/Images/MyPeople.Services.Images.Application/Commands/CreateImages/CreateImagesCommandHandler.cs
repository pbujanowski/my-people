using MediatR;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Services.Images.Application.Services;

namespace MyPeople.Services.Images.Application.Commands.CreateImages;

public class CreateImagesCommandHandler(
    IImageService imageService,
    IImageQueueService imageQueueService
) : IRequestHandler<CreateImagesCommand, CreateImagesCommandResponse>
{
    public async Task<CreateImagesCommandResponse> Handle(
        CreateImagesCommand request,
        CancellationToken cancellationToken
    )
    {
        var createImagesResult = await imageService.CreateImagesAsync(request.Images);
        if (createImagesResult is not null)
        {
            // TODO: it has to be changed to something more reliable, like Hangfire.
            _ = Task.Run(() => imageQueueService.QueueImagesAsync(createImagesResult));
        }

        var response = new CreateImagesCommandResponse(createImagesResult);
        return response;
    }
}
