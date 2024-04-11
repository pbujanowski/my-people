using MudBlazor;

namespace MyPeople.Client.Web.Shared;

public partial class MainLayout
{
    private bool _isDarkMode;
    private MudThemeProvider _mudThemeProvider = default!;

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
