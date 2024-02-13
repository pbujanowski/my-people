using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MyPeople.Client.Web.Shared;

public partial class RedirectToLogin
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        Navigation.NavigateToLogin("authentication/login");
    }
}