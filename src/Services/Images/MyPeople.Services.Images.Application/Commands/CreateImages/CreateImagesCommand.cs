using MediatR;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Commands.CreateImages;

public class CreateImagesCommand(IEnumerable<CreateImageDto> images) : IRequest<CreateImagesCommandResponse>
{
    public IEnumerable<CreateImageDto> Images { get; set; } = images;
}