using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Input;
using Cebv.app.presentation;
using Cebv.core.data;
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
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.dashboard.filtro_busqueda;

public partial class FiltroBusquedaViewModel : ObservableObject
{
    private ISnackbarService _snackbarService;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Opciones;
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = [];
    [ObservableProperty] private ReporteResponse _reporteSelected;
    [ObservableProperty] private DesaparecidoResponse _desaparecidoSelected;
    
    //Variable para busqueda de persona por reportante o desaparecido
    [ObservableProperty] private bool? _buscarPorReportante;
    [ObservableProperty] private bool? _buscarPorDesaparecido;
    [ObservableProperty] private string? _nombre;
    
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
    
    [ObservableProperty] private ObservableCollection<Catalogo> _hipotesisOficial = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _hipotesisOficialCircunstancia = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello;

    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo;
    [ObservableProperty] private ObservableCollection<Catalogo> _vistas;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados;
    [ObservableProperty] private ObservableCollection<Catalogo> _colores;
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos;
    
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
    
    [ObservableProperty] private CatalogoColor? _regionCuerpoSelected;
    [ObservableProperty] private string? _colorRegionCuerpo;
    [ObservableProperty] private Catalogo? _vistaSelected;
    [ObservableProperty] private CatalogoColor? _ladoSelected;
    [ObservableProperty] private string? _colorLado;
    [ObservableProperty] private Catalogo? _tipoSelected;
    [ObservableProperty] private int? _cantidad;
    [ObservableProperty] private string? _descripcion;

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
        //GruposPertenencia = await CebvNetwork.GetRoute("grupos-pertenencias");
        RegionesCuerpo = await CebvNetwork.GetRoute<CatalogoColor>("regiones-cuerpo");
        Lados = await CebvNetwork.GetRoute<CatalogoColor>("lados");
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

    [RelayCommand]
    private async Task OnBuscarPersona()
    {
        string filter = string.Empty;
        
        if (BuscarPorReportante.GetValueOrDefault() && !string.IsNullOrEmpty(Nombre))
        {
            filter = $"[nombreCompleto_desaparecido]={Nombre}";
        }
        if (BuscarPorDesaparecido.GetValueOrDefault() && !string.IsNullOrEmpty(Nombre))
        {
            filter = $"[nombreCompleto_reportante]={Nombre}";
        }
        
        Reportes = await FiltroBusquedaNetwork.GetReportes(filter);
    }

    [RelayCommand]
    private async Task OnClean()
    { 
        TipoMedioSelected = null;
        MedioSelected = null; 
        AreaSelected = null; 
        EstadoSelected = null; 
        ZonaEstadoSelected = null; 
        TipoReporteSelected = null; 
        LugarNacimientoReportanteSelected = null; 
        EscolaridadReportanteSelected = null; 
        SexoReportanteSelected = null; 
        GeneroReportanteSelected = null; 
        NacionalidadReportanteSelected = null; 
        ReligionReportanteSelected = null; 
        LenguaReportanteSelected = null; 
        ParentescoReportanteSelected = null; 
        PertenenciaColectivo = null; 
        ColectivoReportanteSelected = null; 
        EstadoConyugalReportanteSelected = null; 
        FechaNacimientoReportante = null; 
        TipoOcupacionReportanteSelected = null; 
        NoTelefonoReportante = null; 
        CompañiaTelefonicaReportanteSelected = null; 
        EsMovilReportante = null; 
        PublicacionRegistroNacional = null; 
        PublicacionBoletin = null; 
        DescripcionExtorsion = null; 
        DescripcionDondeProviene = null; 
        InformacionConsentimiento = null; 
        InformacionExclusivaBusqueda = null; 
        InformacionRelevante = null; 
        ParticipacionBusqueda = null; 
        DenunciaAnonima = null; 
        CurpReportante = null; 
        RfcReportante = null; 
        NumeroPersonasVive = null; 
        EdadEstimada = null; 
        EstatusEscolaridadReportanteSelectede = null; 
        LugarNacimientoDesaparecidoSelected = null; 
        EscolaridadDesaparecidoSelected = null; 
        SexoDesaparecidoSelected = null; 
        GeneroDesaparecidoSelected = null; 
        NacionalidadDesaparecidoSelected = null; 
        ReligionDesaparecidoSelected = null; 
        LenguaDesaparecidoSelected = null; 
        FechaNacimientoDesaparecido = null; 
        FechaNacimientoAproximadaDesaparecido = null; 
        FechaNacimientoCebvDesaparecido = null; 
        NoTelefonoDesaparecido = null; 
        CompañiaTelefonicaDesaparecidoSelected = null; 
        EsMovilDesaparecido = null; 
        EdadAnos = null; 
        EdadMeses = null; 
        EdadDias = null; 
        HablaEspañol = null; 
        SabeLeer = null; 
        SabeEscribir = null; 
        OcupacionPrincipal = null; 
        TipoOcupacionPrincipalDesaparecidoSelected = null; 
        DescripcionOcupacionPrincipal = null; 
        OcupacionSecundaria = null; 
        TipoOcupacionSecundariaDesaparecidoSelected = null; 
        DescripcionOcupacionSecundaria = null; 
        AccionUrgente = null; 
        DeclaracionEspecialAusencia = null; 
        Dictamen = null; 
        CiNivelFederal = null; 
        EstatusEscolaridadDesaparecidoSelected = null; 
        CurpDesaparecido = null; 
        RfcDesaparecido = null; 
        OtroDerechoHumano = null; 
        IdentidadResguardada = null; 
        ClasificacionPersona = null; 
        NombreParejaConyugal = null; 
        BoletinImgPath = null; 
        UrlBoletin = null; 
        EstatusRpdno = null; 
        EstatusCebv = null; 
        FolioCebv = null; 
        FolioFub = null; 
        VictimaExtorsion = null; 
        RecibioAmenaza = null; 
        FechaDesaparicion = null; 
        FechaDesaparicionAproximada = null; 
        FechaDesaparicionCebv = null; 
        HoraDesaparicion = null; 
        HechosDesaparicion = null; 
        FechaPercato = null; 
        FechaPercatoCebv = null; 
        HoraPercato = null; 
        AmenazaCambioComportamiento = null; 
        DescripcionAmenazaCambioComportamiento = null; 
        SituacionPrevia = null; 
        AclaracionesFechasHechos = null; 
        HechoDesaparicionInformacionRelevante = null; 
        SintesisDesaparicion = null; 
        PersonaMismoEvento = null; 
        ContadorDesapariciones = null; 
        HipotesisOficialSelected = null; 
        HipotesisOficialCircunstanciaSelected = null; 
        HipotesisOficialDescripcion = null; 
        Estatura = null; 
        Peso = null; 
        ComplexionSelected = null; 
        ColorPielSelected = null; 
        ColorOjosSelected = null; 
        ColorCabelloSelected = null; 
        TamanioCabelloSelected = null; 
        TipoCabelloSelected = null; 
        RegionCuerpoSelected = null; 
        ColorRegionCuerpo = null; 
        VistaSelected = null; 
        LadoSelected = null; 
        ColorLado = null; 
        TipoSelected = null; 
        Cantidad = null; 
        Descripcion = null;
    }
    
    private async Task<string> AplicandoFiltros()
    {
        var filtros = new List<string>();
        
        if (PseudonimoDesaparecido != null)
        {
            filtros.Add($"[pseudonimoCompleto_desaparecido]={PseudonimoDesaparecido}");
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
        if (TipoMedioSelected != null)
        {
            filtros.Add($"[tipo_medio_id]={TipoMedioSelected.Id}");
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
        if (LugarNacimientoReportanteSelected != null)
        {
            filtros.Add($"[reportante/lugar_nacimiento_id]={LugarNacimientoReportanteSelected.Id}");
        }
        if (EscolaridadReportanteSelected != null)
        {
            filtros.Add($"[reportante/escolaridad_id]={EscolaridadReportanteSelected.Id}");
        }
        if (SexoReportanteSelected != null)
        {
            filtros.Add($"[reportante/sexo_id]={SexoReportanteSelected.Id}");
        }
        if (GeneroReportanteSelected != null)
        {
            filtros.Add($"[reportante/genero_id]={GeneroReportanteSelected.Id}");
        }
        if (NacionalidadReportanteSelected != null)
        {
            filtros.Add($"[reportante/nacionalidad_id]={NacionalidadReportanteSelected.Id}");
        }
        if (ReligionReportanteSelected != null)
        {
            filtros.Add($"[reportante/religion_id]={ReligionReportanteSelected.Id}");
        }
        if (LenguaReportanteSelected != null)
        {
            filtros.Add($"[reportante/lengua_id]={LenguaReportanteSelected.Id}");
        }
        if (ParentescoReportanteSelected != null)
        {
            filtros.Add($"[reportante/parentesco_id]={ParentescoReportanteSelected.Id}");
        }
        if (ColectivoReportanteSelected != null)
        {
            filtros.Add($"[reportante/colectivo_id]={ColectivoReportanteSelected.Id}");
        }
        if (EstadoConyugalReportanteSelected != null)
        {
            filtros.Add($"[reportante/estado_conyugal_id]={EstadoConyugalReportanteSelected.Id}");
        }
        if (PublicacionRegistroNacional != null)
        {
            filtros.Add($"[publicacion_registro_nacional]={PublicacionRegistroNacional}");
        }
        if (PublicacionBoletin != null)
        {
            filtros.Add($"[publicacion_boletin]={PublicacionBoletin}");
        }
        if (DescripcionExtorsion != null)
        {
            filtros.Add($"[descripcion_extorsion]={DescripcionExtorsion}");
        }
        if (DescripcionDondeProviene != null)
        {
            filtros.Add($"[descripcion_donde_proviene]={DescripcionDondeProviene}");
        }
        if (NoTelefonoReportante != null)
        {
            filtros.Add($"[reportante/telefono]={NoTelefonoReportante}");
        }
        if (CompañiaTelefonicaReportanteSelected != null)
        {
            filtros.Add($"[reportante/telefono/compania_id]={CompañiaTelefonicaReportanteSelected.Id}");
        }
        if (EsMovilReportante != null)
        {
            filtros.Add($"[reportante/telefono/es_movil]={EsMovilReportante}");
        }
        if (InformacionConsentimiento != null)
        {
            filtros.Add($"[informacion_consentimiento]={InformacionConsentimiento}");
        }
        if (InformacionExclusivaBusqueda != null)
        {
            filtros.Add($"[informacion_exclusiva_busqueda]={InformacionExclusivaBusqueda}");
        }
        if (InformacionRelevante != null)
        {
            filtros.Add($"[informacion_relevante]={InformacionRelevante}");
        }
        if (FechaNacimientoReportante != null)
        {
            filtros.Add($"[reportante/fecha_nacimiento]={FechaNacimientoReportante}");
        }
        if (DenunciaAnonima != null)
        {
            filtros.Add($"[reportante/denuncia_anonima]={DenunciaAnonima}");
        }
        if (CurpReportante != null)
        {
            filtros.Add($"[reportante/curp]={CurpReportante}");
        }
        if (PertenenciaColectivo != null)
        {
            filtros.Add($"[reportante/pertenencia_colectivo]={PertenenciaColectivo}");
        }
        if (RfcReportante != null)
        {
            filtros.Add($"[reportante/rfc]={RfcReportante}");
        }
        if (EdadEstimada != null)
        {
            filtros.Add($"[edad_estimada]={EdadEstimada}");
        }
        if (EstatusEscolaridadReportanteSelectede != null)
        {
            filtros.Add($"[reportante/nivel_escolaridad]={EstatusEscolaridadReportanteSelectede.Nombre}");
        }
        if (NumeroPersonasVive != null)
        {
            filtros.Add($"[reportante/numero_personas_vive]={NumeroPersonasVive}");
        }
        if (LugarNacimientoDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/lugar_nacimiento_id]={LugarNacimientoDesaparecidoSelected.Id}");
        }
        if (EscolaridadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/escolaridad_id]={EscolaridadDesaparecidoSelected.Id}");
        }
        if (SexoDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/sexo_id]={SexoDesaparecidoSelected.Id}");
        }
        if (GeneroDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/genero_id]={GeneroDesaparecidoSelected.Id}");
        }
        if (NacionalidadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/nacionalidad_id]={NacionalidadDesaparecidoSelected.Id}");
        }
        if (ReligionDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/religion_id]={ReligionDesaparecidoSelected.Id}");
        }
        if (LenguaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/lengua_id]={LenguaDesaparecidoSelected.Id}");
        }
        if (FechaNacimientoDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento]={FechaNacimientoDesaparecido}");
        }
        if (FechaNacimientoAproximadaDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento_aproximada]={FechaNacimientoAproximadaDesaparecido}");
        }
        if (FechaNacimientoCebvDesaparecido != null)
        {
            filtros.Add($"[desaparecido/fecha_nacimiento_cebv]={FechaNacimientoCebvDesaparecido}");
        }
        if (DescripcionOcupacionPrincipal != null)
        {
            filtros.Add($"[desaparecido/descripcion_ocupacion_principal]={DescripcionOcupacionPrincipal}");
        }
        if (DescripcionOcupacionSecundaria != null)
        {
            filtros.Add($"[desaparecido/descripcion_ocupacion_secundaria]={DescripcionOcupacionSecundaria}");
        }
        if (NoTelefonoDesaparecido != null)
        {
            filtros.Add($"[desaparecido/telefono]={NoTelefonoDesaparecido}");
        }
        if (CompañiaTelefonicaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/telefono/compania_id]={CompañiaTelefonicaDesaparecidoSelected.Id}");
        }
        if (EsMovilDesaparecido != null)
        {
            filtros.Add($"[desaparecido/telefono/es_movil]={EsMovilDesaparecido}");
        }
        if (EdadAnos != null)
        {
            filtros.Add($"[desaparecido/edad_anos]={EdadAnos}");
        }
        if (EdadMeses != null)
        {
            filtros.Add($"[desaparecido/edad_meses]={EdadMeses}");
        }
        if (EdadDias != null)
        {
            filtros.Add($"[desaparecido/edad_dias]={EdadDias}");
        }
        if (HablaEspañol != null)
        {
            filtros.Add($"[desaparecido/habla_espanhol]={HablaEspañol}");
        }
        if (SabeLeer != null)
        {
            filtros.Add($"[desaparecido/sabe_leer]={SabeLeer}");
        }
        if (SabeEscribir != null)
        {
            filtros.Add($"[desaparecido/sabe_escribir]={SabeEscribir}");
        }
        if (EstatusRpdno != null)
        {
            filtros.Add($"[estatus_rpdno_id]={EstatusRpdno.Id}");
        }
        if (EstatusCebv != null)
        {
            filtros.Add($"[estatus_cebv_id]={EstatusCebv.Id}");
        }
        if (OcupacionPrincipal != null)
        {
            filtros.Add($"[desaparecido/ocupacion_principal_id]={OcupacionPrincipal.Id}");
        }
        if (TipoOcupacionPrincipalDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/ocupacion_principal/tipo_ocupacion_id]={TipoOcupacionPrincipalDesaparecidoSelected.Id}");
        }
        if (OcupacionSecundaria != null)
        {
            filtros.Add($"[desaparecido/ocupacion_secundaria_id]={OcupacionSecundaria.Id}");
        }
        if (TipoOcupacionSecundariaDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/ocupacion_secundaria/tipo_ocupacion_id]={TipoOcupacionSecundariaDesaparecidoSelected.Id}");
        }
        if (AccionUrgente != null)
        {
            filtros.Add($"[desaparecido/accion_urgente]={AccionUrgente}");
        }
        if (DeclaracionEspecialAusencia != null)
        {
            filtros.Add($"[desaparecido/declaracion_especial_ausencia]={DeclaracionEspecialAusencia}");
        }
        if (Dictamen != null)
        {
            filtros.Add($"[desaparecido/dictamen]={Dictamen}");
        }
        if (CiNivelFederal != null)
        {
            filtros.Add($"[desaparecido/ci_nivel_federal]={CiNivelFederal}");
        }
        if (EstatusEscolaridadDesaparecidoSelected != null)
        {
            filtros.Add($"[desaparecido/nivel_escolaridad]={EstatusEscolaridadDesaparecidoSelected.Nombre}");
        }
        if (CurpDesaparecido != null)
        {
            filtros.Add($"[desaparecido/curp]={CurpDesaparecido}");
        }
        if (RfcDesaparecido != null)
        {
            filtros.Add($"[desaparecido/rfc]={RfcDesaparecido}");
        }
        if (OtroDerechoHumano != null)
        {
            filtros.Add($"[desaparecido/otro_derecho_humano]={OtroDerechoHumano}");
        }
        if (IdentidadResguardada != null)
        {
            filtros.Add($"[desaparecido/identidad_resguardada]={IdentidadResguardada}");
        }
        if (ClasificacionPersona != null)
        {
            filtros.Add($"[desaparecido/clasificacion_persona]={ClasificacionPersona}");
        }
        if (NombreParejaConyugal != null)
        {
            filtros.Add($"[desaparecido/nombre_pareja_conyugue]={NombreParejaConyugal}");
        }
        if (UrlBoletin != null)
        {
            filtros.Add($"[desaparecido/url_boletin]={UrlBoletin}");
        }
        if (BoletinImgPath != null)
        {
            filtros.Add($"[desaparecido/boletin_img_path]={BoletinImgPath}");
        }
        if (FolioCebv != null)
        {
            filtros.Add($"[folio_cebv]={FolioCebv}");
        }
        if (FolioFub != null)
        {
            filtros.Add($"[folio_fub]={FolioFub}");
        }
        if (FechaDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion]={FechaDesaparicion}");
        }
        if (FechaDesaparicionAproximada != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion_aproximada]={FechaDesaparicionAproximada}");
        }
        if (FechaDesaparicionCebv != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_desaparicion_cebv]={FechaDesaparicionCebv}");
        }
        if (HoraDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/hora_desaparicion]={HoraDesaparicion}");
        }
        if (HechosDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/hechos_desaparicion]={HechosDesaparicion}");
        }
        if (FechaPercato != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_percato]={FechaPercato}");
        }
        if (FechaPercatoCebv != null)
        {
            filtros.Add($"[hechoDesaparicion/fecha_percato_cebv]={FechaPercatoCebv}");
        }
        if (HoraPercato != null)
        {
            filtros.Add($"[hechoDesaparicion/hora_percato]={HoraPercato}");
        }
        if (AmenazaCambioComportamiento != null)
        {
            filtros.Add($"[hechoDesaparicion/amenaza_cambio_comportamiento]={AmenazaCambioComportamiento}");
        }
        if (DescripcionAmenazaCambioComportamiento != null)
        {
            filtros.Add($"[hechoDesaparicion/descripcion/amenaza_cambio_comportamiento]={DescripcionAmenazaCambioComportamiento}");
        }
        if (SituacionPrevia != null)
        {
            filtros.Add($"[hechoDesaparicion/situacion_previa]={SituacionPrevia}");
        }
        if (AclaracionesFechasHechos != null)
        {
            filtros.Add($"[hechoDesaparicion/aclaraciones_fechas_hechos]={AclaracionesFechasHechos}");
        }
        if (HechoDesaparicionInformacionRelevante != null)
        {
            filtros.Add($"[hechoDesaparicion/informacion_relevante]={HechoDesaparicionInformacionRelevante}");
        }
        if (SintesisDesaparicion != null)
        {
            filtros.Add($"[hechoDesaparicion/sintesis_desaparicion]={SintesisDesaparicion}");
        }
        if (PersonaMismoEvento != null)
        {
            filtros.Add($"[hechoDesaparicion/personas_mismo_evento]={PersonaMismoEvento}");
        }
        if (ContadorDesapariciones != null)
        {
            filtros.Add($"[hechoDesaparicion/contador_desapariciones]={ContadorDesapariciones}");
        }
        if (HipotesisOficialSelected != null)
        {
            filtros.Add($"[hipotesis_oficial_id]={HipotesisOficialSelected.Id}");
        }
        if (HipotesisOficialCircunstanciaSelected != null)
        {
            filtros.Add($"[hipotesis_oficial_circunstancia_id]={HipotesisOficialCircunstanciaSelected.Id}");
        }
        if (HipotesisOficialDescripcion != null)
        {
            filtros.Add($"[hipotesis_oficial_descripcion]={HipotesisOficialDescripcion}");
        }
        if (Estatura != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/estatura]={Estatura}");
        }
        if (Peso != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/peso]={Peso}");
        }
        if (ComplexionSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/complexion_id]={ComplexionSelected.Id}");
        }
        if (ColorPielSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_piel_id]={ColorPielSelected.Id}");
        }
        if (ColorOjosSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_ojos_id]={ColorOjosSelected.Id}");
        }
        if (ColorCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/color_cabello_id]={ColorCabelloSelected.Id}");
        }
        if (TamanioCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/tamano_cabello_id]={TamanioCabelloSelected.Id}");
        }
        if (TipoCabelloSelected != null)
        {
            filtros.Add($"[desaparecido/media_filiacion/tipo_cabello_id]={TipoCabelloSelected.Id}");
        }
        if (RegionCuerpoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/region_cuerpo_id]={RegionCuerpoSelected.Id}");
        }
        if (ColorRegionCuerpo != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/region_cuerpo/color]={ColorRegionCuerpo}");
        }
        if (VistaSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/vista_id]={VistaSelected.Id}");
        }
        if (LadoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/lado_id]={LadoSelected.Id}");
        }
        if (ColorLado != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/lado/color]={ColorLado}");
        }
        if (TipoSelected != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/tipo_id]={TipoSelected.Id}");
        }
        if (Cantidad != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/cantidad]={Cantidad}");
        }
        if (Descripcion != null)
        {
            filtros.Add($"[desaparecido/senas_particulares/descripcion]={Descripcion}");
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