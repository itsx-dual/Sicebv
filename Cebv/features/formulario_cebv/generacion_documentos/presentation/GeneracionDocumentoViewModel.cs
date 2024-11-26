using System.Collections.ObjectModel;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.util.enums.FactorRhesus;

namespace Cebv.features.formulario_cebv.generacion_documentos.presentation;

public partial class GeneracionDocumentoViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    [ObservableProperty]
    private ObservableCollection<String> _resultadosRdn = new() { Positivo, Negativo, NoEspecifica };

    [ObservableProperty] private ObservableCollection<Catalogo> _mediosDifusion = new();

    public GeneracionDocumentoViewModel()
    {
        InitAsync();
        
        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Reporte.HechosDesaparicion ??= new();
        Reporte.GeneracionDocumento ??= new();
    }

    private async void InitAsync()
    {
        MediosDifusion = await CebvNetwork.GetRoute<Catalogo>("medios-difusion");
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


    # region Ficha de Datos

    [RelayCommand]
    private void FichaDatos()
    {
        var url = $"reportes/documentos/ficha-datos/{Desaparecido.Id}";

        var webview = new WebView2Window(url, "Ficha de Datos");
        webview.Show();
    }

    # endregion

    [RelayCommand]
    private async Task OnGuardar()
    {
        await _reporteService.Sync();
    }
}