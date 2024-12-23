namespace MyPeople.Lambdas.Images.Application.Dtos;

public class FunctionResponseDto
{
    public ImageUploadResponseDto? ImageUploadResponse { get; set; }

    public int StatusCode { get; set; }

    public string? Message { get; set; }
}
