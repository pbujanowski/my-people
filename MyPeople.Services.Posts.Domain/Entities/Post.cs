namespace MyPeople.Services.Posts.Domain.Entities;

public class Post : Entity
{
    public Guid? UserId { get; set; }

    public string? Content { get; set; }
}
