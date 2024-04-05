using MediatR;
using MyPeople.Common.Abstractions.Services;

namespace MyPeople.Services.Images.Application.Commands.CreateImages;

public class CreateImagesCommandHandler(IImageService imageService)
    : IRequestHandler<CreateImagesCommand, CreateImagesCommandResponse>
{
    private readonly IImageService _imageService = imageService;
    
    public async Task<CreateImagesCommandResponse> Handle(CreateImagesCommand request, CancellationToken cancellationToken)
    {
        var result = await _imageService.CreateImagesAsync(request.Images);
        var response = new CreateImagesCommandResponse(result);
        return response;
    }
}