using MediatR;
using MyPeople.Common.Abstractions.Services;

namespace MyPeople.Services.Images.Application.Commands.DeleteImages;

public class DeleteImagesCommandHandler(IImageService imageService)
    : IRequestHandler<DeleteImagesCommand, DeleteImagesCommandResponse>
{
    private readonly IImageService _imageService = imageService;
    
    public async Task<DeleteImagesCommandResponse> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        var result = await _imageService.DeleteImagesAsync(request.Images);
        var response = new DeleteImagesCommandResponse(result);
        return response;
    }
}