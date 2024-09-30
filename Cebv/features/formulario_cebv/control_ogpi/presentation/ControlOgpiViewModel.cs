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
    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;

    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    /**
     * Constructor de la clase
     */
    public ControlOgpiViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any())
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

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
        if (Desaparecido.Folios is null) return;

        var folio = Desaparecido.Folios;

        await ControlOgpiNetwork.SetFolioFub(folio);
    }

    [RelayCommand]
    private void Guardar()
    {
        _ = AsignarFolio();
        _reporteService.Sync();
    }
}