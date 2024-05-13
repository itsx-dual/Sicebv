using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.reporte.presentation;

public partial class ReporteViewModel : ObservableObject
{
    [ObservableProperty] private ReportesQueryResponse? _reportes;

    [ObservableProperty] private ObservableCollection<Catalogo>? _tiposReportes;
    [ObservableProperty] private Catalogo? _tipoReporteSeleccionado;

    [ObservableProperty] private ObservableCollection<Catalogo>? _areas;
    [ObservableProperty] private Catalogo? _areaSeleccionada;

    [ObservableProperty] private ObservableCollection<Catalogo>? _tiposMedios;
    [ObservableProperty] private Catalogo? _tipoMedioSeleccionado;

    [ObservableProperty] private ObservableCollection<Medio>? _medios;
    [ObservableProperty] private Medio? _medioSeleccionado;

    [ObservableProperty] private ObservableCollection<Estado>? _estados;
    [ObservableProperty] private Estado? _estadoSeleccionado;

    [ObservableProperty] private ObservableCollection<ZonaEstado>? _zonasEstados;
    [ObservableProperty] private ZonaEstado? _zonaEstadoSeleccionada;

    [ObservableProperty] private ObservableCollection<TipoHipotesis>? _tiposHipotesis;
    [ObservableProperty] private TipoHipotesis? _tipoHipotesisSeleccionada;

    [ObservableProperty] private Dictionary<string, string>? _tiposDesapariciones;
    [ObservableProperty] private KeyValuePair<string, string> _tipoDesaparicionSeleccionada;

    [ObservableProperty] private string? _fechaDesaparicion;
    [ObservableProperty] private string? _sintesisLocalizacion;
    [ObservableProperty] private string? _clasificacionPersona;


    public ReporteViewModel()
    {
        CargarCatalogos();
    }


    private async void CargarCatalogos()
    {
        TiposReportes = (ObservableCollection<Catalogo>)await ReporteNetwork.GetTiposReportes();
        Areas = (ObservableCollection<Catalogo>)await ReporteNetwork.GetAreas();
        TiposMedios = (ObservableCollection<Catalogo>)await ReporteNetwork.GetTiposMedios();
        Medios = (ObservableCollection<Medio>)await ReporteNetwork.GetMedios();
        Estados = (ObservableCollection<Estado>)await ReporteNetwork.GetEstados();
        ZonasEstados = (ObservableCollection<ZonaEstado>)await ReporteNetwork.GetZonasEstados();
        TiposHipotesis = (ObservableCollection<TipoHipotesis>)await ReporteNetwork.GetTiposHipotesis();
        TiposDesapariciones = (Dictionary<string, string>)await ReporteNetwork.GetTiposDesapariciones();
    }
}