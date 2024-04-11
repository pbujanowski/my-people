using MediatR;

namespace MyPeople.Services.Images.Application.Queries.GetImagesByIds;

public class GetImagesByIdsQuery(IEnumerable<Guid> ids) : IRequest<GetImagesByIdsQueryResponse>
{
    public IEnumerable<Guid> Ids { get; set; } = ids;
}
