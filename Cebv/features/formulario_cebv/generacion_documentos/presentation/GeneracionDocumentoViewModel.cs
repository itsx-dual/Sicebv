using Cebv.app.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.generacion_documentos.presentation;

public partial class GeneracionDocumentoViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    public GeneracionDocumentoViewModel()
    {
        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
    }

    # region Informe de Inicio

    [RelayCommand]
    private void InformeInicio()
    {
        var url = $"reportes/documentos/informes-inicio/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Informe de Inicio");
        webview.Show();
    }

    # endregion

    # region Oficio C4

    [RelayCommand]
    private void OficioC4()
    {
        var url = $"reportes/documentos/oficio-c4/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Oficio C4");
        webview.Show();
    }

    # endregion

    # region Oficio CEI

    [RelayCommand]
    private void OficioCei()
    {
        var url = $"reportes/documentos/oficio-cei/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Oficio CEI");
        webview.Show();
    }

    # endregion

    # region Oficio Fiscalía

    [RelayCommand]
    private void OficioFiscalia()
    {
        var url = $"reportes/documentos/oficio-fiscalia/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Oficio Fiscalía");
        webview.Show();
    }

    # endregion

    # region Oficio SSA

    [RelayCommand]
    private void OficioSsa()
    {
        var url = $"reportes/documentos/oficio-ssa/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Oficio SSA");
        webview.Show();
    }

    # endregion
}