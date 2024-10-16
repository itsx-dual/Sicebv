using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Appearance;

namespace Cebv.features.settings;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private Dictionary<ApplicationTheme, string> _items = new()
    {
        { ApplicationTheme.Light, "Claro" },
        { ApplicationTheme.Dark, "Oscuro" },
        { ApplicationTheme.HighContrast, "Alto Contraste" }
    };
    
    [ObservableProperty] private ApplicationTheme _appTheme = ApplicationThemeManager.GetAppTheme();
    
    partial void OnAppThemeChanged(ApplicationTheme theme)
    {
        ApplicationThemeManager.Apply(theme);
    }
}