namespace MyPeople.Services.Posts.Application.Dtos;

public class PostDto : Dto
{
    public Guid? UserId { get; set; }

    public string? UserDisplayName { get; set; }

    public string? Content { get; set; }
}
