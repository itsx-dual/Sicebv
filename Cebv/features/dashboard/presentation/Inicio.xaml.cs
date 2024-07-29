using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Markup;

namespace Cebv.features.dashboard.presentation;

public partial class Inicio : Page
{
    public Inicio()
    {
        InitializeComponent();
    }

    private void Light(object sender, RoutedEventArgs e)
    {
        SetApplicationTheme(ApplicationTheme.Dark);
    }

    private void Dark(object sender, RoutedEventArgs e)
    {
        SetApplicationTheme(ApplicationTheme.Light);
    }
    
    private void SetApplicationTheme(ApplicationTheme theme)
    {
        var app = Application.Current as App;
        app?.SetTheme(theme);
    }
}