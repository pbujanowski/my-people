namespace MyPeople.Services.Images.Domain.Entities;

public abstract class Entity
{
    public Guid? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}