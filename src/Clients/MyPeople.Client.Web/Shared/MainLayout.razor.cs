using MudBlazor;

namespace MyPeople.Client.Web.Shared;

public partial class MainLayout
{
    private bool _isDarkMode;

#pragma warning disable IDE0044 // Add readonly modifier
    private MudThemeProvider _mudThemeProvider = default!;
#pragma warning restore IDE0044 // Add readonly modifier

    private string GetThemeModeIcon()
    {
        return _isDarkMode ? Icons.Material.Filled.DarkMode : Icons.Material.Filled.LightMode;
    }

    private void SwitchThemeMode()
    {
        _isDarkMode = !_isDarkMode;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _isDarkMode = await _mudThemeProvider.GetSystemPreference();
            StateHasChanged();
        }
    }
}
