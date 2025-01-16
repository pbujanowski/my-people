using System.Text.Json;

namespace MyPeople.Lambdas.Images.Upload.Application.Dtos;

public class FunctionResponseDto
{
    public ImageUploadResponseDto? ImageUploadResponse { get; set; }

    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
