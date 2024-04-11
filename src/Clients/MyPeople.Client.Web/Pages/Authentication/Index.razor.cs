using Microsoft.AspNetCore.Components;

namespace MyPeople.Client.Web.Pages.Authentication;

public partial class Index
{
    [Parameter]
    public string? Action { get; set; }
}
