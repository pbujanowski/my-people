using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Commands.CreateImages;

public class CreateImagesCommandResponse(IEnumerable<ImageDto>? images)
{
    public IEnumerable<ImageDto>? Images { get; set; } = images;
}