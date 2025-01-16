using System.Text.Json;
using MyPeople.Common.Models.Dtos;

namespace MyPeople.Lambdas.Images.Upload.Application.Dtos;

public class FunctionRequestDto
{
    public ImageDto? Image { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
