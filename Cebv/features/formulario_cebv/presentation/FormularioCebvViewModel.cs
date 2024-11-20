using Cebv.core.domain;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvViewModel : ObservableObject
{
    [ObservableProperty] private string _nombreCompleto = string.Empty;
    [ObservableProperty] private bool _puedeGuardar;

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private ISnackbarService _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    [ObservableProperty] private Reporte _reporte;

    /**
     * Reportante
     */
    //[ObservableProperty] private ReportanteRequest _reportante = new();
    public FormularioCebvViewModel()
    {
        Reporte = _reporteService.GetReporte();
    }

    [RelayCommand]
    private async Task OnGenerarFolio()
    {
        var id = _reporteService.GetReporteId();
        var client = CebvClientHandler.SharedClient;
        using var response = await client.GetAsync($"/api/reportes/asignar_folio/{id}");

        if (response.IsSuccessStatusCode)
        {
            _snackbarService.Show(
                "Folio/s asignado correctamente.",
                "",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.Alert32),
                new TimeSpan(0, 0, 5));
        }
        else
        {
            _snackbarService.Show(
                "Folio no asignado.",
                "Persona desaparecida o no localizada no tiene un folio asignado",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.ErrorCircle24),
                new TimeSpan(0, 0, 5));
        }
    }
}