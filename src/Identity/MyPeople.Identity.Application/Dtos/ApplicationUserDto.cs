namespace MyPeople.Identity.Application.Dtos;

public class ApplicationUserDto
{
    public Guid? Id { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }
}
