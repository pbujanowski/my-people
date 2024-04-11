using MediatR;
using MyPeople.Common.Abstractions.Services;

namespace MyPeople.Services.Images.Application.Queries.GetImageById;

public class GetImageByIdQueryHandler(IImageService imageService)
    : IRequestHandler<GetImageByIdQuery, GetImageByIdQueryResponse>
{
    private readonly IImageService _imageService = imageService;

    public async Task<GetImageByIdQueryResponse> Handle(
        GetImageByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _imageService.GetImageByIdAsync(request.Id);
        var response = new GetImageByIdQueryResponse(result);
        return response;
    }
}
