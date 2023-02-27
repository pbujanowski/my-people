namespace MyPeople.Services.Posts.Application.Dtos;

public class PostDto
{
    public Guid? Id { get; set; }

    public string? UserId { get; set; }

    public string? Content { get; set; }
}