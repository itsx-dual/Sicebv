using System.Collections.ObjectModel;
using System.Diagnostics;
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
    
    // Catalogos y valores predefinidos
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _razonesCurp = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _zonasEstados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello;

    [ObservableProperty] private ObservableCollection<Catalogo> _vistas;
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo;
    [ObservableProperty] private ObservableCollection<Catalogo> _colores;
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia;
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias;

    public FiltroBusquedaViewModel()
    {
        CargarReportes();
        _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    }
    
    private async Task CargarCatalogos()
    {
        Stopwatch sw = new();
        sw.Start();
        Sexos = await CargarCatalogo.GetCatalogo("sexos");
        Areas = await CargarCatalogo.GetCatalogo("areas");
        RazonesCurp = await CargarCatalogo.GetCatalogo("razones-curp");
        Generos = await CargarCatalogo.GetCatalogo("generos");
        Parentescos = await CargarCatalogo.GetCatalogo("parentescos");
        Nacionalidades = await CargarCatalogo.GetCatalogo("nacionalidades");
        CompaniasTelefonicas = await CargarCatalogo.GetCatalogo("companias-telefonicas");
        Complexiones = await CargarCatalogo.GetCatalogo("complexiones");
        ColoresPiel = await CargarCatalogo.GetCatalogo("colores-pieles");
        ColoresOjos = await CargarCatalogo.GetCatalogo("colores-ojos");
        ColoresCabello = await CargarCatalogo.GetCatalogo("colores-cabellos");
        TamanosCabello = await CargarCatalogo.GetCatalogo("tamanos-cabellos");
        TiposCabello = await CargarCatalogo.GetCatalogo("tipos-cabellos");
        Vistas = await CargarCatalogo.GetCatalogo("vistas");
        Tipos = await CargarCatalogo.GetCatalogo("tipos");
        Colores = await CargarCatalogo.GetCatalogo("colores");
        GruposPertenencia = await CargarCatalogo.GetCatalogo("grupos-pertenencias");
        RegionesCuerpo = await SenasParticularesNetwork.GetCatalogoColor("regiones-cuerpo");
        Lados = await SenasParticularesNetwork.GetCatalogoColor("lados");
        ZonasEstados = await CargarCatalogo.GetCatalogo("zonas-estados");
        Estados = await ReportanteNetwork.GetEstados();
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    private async void CargarReportes()
    {
        //Reportes = await FiltroBusquedaNetwork.GetReportes();
    }

    [RelayCommand]
    public async void OnReporteClick()
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