namespace MyPeople.Common.Models.Dtos;

public class CreateImageDto
{
    public string? Name { get; set; }

    public string? Content { get; set; }

    public string? ContentType { get; set; }

    public Guid? PostId { get; set; }
}