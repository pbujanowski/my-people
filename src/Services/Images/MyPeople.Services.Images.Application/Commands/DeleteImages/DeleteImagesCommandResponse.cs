using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Commands.DeleteImages;

public class DeleteImagesCommandResponse(IEnumerable<ImageDto>? images)
{
    public IEnumerable<ImageDto>? Images { get; set; } = images;
}