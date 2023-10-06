namespace MyPeople.Services.Posts.Application.Dtos;

public abstract class Dto
{
    public Guid? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
