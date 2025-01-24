namespace MyPeople.Lambdas.Images.Upload.Application.Dtos;

public class FunctionResponseDto
{
    public ICollection<ImageUploadResponseDto> ImageUploadResponse { get; set; } = [];

    public int? StatusCode { get; set; }

    public string? Message { get; set; }
}
