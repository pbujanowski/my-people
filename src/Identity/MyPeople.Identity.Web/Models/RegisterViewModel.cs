namespace MyPeople.Identity.Web.Models;

public class RegisterViewModel
{
    public RegisterInputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }
}