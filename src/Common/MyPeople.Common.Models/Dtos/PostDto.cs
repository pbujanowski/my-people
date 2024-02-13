namespace MyPeople.Common.Models.Dtos;

public class PostDto
{
    public Guid? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UserId { get; set; }

    public string? UserDisplayName { get; set; }

    public string? Content { get; set; }

    public ICollection<PostImageDto>? Images { get; set; }
}