using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.folio_expediente.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedienteViewModel : ObservableObject
{
    public FolioExpedienteViewModel()
    {
        CargarCatalogos();
        TiposDesapariciones.Add("UNICA", "U");
        TiposDesapariciones.Add("MULTIPLE", "M");
        Estado = _reporteService.UbicacionEstado;
        UbicacionHechos = _reporteService.UbicacionHechos;
    }

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    [ObservableProperty] private Estado? _estado;
    [ObservableProperty] private UbicacionViewModel? _ubicacionHechos;

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes = new();

    [ObservableProperty] private Catalogo _tipoReporte = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private Catalogo _area = new();

    [ObservableProperty] private Dictionary<string, string> _tiposDesapariciones = new();

    [ObservableProperty] private string _tipoDesaparicion = String.Empty;

    partial void OnTipoDesaparicionChanged(string value)
    {
        Console.WriteLine(value);
    }


    /**
     * Peticiones a la APi para cargar los catalagos
     */
    private async void CargarCatalogos()
    {
        TiposReportes = await FolioExpedienteNetwork.GetTiposReportes();
        Areas = await FolioExpedienteNetwork.GetAreas();
        Estado = _reporteService.UbicacionEstado;
    }

    partial void OnTipoReporteChanged(Catalogo value)
    {
        Estado = _reporteService.UbicacionEstado;
    }
}