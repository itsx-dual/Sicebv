using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.reportante.data;
using Cebv.features.reportante.domain;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.reporte.presentation;

public partial class ReporteViewModel : ObservableObject
{
    [ObservableProperty] private ReporteWrapped _reportes;

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes;
    [ObservableProperty] private Catalogo _tipoReporteSeleccionado;

    [ObservableProperty] private ObservableCollection<Catalogo> _areas;
    [ObservableProperty] private Catalogo _areaSeleccionada;

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios;
    [ObservableProperty] private Catalogo _tipoMedioSeleccionado;

    [ObservableProperty] private ObservableCollection<Medio> _medios;
    [ObservableProperty] private Medio _medioSeleccionado;

    [ObservableProperty] private ObservableCollection<Estado> _estados;
    [ObservableProperty] private Estado _estadoSeleccionado;

    [ObservableProperty] private ObservableCollection<ZonaEstado> _zonasEstados;
    [ObservableProperty] private ZonaEstado _zonaEstadoSeleccionada;

    [ObservableProperty] private ObservableCollection<TipoHipotesis> _tiposHipotesis;
    [ObservableProperty] private TipoHipotesis _tipoHipotesisSeleccionada;

    [ObservableProperty] private Dictionary<string, string> _tiposDesapariciones;
    [ObservableProperty] private KeyValuePair<string, string> _tipoDesaparicionSeleccionada;

    [ObservableProperty] private string _fechaDesaparicion;
    [ObservableProperty] private string _sintesisLocalizacion;
    [ObservableProperty] private string _clasificacionPersona;

    [ObservableProperty] private Reporte _reporteActual;

    public ReporteViewModel()
    {
        CargarCatalogos();
        CatalogosReportante();
    }

    [RelayCommand]
    private async Task CrearReporte()
    {
        ReporteActual = (Reporte)await ReporteNetwork
            .PostReporte(
                TipoReporteSeleccionado.Id,
                TipoDesaparicionSeleccionada.Key,
                AreaSeleccionada.Id,
                MedioSeleccionado.Id,
                ZonaEstadoSeleccionada.Id,
                TipoHipotesisSeleccionada.Id,
                FechaDesaparicion,
                SintesisLocalizacion,
                ClasificacionPersona);
    }

    private async void CargarCatalogos()
    {
        Reportes = await ReporteNetwork.GetReportes();
        TiposReportes = (ObservableCollection<Catalogo>)await ReporteNetwork.GetTiposReportes();
        Areas = (ObservableCollection<Catalogo>)await ReporteNetwork.GetAreas();
        TiposMedios = (ObservableCollection<Catalogo>)await ReporteNetwork.GetTiposMedios();
        Medios = (ObservableCollection<Medio>)await ReporteNetwork.GetMedios();
        Estados = (ObservableCollection<Estado>)await ReporteNetwork.GetEstados();
        ZonasEstados = (ObservableCollection<ZonaEstado>)await ReporteNetwork.GetZonasEstados();
        TiposHipotesis = (ObservableCollection<TipoHipotesis>)await ReporteNetwork.GetTiposHipotesis();
        TiposDesapariciones = (Dictionary<string, string>)await ReporteNetwork.GetTiposDesapariciones();
    }

    //
    // ViewModel de Reportante
    //
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos;
    [ObservableProperty] private Catalogo _parentescoSeleccionado;

    [ObservableProperty] private bool _denunciaAnonimaSeleccionada;

    [ObservableProperty] private bool _informacionConsentimientoSeleccionada;

    [ObservableProperty] private bool _informacionExclusivaBusquedaSeleccionada;

    [ObservableProperty] private bool _publicacionRegistroNacionalSeleccionada;

    [ObservableProperty] private bool _publicacionBoletinSeleccionada;

    [ObservableProperty] private bool _pertenenciaColectivoSeleccionado;

    [ObservableProperty] private string _nombreColectivo;
    [ObservableProperty] private string _informacionRelevante;

    private async void CatalogosReportante()
    {
        Parentescos = (ObservableCollection<Catalogo>)await ReportanteNetwork.GetParentescos();
    }

    [RelayCommand]
    private async Task CrearReportante()
    {
        await ReportanteNetwork
            .PostReportante(
                1,
                1,
                ParentescoSeleccionado.Id,
                DenunciaAnonimaSeleccionada,
                InformacionConsentimientoSeleccionada,
                InformacionExclusivaBusquedaSeleccionada,
                PublicacionRegistroNacionalSeleccionada,
                PublicacionBoletinSeleccionada,
                PertenenciaColectivoSeleccionado,
                NombreColectivo,
                InformacionRelevante
            );
    }

}