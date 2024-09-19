using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Cebv.app.presentation;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.filtro_busqueda.Domain;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.formulario_cebv.presentation;
using Cebv.features.formulario_cebv.senas_particulares.domain;
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
    
    // Catalogos
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _medios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _zonasEstados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tipoReporte = new();
    
    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
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
    [ObservableProperty] private Catalogo _tipoMedioSelected;
    [ObservableProperty] private Catalogo _medioSelected;
    [ObservableProperty] private Catalogo _areaSelected;
    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Catalogo _zonaEstadoSelected;
    [ObservableProperty] private Catalogo _tipoReporteSelected;
    
    [ObservableProperty] private Estado _lugarNacimientoReportanteSelected;
    [ObservableProperty] private Catalogo _escolaridadReportanteSelected;
    [ObservableProperty] private Catalogo _sexoReportanteSelected;
    [ObservableProperty] private Catalogo _generoReportanteSelected;
    [ObservableProperty] private Catalogo _nacionalidadReportanteSelected;
    [ObservableProperty] private Catalogo _religionReportanteSelected;
    [ObservableProperty] private Catalogo _lenguaReportanteSelected;
    [ObservableProperty] private Catalogo _parentescoReportanteSelected;
    [ObservableProperty] private Catalogo _colectivoReportanteSelected;
    [ObservableProperty] private Catalogo _estadoConyugalReportanteSelected;
    [ObservableProperty] private DateTime? _fechaNacimientoReportante;
    [ObservableProperty] private Catalogo _tipoOcupacionReportanteSelected;
    [ObservableProperty] private string _noTelefonoReportante = string.Empty;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaReportanteSelected;
    
    [ObservableProperty] private Estado _lugarNacimientoDesaparecidoSelected;
    [ObservableProperty] private Catalogo _escolaridadDesaparecidoSelected;
    [ObservableProperty] private Catalogo _sexoDesaparecidoSelected;
    [ObservableProperty] private Catalogo _generoDesaparecidoSelected;
    [ObservableProperty] private Catalogo _nacionalidadDesaparecidoSelected;
    [ObservableProperty] private Catalogo _religionDesaparecidoSelected;
    [ObservableProperty] private Catalogo _lenguaDesaparecidoSelected;
    [ObservableProperty] private Catalogo _parentescoDesaparecidoSelected;
    [ObservableProperty] private Catalogo _colectivoDesaparecidoSelected;
    [ObservableProperty] private Catalogo _estadoConyugalDesaparecidoSelected;
    [ObservableProperty] private DateTime? _fechaNacimientoDesaparecido;
    [ObservableProperty] private Catalogo _tipoOcupacionDesaparecidoSelected;
    [ObservableProperty] private string _noTelefonoDesaparecido = string.Empty;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    
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

    //[ObservableProperty] private DateTime _fechaNacimientoDesaparecido = DateTime.Today;
    //[ObservableProperty] private DateTime _fechaDesaparicion = DateTime.Today;
    //[ObservableProperty] private TimeSpan _horaDesaparicion;
    //[ObservableProperty] private int _anosDesaparecido;
    //[ObservableProperty] private int _mesesDesaparecido;
    //[ObservableProperty] private int _diasDesaparecido;
    //[ObservableProperty] private ObservableCollection<string> _files = new();
    //[ObservableProperty] private string _curp;

    public FiltroBusquedaViewModel()
    {
        CargarCatalogos();
        CargarReportes();
        
        _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    }
    
    private async Task CargarCatalogos()
    {
        Stopwatch sw = new();
        sw.Start();
        Sexos = await CatalogosNetwork.GetCatalogo("sexos");
        Areas = await CatalogosNetwork.GetCatalogo("areas");
        //RazonesCurp = await CatalogosNetwork.GetCatalogo("razones-curp");
        Generos = await CatalogosNetwork.GetCatalogo("generos");
        Parentescos = await CatalogosNetwork.GetCatalogo("parentescos");
        Nacionalidades = await CatalogosNetwork.GetCatalogo("nacionalidades");
        CompaniasTelefonicas = await CatalogosNetwork.GetCatalogo("companias-telefonicas");
        //Complexiones = await CatalogosNetwork.GetCatalogo("complexiones");
        //ColoresPiel = await CatalogosNetwork.GetCatalogo("colores-pieles");
        //ColoresOjos = await CatalogosNetwork.GetCatalogo("colores-ojos");
        //ColoresCabello = await CatalogosNetwork.GetCatalogo("colores-cabellos");
        //TamanosCabello = await CatalogosNetwork.GetCatalogo("tamanos-cabellos");
        //TiposCabello = await CatalogosNetwork.GetCatalogo("tipos-cabellos");
        //Vistas = await CatalogosNetwork.GetCatalogo("vistas");
        //Tipos = await CatalogosNetwork.GetCatalogo("tipos");
        //Colores = await CatalogosNetwork.GetCatalogo("colores");
        //GruposPertenencia = await CatalogosNetwork.GetCatalogo("grupos-pertenencias");
        //RegionesCuerpo = await SenasParticularesNetwork.GetCatalogoColor("regiones-cuerpo");
        //Lados = await SenasParticularesNetwork.GetCatalogoColor("lados");
        ZonasEstados = await CatalogosNetwork.GetCatalogo("zonas-estados");
        Estados = await ReportanteNetwork.GetEstados();
        LugaresNacimientos = await ReportanteNetwork.GetEstados();
        TipoReporte = await CatalogosNetwork.GetCatalogo("tipos-reportes");
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
        Escolaridades = await CatalogosNetwork.GetCatalogo("escolaridades");
        Medios = await CatalogosNetwork.GetCatalogo("medios");
        Religiones = await CatalogosNetwork.GetCatalogo("religiones");
        Lenguas = await CatalogosNetwork.GetCatalogo("lenguas");
        Parentescos = await CatalogosNetwork.GetCatalogo("parentescos");
        Colectivos = await CatalogosNetwork.GetCatalogo("colectivos");
        EstadosConyugales = await CatalogosNetwork.GetCatalogo("estados-conyugales");
        TiposOcupaciones = await CatalogosNetwork.GetCatalogo("tipos-ocupaciones");
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    private async Task CargarReportes()
    {
        /*if (filters == false)
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes();
        }
        else
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes(filter);
        }*/
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