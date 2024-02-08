namespace MyPeople.Services.Posts.Domain.Entities;

public class PostImage : Entity
{
    public Guid? PostId { get; set; }

    public Post? Post { get; set; }

    public Guid? ImageId { get; set; }
}
