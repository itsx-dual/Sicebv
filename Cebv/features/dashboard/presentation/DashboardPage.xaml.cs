using System.Windows;
using System.Windows.Controls;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.presentation;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.presentation;

public partial class DashboardPage : Page
{
    private bool _isUserClosedPane;
    private bool _isPaneOpenedOrClosedFromCode;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    
    public DashboardPage()
    {
        InitializeComponent();
        var navigationService = App.Current.Services.GetService<IDashboardNavigationService>()!;
        var snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
        
        navigationService.SetNavigationControl(MainNavigationView);
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }

    private void DashboardPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainNavigationView.Navigate(typeof(Inicio));
    }

    private void DashboardPage_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_isUserClosedPane)
        {
            return;
        }
        
        _isPaneOpenedOrClosedFromCode = true;
        MainNavigationView.SetCurrentValue(NavigationView.IsPaneOpenProperty, e.NewSize.Width > 1200);
        _isPaneOpenedOrClosedFromCode = false;
    }
    
    private void NavigationView_OnPaneOpened(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
        {
            return;
        }

        _isUserClosedPane = false;
    }

    private void NavigationView_OnPaneClosed(NavigationView sender, RoutedEventArgs args)
    {
        if (_isPaneOpenedOrClosedFromCode)
        {
            return;
        }

        _isUserClosedPane = true;
    }

    private void MainNavigationView_OnSelectionChanged(NavigationView sender, RoutedEventArgs args)
    {
        if (MainNavigationView.SelectedItem.TargetPageType == typeof(FormularioCebvPage)) {
            MainNavigationView.SetCurrentValue(NavigationView.IsPaneOpenProperty, false);
        }
    }

    private void NuevoReporte_OnClick(object sender, RoutedEventArgs e)
    {
        _reporteService.ClearReporte();
    }
}