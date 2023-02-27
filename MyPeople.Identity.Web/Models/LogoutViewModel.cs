using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyPeople.Identity.Web.Models;

public class LogoutViewModel
{
    [BindNever]
    public string? RequestId { get; set; }
}