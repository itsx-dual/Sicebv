using System.Collections.ObjectModel;
using System.Web;
using Cebv.app.presentation;
using Cebv.core.domain;
using static Cebv.core.util.enums.TipoDesaparicion;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class PostEncuadreModalViewModel : ObservableObject
{
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;

    [ObservableProperty] private ObservableCollection<BasicResource> _tiposReportes = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();

    [ObservableProperty] private Dictionary<string, string> _tiposDesapariciones =
        new() { { Unica, U }, { Multiple, M } };

    [ObservableProperty] private string? _senasParticulares;

    public PostEncuadreModalViewModel()
    {
        InitAsync();
    }

    private async Task CargarCatalogos()
    {
        TiposReportes = await CebvNetwork.GetRoute<BasicResource>("tipos-reportes");
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas");
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        GetReporteFromService();
    }

    private void GetReporteFromService()
    {
        Reporte = _reporteService.GetReporte();

        if (Reporte.Reportantes.Any())
        {
            Reportante = Reporte.Reportantes.First();
        }
        else
        {
            Reportante = new Reportante();
            Reporte.Reportantes.Add(Reportante);
        }

        if (Reporte.Desaparecidos.Any())
        {
            Desaparecido = Reporte.Desaparecidos.First();
        }
        else
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }
    }

    [RelayCommand]
    private void OnGenerarBoletinBusquedaInmediata()
    {
        if (Desaparecido.Id is null or < 1) return;
        var webview =
            new  WebView2Window($"reportes/boletines/busqueda-inmediata/{Desaparecido.Id}", "Boletin de busqueda inmediata");
        webview.Show();
    }

    [RelayCommand]
    private void OnGenerarFichaDeDatos()
    {
        if (Desaparecido.Id is null or < 1) return;
        var webview =
            new WebView2Window($"reportes/reportes-preliminares/{Desaparecido.Id}", "Ficha de datos resumida");
        webview.Show();
    }

    [RelayCommand]
    private void GetInformeInicio()
    {
        if (Desaparecido.Id is null or < 1) return;
        var webview = new WebView2Window($"reportes/boletines/busqueda-inmediata/{Desaparecido.Id}", "Informe de inicio");
        webview.Show();
    }

    [RelayCommand]
    private async Task SetFolio()
    {
        await _reporteService.Sync();

        if (await _reporteService.SetFolios())
        {
            await _reporteService.Sync();
            GetReporteFromService();
            return;
        }

        _snackBarService.Show(
            "Error fatal",
            "No se pudo asignar el folio",
            ControlAppearance.Danger,
            new SymbolIcon(SymbolRegular.Warning32),
            new TimeSpan(0, 0, 5));
    }
}