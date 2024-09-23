using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Input;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.filtro_busqueda.Domain;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.filtro_busqueda;

public partial class FiltroBusquedaViewModel : ObservableObject
{
    private ISnackbarService _snackbarService;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = [];
    [ObservableProperty] private ReporteResponse _reporteSelected;
    [ObservableProperty] private DesaparecidoResponse _desaparecidoSelected;
    
    //Variable para busqueda de persona por reportante o desaparecido
    [ObservableProperty] private bool? _optionSelected;
    
    [ObservableProperty] private string? _nombreDesaparecido = string.Empty;
    [ObservableProperty] private string? _pseudonimoDesaparecido = string.Empty;
    [ObservableProperty] private string? _nombreReportante = string.Empty;
    [ObservableProperty] private string? _pseudonimoReportante = string.Empty;
    
    // Catalogos
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _medios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _zonasEstados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tipoReporte = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposOcupaciones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estatusEscolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _ocupaciones = new();
    [ObservableProperty] private ObservableCollection<EstatusPersona> _estatusPersonas = new();

    //[ObservableProperty] private ObservableCollection<Catalogo> _complexiones;
    //[ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel;
    //[ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos;
    //[ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello;
    //[ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello;
    //[ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello;

    //[ObservableProperty] private ObservableCollection<Catalogo> _vistas;
    //[ObservableProperty] private ObservableCollection<Catalogo> _tipos;
    //[ObservableProperty] private ObservableCollection<CatalogoColor> _lados;
    //[ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo;
    //[ObservableProperty] private ObservableCollection<Catalogo> _colores;
    //[ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia;
    //[ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias;
    
    // Valores seleccionados
    [ObservableProperty] private Catalogo? _tipoMedioSelected;
    [ObservableProperty] private Catalogo? _medioSelected;
    [ObservableProperty] private Catalogo? _areaSelected;
    [ObservableProperty] private Estado? _estadoSelected;
    [ObservableProperty] private Catalogo? _zonaEstadoSelected;
    [ObservableProperty] private Catalogo? _tipoReporteSelected;
    
    [ObservableProperty] private Estado? _lugarNacimientoReportanteSelected;
    [ObservableProperty] private Catalogo? _escolaridadReportanteSelected;
    [ObservableProperty] private Catalogo? _sexoReportanteSelected;
    [ObservableProperty] private Catalogo? _generoReportanteSelected;
    [ObservableProperty] private Catalogo? _nacionalidadReportanteSelected;
    [ObservableProperty] private Catalogo? _religionReportanteSelected;
    [ObservableProperty] private Catalogo? _lenguaReportanteSelected;
    [ObservableProperty] private Catalogo? _parentescoReportanteSelected;
    [ObservableProperty] private Catalogo? _colectivoReportanteSelected;
    [ObservableProperty] private Catalogo? _estadoConyugalReportanteSelected;
    [ObservableProperty] private DateTime? _fechaNacimientoReportante;
    [ObservableProperty] private Catalogo? _tipoOcupacionReportanteSelected;
    [ObservableProperty] private string? _noTelefonoReportante = string.Empty;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaReportanteSelected;
    [ObservableProperty] private bool? _esMovilReportante;
    [ObservableProperty] private bool? _publicacionRegistroNacional;
    [ObservableProperty] private bool? _publicacionBoletin;
    [ObservableProperty] private string? _descripcionExtorsion = string.Empty;
    [ObservableProperty] private string? _descripcionDondeProviene = string.Empty;
    [ObservableProperty] private bool? _informacionConsentimiento;
    [ObservableProperty] private bool? _informacionExclusivaBusqueda;
    [ObservableProperty] private bool? _informacionRelevante;
    [ObservableProperty] private string? _participacionBusqueda;
    [ObservableProperty] private bool? _denunciaAnonima;
    [ObservableProperty] private string? _curpReportante;
    [ObservableProperty] private string? _rfcReportante;
    [ObservableProperty] private int? _numeroPersonasVive;
    [ObservableProperty] private int? _edadEstimada;
    [ObservableProperty] private Catalogo? _estatusEscolaridadReportanteSelectede;
    
    
    [ObservableProperty] private Estado? _lugarNacimientoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _escolaridadDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _sexoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _generoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _nacionalidadDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _religionDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _lenguaDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _parentescoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _colectivoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _estadoConyugalDesaparecidoSelected;
    [ObservableProperty] private DateTime? _fechaNacimientoDesaparecido;
    [ObservableProperty] private Catalogo? _tipoOcupacionDesaparecidoSelected;
    [ObservableProperty] private string? _noTelefonoDesaparecido = string.Empty;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    [ObservableProperty] private bool? _esMovilDesaparecido;
    [ObservableProperty] private DateTime? _fechaNacimiento;
    [ObservableProperty] private string? _descripcionOcupacionPrincipal = string.Empty;
    [ObservableProperty] private string? _descripcionOcupacionSecundaria = string.Empty;
    [ObservableProperty] private bool? _hablaEspañol;
    [ObservableProperty] private bool? _sabeLeer;
    [ObservableProperty] private bool? _sabeEscribir;
    [ObservableProperty] private string? _uocupacionPrincipal = string.Empty;
    [ObservableProperty] private string? _tipoOcupacion = string.Empty;
    [ObservableProperty] private bool? _accionUrgente;
    [ObservableProperty] private bool? _declaracionEspecialAusencia;
    [ObservableProperty] private bool? _dictamen;
    [ObservableProperty] private bool? _ciNivelFederal;
    [ObservableProperty] private Catalogo? _estatusEscolaridadDesaparecidoSelectede;
    [ObservableProperty] private string? _curpDesaparecido;
    [ObservableProperty] private string? _rfcDesaparecido;
    [ObservableProperty] private string? _otroDerechoHumano = string.Empty;
    [ObservableProperty] private string? _identidadResguardada = string.Empty;
    [ObservableProperty] private string? _clasificacionPersona = string.Empty;
    [ObservableProperty] private string? _nombreParejaConyugal = string.Empty;
    [ObservableProperty] private string? _boletinImgPath = string.Empty;
    [ObservableProperty] private string? _urlBoletin = string.Empty;
    
    [ObservableProperty] private string? _folioCebv = string.Empty;
    [ObservableProperty] private string? _folioFub = string.Empty;
    
    [ObservableProperty] private bool? _victimaExtorsion;
    [ObservableProperty] private bool? _recibioAmenaza;
    
    [ObservableProperty] private DateTime _fechaDesaparicion;
    [ObservableProperty] private DateTime _fechaDesaparicionAproximada;
    [ObservableProperty] private DateTime _fechaDesaparicionCebv;
    [ObservableProperty] private TimeSpan _horaDesaparicion;
    [ObservableProperty] private string? _hechosDesaparicion;
    
    //[ObservableProperty] private Catalogo _vistaSelected;
    //[ObservableProperty] private Catalogo _tipoSelected;
    //[ObservableProperty] private CatalogoColor _regionCuerpoSelected;
    //[ObservableProperty] private CatalogoColor _ladoSelected;
    //[ObservableProperty] private string _colorRegionCuerpo;
    //[ObservableProperty] private string _colorLado;
    //[ObservableProperty] private int _cantidad = 1;
    //[ObservableProperty] private string _descripcion;

    //[ObservableProperty] private Catalogo _grupoPerteneciaSelected;
    //[ObservableProperty] private Pertenencia _perteneciaSelected;
    //[ObservableProperty] private Catalogo _colorSelected;
    //[ObservableProperty] private string _currentMarca;
    //[ObservableProperty] private string _currentPrendaDescripcion;
    
    
    public FiltroBusquedaViewModel()
    {
        CargarCatalogos();
        
        _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    }
    
    private async Task CargarCatalogos()
    {
        Stopwatch sw = new();
        sw.Start();
        Sexos = await CebvNetwork.GetRoute<Catalogo>("sexos");
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas");
        //RazonesCurp = await CebvNetwork.GetRoute<Catalogo>("razones-curp");
        Generos = await CebvNetwork.GetRoute<Catalogo>("generos");
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
        Nacionalidades = await CebvNetwork.GetRoute<Catalogo>("nacionalidades");
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
        //Complexiones = await CebvNetwork.GetRoute("complexiones");
        //ColoresPiel = await CebvNetwork.GetRoute("colores-pieles");
        //ColoresOjos = await CebvNetwork.GetRoute("colores-ojos");
        //ColoresCabello = await CebvNetwork.GetRoute("colores-cabellos");
        //TamanosCabello = await CebvNetwork.GetRoute("tamanos-cabellos");
        //TiposCabello = await CebvNetwork.GetRoute("tipos-cabellos");
        //Vistas = await CebvNetwork.GetRoute("vistas");
        //Tipos = await CebvNetwork.GetRoute("tipos");
        //Colores = await CebvNetwork.GetRoute("colores");
        //GruposPertenencia = await CebvNetwork.GetRoute("grupos-pertenencias");
        //RegionesCuerpo = await SenasParticularesNetwork.GetRouteColor("regiones-cuerpo");
        //Lados = await SenasParticularesNetwork.GetRouteColor("lados");
        ZonasEstados = await CebvNetwork.GetRoute<Catalogo>("zonas-estados");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        TipoReporte = await CebvNetwork.GetRoute<Catalogo>("tipos-reportes");
        TiposMedios = await CebvNetwork.GetRoute<Catalogo>("tipos-medios");
        Escolaridades = await CebvNetwork.GetRoute<Catalogo>("escolaridades");
        Medios = await CebvNetwork.GetRoute<Catalogo>("medios");
        Religiones = await CebvNetwork.GetRoute<Catalogo>("religiones");
        Lenguas = await CebvNetwork.GetRoute<Catalogo>("lenguas");
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
        Colectivos = await CebvNetwork.GetRoute<Catalogo>("colectivos");
        EstadosConyugales = await CebvNetwork.GetRoute<Catalogo>("estados-conyugales");
        TiposOcupaciones = await CebvNetwork.GetRoute<Catalogo>("tipos-ocupaciones");
        EstatusEscolaridades = await CebvNetwork.GetRoute<Catalogo>("estatus-escolaridades");
        Ocupaciones = await CebvNetwork.GetRoute<Catalogo>("ocupaciones");
        EstatusPersonas = await CebvNetwork.GetRoute<EstatusPersona>("estatus-personas");
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    [RelayCommand]
    private async Task OnCargarReportes()
    {
        var filter = await AplicandoFiltros();
        
        if (!string.IsNullOrEmpty(filter))
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes(filter);
        }
        else
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes();
        }
    }
    
    private async Task<string> AplicandoFiltros()
    {
        var filtros = new List<string>();

        if (NombreDesaparecido != null)
        {
            filtros.Add($"[nombreCompleto_desaparecido]={NombreDesaparecido}");
        }
        if (PseudonimoDesaparecido != null)
        {
            filtros.Add($"[pseudonimoCompleto_desaparecido]={PseudonimoDesaparecido}");
        }
        if (NombreReportante != null)
        {
            filtros.Add($"[nombreCompleto_reportante]={NombreReportante}");
        }
        if (PseudonimoReportante != null)
        {
            filtros.Add($"[pseudonimoCompleto_reportante]={PseudonimoReportante}");
        }
        if (TipoReporteSelected != null)
        {
            filtros.Add($"[tipo_reporte_id]={TipoReporteSelected.Id}");
        }
        if (AreaSelected != null)
        {
            filtros.Add($"[area_atiende_id]={AreaSelected.Id}");
        }
        if (MedioSelected != null)
        {
            filtros.Add($"[medio_conocimiento_id]={MedioSelected.Id}");
        }
        if (EstadoSelected != null)
        {
            filtros.Add($"[estado_id]={EstadoSelected.Id}");
        }
        if (ZonaEstadoSelected != null)
        {
            filtros.Add($"[zona_estado_id]={ZonaEstadoSelected.Id}");
        }
        
        return string.Join("&filter", filtros);
    }

    partial void OnTipoReporteSelectedChanged(Catalogo? value)
    {
        Console.WriteLine(value.Id + value.Nombre);
    }

    [RelayCommand]
    public async Task OnReporteClick()
    {
        if (ReporteSelected == null) return;
        var dashboardNavigation = App.Current.Services.GetService<IDashboardNavigationService>();
        if (dashboardNavigation == null) return;

        var reporteService = App.Current.Services.GetService<IReporteService>()!;
        await reporteService.Reload(ReporteSelected.Id);
        reporteService.SetStatusReporte(EstadoReporte.Cargado);
        
        dashboardNavigation.Navigate(typeof(FormularioCebvPage));
    }

    [RelayCommand]
    public async void OnDesaparecidoClick()
    {
        if (DesaparecidoSelected.FolioCebv == null)
        {
            _snackbarService.Show(
                "Folio no asignado.", 
                "Persona desaparecida o no localizada no tiene un folio asignado",
                ControlAppearance.Caution,
                new SymbolIcon(SymbolRegular.Alert32),
                new TimeSpan(0,0, 5));
            return;
        }
        var webview = new WebView2Window($"reportes/informes-inicios/{DesaparecidoSelected.Id}", "Informe de inicio");
        webview.Show();
    }
}