using System.Collections.ObjectModel;
using System.Diagnostics;
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
    
    string? _filter = string.Empty;
    
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
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    private async Task CargarReportes()
    {
        AplicandoFiltros();
        
        if (_filter != null)
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes(_filter);
        }
        else
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes();
        }
    }

    private async Task<string> AplicandoFiltros()
    {
        int countFilter = 0;
        
        if (TipoReporteSelected != null)
        {
            countFilter += 1;

            if (countFilter == 1)
            {
                _filter += $"[tipo_reporte_id]={TipoReporteSelected.Id}";
            }
            else
            {
                _filter += $"&filter[tipo_reporte_id]={TipoReporteSelected.Id}";
            }
            
        }
        if (AreaSelected != null)
        {
            countFilter += 1;
            
            if (countFilter == 1)
            {
                _filter += $"area_atiende_id={AreaSelected.Id}";
            }
            else
            {
                _filter += $"&area_atiende_id={AreaSelected.Id}";
            }
        }
        if (MedioSelected != null)
        {
            countFilter += 1;
            
            if (countFilter == 1)
            {
                _filter += $"medio_conocimiento_id={MedioSelected.Id}";
            }
            else
            {
                _filter += $"&medio_conocimiento_id={MedioSelected.Id}";
            }
        }
        if (EstadoSelected != null)
        {
            countFilter += 1;
            
            if (countFilter == 1)
            {
                _filter += $"estado_id={EstadoSelected.Id}";
            }
            else
            {
                _filter += $"&estado_id={EstadoSelected.Id}";
            }
        }
        if (ZonaEstadoSelected != null)
        {
            countFilter += 1;
            
            if (countFilter == 1)
            {
                _filter += $"zona_estado_id={ZonaEstadoSelected.Id}";
            }
            else
            {
                _filter += $"&zona_estado_id={ZonaEstadoSelected.Id}";
            }
        }

        return _filter;
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