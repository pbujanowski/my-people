using MyPeople.Common.Models.Dtos;

namespace MyPeople.Services.Images.Application.Queries.GetImageById;

public class GetImageByIdQueryResponse(ImageDto? image)
{
    public ImageDto? Image { get; set; } = image;
}
