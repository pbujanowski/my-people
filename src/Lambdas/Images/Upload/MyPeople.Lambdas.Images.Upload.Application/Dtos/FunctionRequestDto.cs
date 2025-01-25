namespace MyPeople.Lambdas.Images.Upload.Application.Dtos;

public class FunctionRequestDto
{
    public required IEnumerable<Guid> ImagesIds { get; set; }
}
