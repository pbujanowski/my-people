namespace MyPeople.Identity.Web.Models;

public class LoginViewModel
{
    public LoginInputModel Input { get; set; } = new LoginInputModel();

    public string? ReturnUrl { get; set; }
}
