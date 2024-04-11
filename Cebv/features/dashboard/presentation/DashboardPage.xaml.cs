using System.Windows;
using System.Windows.Controls;

namespace Cebv.features.dashboard.presentation;

public partial class DashboardPage : Page
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private void DashboardPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainNavigationView.Navigate(typeof(Inicio));
    }
}