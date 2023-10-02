namespace MyPeople.Identity.Web.Models;

public class RegisterViewModel
{
    public RegisterInputModel Input { get; set; } = new RegisterInputModel();

    public string? ReturnUrl { get; set; }
}
