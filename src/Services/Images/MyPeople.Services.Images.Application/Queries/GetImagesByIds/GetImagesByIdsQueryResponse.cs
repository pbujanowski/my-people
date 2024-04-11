using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Queries.GetImagesByIds;

public class GetImagesByIdsQueryResponse(IEnumerable<ImageDto>? images)
{
    public IEnumerable<ImageDto>? Images { get; set; } = images;
}
