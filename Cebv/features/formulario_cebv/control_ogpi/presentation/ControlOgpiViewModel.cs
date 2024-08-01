using System.Collections.ObjectModel;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.control_ogpi.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.control_ogpi.presentation;

public partial class ControlOgpiViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reporte;

    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    /**
     * Constructor de la clase
     */
    public ControlOgpiViewModel()
    {
        LoadAsync();
    }

    private async Task LoadAsync()
    {
        Reporte = _reporteService.GetReporte();
        await CargarCatalogos();
    }

    [ObservableProperty] private ObservableCollection<EstatusPersona> _estatusPersonas = new();

    /**
     * Método que carga los catálogos
     */
    private async Task CargarCatalogos() =>
        EstatusPersonas = await ControlOgpiNetwork.GetEstatusPersonas();

    [RelayCommand]
    private async Task AsignarFolio()
    {
        if (Reporte.Folios is null ||
            Reporte.Folios.Count <= 0) return;

        var folio = Reporte.Folios[0];

        await ControlOgpiNetwork.SetFolioFub(folio);
    }

    [RelayCommand]
    private void Guardar()
    {
        _ = AsignarFolio();
        _reporteService.Sync();
    }
}