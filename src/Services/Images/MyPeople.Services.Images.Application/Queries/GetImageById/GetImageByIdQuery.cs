using MediatR;

namespace MyPeople.Services.Images.Application.Queries.GetImageById;

public class GetImageByIdQuery(Guid id) : IRequest<GetImageByIdQueryResponse>
{
    public Guid Id { get; set; } = id;
}