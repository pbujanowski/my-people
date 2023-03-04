namespace MyPeople.Services.Posts.Application.Dtos;

public class PostDto
{
    public Guid? Id { get; set; }

    public Guid? UserId { get; set; }

    public string? UserDisplayName { get; set; }

    public string? Content { get; set; }
}