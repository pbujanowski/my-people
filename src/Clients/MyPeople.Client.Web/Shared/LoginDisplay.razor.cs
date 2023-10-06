using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MyPeople.Client.Web.Shared;

public partial class LoginDisplay
{
    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    private void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
}
