using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.filtro_busqueda.Data;
using Cebv.features.dashboard.filtro_busqueda.Domain;
using Cebv.features.formulario_cebv.presentation;
using Cebv.features.formulario_cebv.senas_particulares.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.dashboard.filtro_busqueda.Presentation;

public partial class FiltroBusquedaViewModel : ObservableObject
{
    private FilterDictionary filterDictionary;
    private ISnackbarService _snackbarService;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Opciones;
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = [];
    [ObservableProperty] private ReporteResponse _reporteSelected;
    [ObservableProperty] private DesaparecidoResponse _desaparecidoSelected;
    
    //Variable para busqueda de persona por reportante o desaparecido
    [ObservableProperty] private string? _nombreCompletoDesaparecido = string.Empty;
    [ObservableProperty] private string? _pseudonimoCompletoDesaparecido = string.Empty;
    [ObservableProperty] private string? _nombreCompletoReportante = string.Empty;
    [ObservableProperty] private string? _pseudonimoCompletoReportante = string.Empty;
    [ObservableProperty] private bool? _buscarPorReportante;
    [ObservableProperty] private bool? _buscarPorDesaparecido;
    [ObservableProperty] private string? _nombre;
    
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
    
    [ObservableProperty] private ObservableCollection<Catalogo> _hipotesisOficial = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _hipotesisOficialCircunstancia = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _regionesCuerpo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _vistas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _relaciones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _marcas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposVehiculos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _usosVehiculos = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia = new();
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();

    //Valroes de los filtros
    [ObservableProperty] private Catalogo? _tipoMedioSelected;
    [ObservableProperty] private Catalogo? _medioSelected;
    [ObservableProperty] private Catalogo? _areaSelected;
    [ObservableProperty] private Estado? _estadoSelected;
    [ObservableProperty] private Catalogo? _zonaEstadoSelected;
    [ObservableProperty] private Catalogo? _tipoReporteSelected;
    
    [ObservableProperty] private string? _nombreReportante;
    [ObservableProperty] private string? _apellidoPaternoReportante;
    [ObservableProperty] private string? _apellidoMaternoReportante;
    [ObservableProperty] private string? _pseudonimoNombreReportante;
    [ObservableProperty] private string? _pseudonimoApellidoPaternoReportante;
    [ObservableProperty] private string? _pseudonimoApellidoMaternoReportante;
    [ObservableProperty] private Estado? _lugarNacimientoReportanteSelected;
    [ObservableProperty] private Catalogo? _escolaridadReportanteSelected;
    [ObservableProperty] private Catalogo? _sexoReportanteSelected;
    [ObservableProperty] private Catalogo? _generoReportanteSelected;
    [ObservableProperty] private Catalogo? _nacionalidadReportanteSelected;
    [ObservableProperty] private Catalogo? _religionReportanteSelected;
    [ObservableProperty] private Catalogo? _lenguaReportanteSelected;
    [ObservableProperty] private Catalogo? _parentescoReportanteSelected;
    [ObservableProperty] private bool? _pertenenciaColectivo;
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
    [ObservableProperty] private string? _informacionRelevante = string.Empty;
    [ObservableProperty] private string? _participacionBusqueda = string.Empty;
    [ObservableProperty] private bool? _denunciaAnonima;
    [ObservableProperty] private string? _curpReportante;
    [ObservableProperty] private string? _rfcReportante;
    [ObservableProperty] private int? _numeroPersonasVive;
    [ObservableProperty] private int? _edadEstimada;
    [ObservableProperty] private Catalogo? _estatusEscolaridadReportanteSelectede;
    
    [ObservableProperty] private string? _nombreDesaparecido;
    [ObservableProperty] private string? _apellidoPaternoDesaparecido;
    [ObservableProperty] private string? _apellidoMaternoDesaparecido;
    [ObservableProperty] private string? _pseudonimoNombreDesaparecido;
    [ObservableProperty] private string? _pseudonimoApellidoPaternoDesaparecido;
    [ObservableProperty] private string? _pseudonimoApellidoMaternoDesaparecido;
    [ObservableProperty] private Estado? _lugarNacimientoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _escolaridadDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _sexoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _generoDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _nacionalidadDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _religionDesaparecidoSelected;
    [ObservableProperty] private Catalogo? _lenguaDesaparecidoSelected;
    [ObservableProperty] private DateTime? _fechaNacimientoDesaparecido;
    [ObservableProperty] private DateTime? _fechaNacimientoAproximadaDesaparecido;
    [ObservableProperty] private DateTime? _fechaNacimientoCebvDesaparecido;
    [ObservableProperty] private string? _noTelefonoDesaparecido = string.Empty;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    [ObservableProperty] private bool? _esMovilDesaparecido;
    [ObservableProperty] private int? _edadAnos;
    [ObservableProperty] private int? _edadMeses;
    [ObservableProperty] private int? _edadDias;
    [ObservableProperty] private bool? _hablaEspañol;
    [ObservableProperty] private bool? _sabeLeer;
    [ObservableProperty] private bool? _sabeEscribir;
    [ObservableProperty] private Catalogo? _ocupacionPrincipal;
    [ObservableProperty] private Catalogo? _tipoOcupacionPrincipalDesaparecidoSelected;
    [ObservableProperty] private string? _descripcionOcupacionPrincipal = string.Empty;
    [ObservableProperty] private Catalogo? _ocupacionSecundaria;
    [ObservableProperty] private Catalogo? _tipoOcupacionSecundariaDesaparecidoSelected;
    [ObservableProperty] private string? _descripcionOcupacionSecundaria = string.Empty;
    [ObservableProperty] private bool? _accionUrgente;
    [ObservableProperty] private bool? _declaracionEspecialAusencia;
    [ObservableProperty] private bool? _dictamen;
    [ObservableProperty] private bool? _ciNivelFederal;
    [ObservableProperty] private Catalogo? _estatusEscolaridadDesaparecidoSelected;
    [ObservableProperty] private string? _curpDesaparecido;
    [ObservableProperty] private string? _rfcDesaparecido;
    [ObservableProperty] private string? _otroDerechoHumano = string.Empty;
    [ObservableProperty] private string? _identidadResguardada = string.Empty;
    [ObservableProperty] private string? _clasificacionPersona = string.Empty;
    [ObservableProperty] private string? _nombreParejaConyugal = string.Empty;
    [ObservableProperty] private string? _boletinImgPath = string.Empty;
    [ObservableProperty] private string? _urlBoletin = string.Empty;
    [ObservableProperty] private EstatusPersona? _estatusRpdno;
    [ObservableProperty] private EstatusPersona? _estatusCebv;
    
    [ObservableProperty] private string? _folioCebv = string.Empty;
    [ObservableProperty] private string? _folioFub = string.Empty;
    
    [ObservableProperty] private bool? _victimaExtorsion;
    [ObservableProperty] private bool? _recibioAmenaza;
    
    [ObservableProperty] private DateTime? _fechaDesaparicion;
    [ObservableProperty] private DateTime? _fechaDesaparicionAproximada;
    [ObservableProperty] private DateTime? _fechaDesaparicionCebv;
    [ObservableProperty] private TimeSpan? _horaDesaparicion;
    [ObservableProperty] private string? _hechosDesaparicion;
    [ObservableProperty] private DateTime? _fechaPercato;
    [ObservableProperty] private DateTime? _fechaPercatoCebv;
    [ObservableProperty] private TimeSpan? _horaPercato;
    [ObservableProperty] private bool? _amenazaCambioComportamiento;
    [ObservableProperty] private string? _descripcionAmenazaCambioComportamiento;
    [ObservableProperty] private string? _situacionPrevia;
    [ObservableProperty] private string? _aclaracionesFechasHechos;
    [ObservableProperty] private string? _hechoDesaparicionInformacionRelevante;
    [ObservableProperty] private string? _sintesisDesaparicion;
    [ObservableProperty] private int? _personaMismoEvento;
    [ObservableProperty] private int? _contadorDesapariciones;

    [ObservableProperty] private Catalogo? _hipotesisOficialSelected;
    [ObservableProperty] private Catalogo? _hipotesisOficialCircunstanciaSelected;
    [ObservableProperty] private string? _hipotesisOficialDescripcion = string.Empty;

    [ObservableProperty] private Double? _estatura;
    [ObservableProperty] private Double? _peso;
    [ObservableProperty] private Catalogo? _complexionSelected;
    [ObservableProperty] private Catalogo? _colorPielSelected;
    [ObservableProperty] private Catalogo? _colorOjosSelected;
    [ObservableProperty] private Catalogo? _colorCabelloSelected;
    [ObservableProperty] private Catalogo? _tamanioCabelloSelected;
    [ObservableProperty] private Catalogo? _tipoCabelloSelected;
    
    [ObservableProperty] private Catalogo? _regionCuerpoSelected;
    [ObservableProperty] private string? _colorRegionCuerpo;
    [ObservableProperty] private Catalogo? _vistaSelected;
    [ObservableProperty] private Catalogo? _ladoSelected;
    [ObservableProperty] private string? _colorLado;
    [ObservableProperty] private Catalogo? _tipoSelected;
    [ObservableProperty] private int? _cantidad;
    [ObservableProperty] private string? _descripcion;
    
    [ObservableProperty] private Catalogo? _relacionSelected;
    [ObservableProperty] private Catalogo? _marcaSelected;
    [ObservableProperty] private string? _submarca = string.Empty;
    [ObservableProperty] private Catalogo? _colorVehiculoSelected;
    [ObservableProperty] private string? _placa = string.Empty;
    [ObservableProperty] private string? _modelo = string.Empty;
    [ObservableProperty] private string? _numeroSerie = string.Empty;
    [ObservableProperty] private string? _numeroMotor = string.Empty;
    [ObservableProperty] private string? _numeroPermiso = string.Empty;
    [ObservableProperty] private Catalogo? _tipoVehiculoSelected;
    [ObservableProperty] private Catalogo? _usoVehiculoSelected;
    [ObservableProperty] private string? _senasParticularesVehiculo = string.Empty;
    [ObservableProperty] private bool? _localizado;
    
    [ObservableProperty] private Catalogo? _grupoPerteneciaSelected;
    [ObservableProperty] private Pertenencia? _perteneciaSelected;
    [ObservableProperty] private Catalogo? _colorPertenenciaSelected;
    [ObservableProperty] private string? _currentMarca = string.Empty;
    [ObservableProperty] private string? _currentPrendaDescripcion = string.Empty;
    
    [ObservableProperty] private string? _tipoDocumento = string.Empty;
    [ObservableProperty] private string? _numeroDocumento = string.Empty;
    [ObservableProperty] private string? _dondeRadica = string.Empty;
    [ObservableProperty] private string? _nombreServidorPublico = string.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcion;
    
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
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas"); ;
        Generos = await CebvNetwork.GetRoute<Catalogo>("generos");
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
        Nacionalidades = await CebvNetwork.GetRoute<Catalogo>("nacionalidades");
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
        Complexiones = await CebvNetwork.GetRoute<Catalogo>("complexiones");
        ColoresPiel = await CebvNetwork.GetRoute<Catalogo>("colores-pieles");
        ColoresOjos = await CebvNetwork.GetRoute<Catalogo>("colores-ojos");
        ColoresCabello = await CebvNetwork.GetRoute<Catalogo>("colores-cabellos");
        TamanosCabello = await CebvNetwork.GetRoute<Catalogo>("tamanos-cabellos");
        TiposCabello = await CebvNetwork.GetRoute<Catalogo>("tipos-cabellos");
        Vistas = await CebvNetwork.GetRoute<Catalogo>("vistas");
        Tipos = await CebvNetwork.GetRoute<Catalogo>("tipos");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
        GruposPertenencia = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        Pertenencias = await CebvNetwork.GetRoute<Pertenencia>("pertenencias");
        RegionesCuerpo = await CebvNetwork.GetRoute<Catalogo>("regiones-cuerpo");
        Lados = await CebvNetwork.GetRoute<Catalogo>("lados");
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
        HipotesisOficial = await CebvNetwork.GetRoute<Catalogo>("tipos-hipotesis");
        HipotesisOficialCircunstancia = await CebvNetwork.GetRoute<Catalogo>("circunstancias");
        Relaciones = await CebvNetwork.GetRoute<Catalogo>("relaciones-vehiculos");
        Marcas = await CebvNetwork.GetRoute<Catalogo>("marcas-vehiculos");
        TiposVehiculos = await CebvNetwork.GetRoute<Catalogo>("tipos-vehiculos");
        UsosVehiculos = await CebvNetwork.GetRoute<Catalogo>("usos-vehiculos");
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }
    
    private string AplicandoFiltros()
    {
        filterDictionary = new FilterDictionary(this);
        var dic = filterDictionary.DiccionarioFiltros;

        var filtrosValidos = new List<string>();
        var first = true;

        foreach (var filtro in dic)
        {
            if (filtro.Value is null ||filtro.Value is string && string.IsNullOrEmpty(filtro.Value)) continue;
            if (first)
            {
                filtrosValidos.Add($"?filter[{filtro.Key}]={ (filtro.Value is string ? filtro.Value : filtro.Value.ToString()) }");
                first = false;
                continue;
            }
            filtrosValidos.Add($"&filter[{filtro.Key}]={ (filtro.Value is string ? filtro.Value : filtro.Value.ToString()) }");
        }
        
        return string.Join("", filtrosValidos);
    }
    
    [RelayCommand]
    private async Task OnCargarReportes()
    {
        var filter = AplicandoFiltros();
        
        if (!string.IsNullOrEmpty(filter))
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes(filter);
        }
        else
        {
            Reportes = await FiltroBusquedaNetwork.GetReportes();
        }
    }

    [RelayCommand]
    private async Task OnBuscarPersona()
    {
        string filter = string.Empty;
        
        if (BuscarPorReportante.GetValueOrDefault() && !string.IsNullOrEmpty(Nombre))
        {
            filter = $"[nombreCompleto_desaparecido]={Nombre}";
        }
        else if (BuscarPorDesaparecido.GetValueOrDefault() && !string.IsNullOrEmpty(Nombre))
        {
            filter = $"[nombreCompleto_reportante]={Nombre}";
        }
        else if (!BuscarPorReportante.GetValueOrDefault() && !BuscarPorDesaparecido.GetValueOrDefault())
        {
            _snackbarService.Show(
                "No hay opcion seleccionada.", 
                "Por favor selecciona una opcion para realizar la busqueda",
                ControlAppearance.Caution,
                new SymbolIcon(SymbolRegular.Alert32),
                new TimeSpan(0,0, 5));
            
            return;
        }
        else if (string.IsNullOrEmpty(Nombre))
        {
            _snackbarService.Show(
                "No hay valor de busqueda.", 
                "Por favor ongrese un nombre para realizar la busqueda",
                ControlAppearance.Caution,
                new SymbolIcon(SymbolRegular.Alert32),
                new TimeSpan(0,0, 5));
            
            return;
        }
        
        Reportes = await FiltroBusquedaNetwork.GetReportes(filter);
    }

    [RelayCommand]
    private async Task OnClean()
    { 
        PropertyInfo [] properties = GetType().GetProperties();
        
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(ObservableCollection<Catalogo>) || property.PropertyType 
                == typeof(ObservableCollection<Estado>) || property.PropertyType 
                == typeof(ObservableCollection<CatalogoColor>) || property.PropertyType 
                == typeof(ObservableCollection<EstatusPersona>)) continue;
            
            if (property.PropertyType == typeof(string))
            {
                property.SetValue(this, string.Empty);
            }
            else if (Nullable.GetUnderlyingType(property.PropertyType) != null) 
            { 
                property.SetValue(this, null); 
            }
            else if (property.PropertyType == typeof(Catalogo) || property.PropertyType == typeof(CatalogoColor) ||
                      property.PropertyType == typeof(Estado) || property.PropertyType == typeof(EstatusPersona)) 
            { 
                property.SetValue(this, null); 
            }
        }
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