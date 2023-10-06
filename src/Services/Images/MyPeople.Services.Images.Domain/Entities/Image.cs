namespace MyPeople.Services.Images.Domain.Entities;

public class Image : Entity
{
    public string? Name { get; set; }

    public string? ContentType { get; set; }

    public string? Content { get; set; }
}
