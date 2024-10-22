using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Cebv.features.settings;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }
    
}