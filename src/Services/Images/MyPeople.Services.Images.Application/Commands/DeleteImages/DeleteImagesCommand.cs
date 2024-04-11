using MediatR;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Commands.DeleteImages;

public class DeleteImagesCommand(IEnumerable<DeleteImageDto> images)
    : IRequest<DeleteImagesCommandResponse>
{
    public IEnumerable<DeleteImageDto> Images { get; set; } = images;
}
