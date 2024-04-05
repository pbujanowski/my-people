using MediatR;
using MyPeople.Common.Abstractions.Services;

namespace MyPeople.Services.Images.Application.Queries.GetImagesByIds;

public class GetImagesByIdsQueryHandler(IImageService imageService)
    : IRequestHandler<GetImagesByIdsQuery, GetImagesByIdsQueryResponse>
{
    private readonly IImageService _imageService = imageService;
    
    public async Task<GetImagesByIdsQueryResponse> Handle(GetImagesByIdsQuery request, CancellationToken cancellationToken)
    {
        var result = await _imageService.GetImagesByIdsAsync(request.Ids);
        var response = new GetImagesByIdsQueryResponse(result);
        return response;
    }
}