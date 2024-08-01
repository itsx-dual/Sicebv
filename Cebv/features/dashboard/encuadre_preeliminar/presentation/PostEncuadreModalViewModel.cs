using System.Collections.ObjectModel;
using Cebv.app.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.encuadre_preeliminar.domain;
using Cebv.features.formulario_cebv.folio_expediente.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class PostEncuadreModalViewModel : ObservableObject
{
    
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private static IDashboardNavigationService _navigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    [ObservableProperty] private Desaparecido _desaparecido;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes;
    [ObservableProperty] private ObservableCollection<Catalogo> _areas;
    [ObservableProperty] private Dictionary<string, string> _tiposDesapariciones;

    public PostEncuadreModalViewModel()
    {
        InitAsync();
    }

    private async Task CargarCatalogos()
    {
        TiposReportes = await EncuadrePreeliminarNetwork.GetCatalogo("tipos-reportes");
        Areas = await EncuadrePreeliminarNetwork.GetCatalogo("areas");
        TiposDesapariciones = new Dictionary<string, string>
        {
            {"Unica", "U"},
            {"Multiple", "M"}
        };
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        Reporte = _reporteService.GetReporte();
        Reportante = Reporte.Reportantes.FirstOrDefault()!;
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
    }

    [RelayCommand]
    private void OnGenerarBoletinBusquedaInmediata()
    {
        if (Desaparecido.Id == null || Desaparecido.Id < 1) return;
        var webview = new WebView2Window($"reportes/boletines/{Desaparecido.Id}", "Boletin de busqueda inmediata");
        webview.Show();
    }
    
    [RelayCommand]
    private void OnGenerarFichaDeDatos()
    {
        if (Desaparecido.Id == null || Desaparecido.Id < 1) return;
        var webview = new WebView2Window($"reportes/reportes-preliminares/{Desaparecido.Id}", "Ficha de datos resumida");
        webview.Show();
    }
    
    [RelayCommand]
    private async void SetFolio()
    {
        await _reporteService.Sync();
        
        if (await _reporteService.SetFolios())
        {
            await _reporteService.Sync();
            Reporte = _reporteService.GetReporte();
            Reportante = Reporte.Reportantes.FirstOrDefault()!;
            Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
            return;
        }
        _snackBarService.Show(
            "Error fatal",
            "No se pudo asignar el folio",
            ControlAppearance.Danger,
            new SymbolIcon(SymbolRegular.Warning32),
            new TimeSpan(0, 0, 5));
    }

    [RelayCommand]
    private void GetInformeInicio()
    {
        if (Desaparecido.Id == null || Desaparecido.Id < 1) return;
        var webview = new WebView2Window($"reportes/informes-inicios/{Desaparecido.Id}", "Informe de inicio");
        webview.Show();
    }
}