using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Desaparecido : ObservableObject
{
    [JsonConstructor]
    public Desaparecido(
        int id,
        Ocupacion? ocupacion_principal, 
        Ocupacion? ocupacion_secundaria,
        string? reporte_id,
        Persona? persona,
        EstatusPersona? estatus_rpdno,
        EstatusPersona? estatus_cebv,
        ObservableCollection<DocumentoLegal>? documentos_legales,
        ObservableCollection<PrendaDeVestir>? prendas_de_vestir,
        bool? habla_espanhol,
        bool? sabe_leer,
        bool? sabe_escribir,
        string? url_boletin,
        bool declaracion_especial_ausencia,
        bool accion_urgente,
        bool dictamen,
        bool ci_nivel_federal,
        string? clasificacion_persona,
        string otro_derecho_humano,
        string? folio_cebv,
        string? identidad_resguardada,
        string? alias,
        string? descripcion_ocupacion_principal,
        string? descripcion_ocupacion_secundaria,
        string? otras_especificaciones_ocupacion,
        string? nombre_pareja_conyugue,
        DateTime? fecha_nacimiento_aproximada,
        string? fecha_nacimiento_cebv,
        string? observaciones_fecha_nacimiento,
        int? edad_momento_desaparicion_anos,
        int? edad_momento_desaparicion_meses,
        int? edad_momento_desaparicion_dias,
        DateTime? created_at,
        DateTime? updated_at)
    {
        Id = id;
        AccionUrgente = accion_urgente;
        Alias = alias;
        CiNivelFederal = ci_nivel_federal;
        ClasificacionPersona = clasificacion_persona;
        DeclaracionEspecialAusencia = declaracion_especial_ausencia;
        DescripcionOcupacionPrincipal = descripcion_ocupacion_principal;
        DescripcionOcupacionSecundaria = descripcion_ocupacion_secundaria;
        Dictamen = dictamen;
        DocumentosLegales = documentos_legales;
        EdadMomentoDesaparicionAnos = edad_momento_desaparicion_anos;
        EdadMomentoDesaparicionDias = edad_momento_desaparicion_dias;
        EdadMomentoDesaparicionMeses = edad_momento_desaparicion_meses;
        EstatusCebv = estatus_cebv;
        EstatusRpdno = estatus_rpdno;
        FechaNacimientoAproximada = fecha_nacimiento_aproximada;
        FechaNacimientoCebv = fecha_nacimiento_cebv;
        FolioCebv = folio_cebv;
        HablaEspanhol = habla_espanhol;
        IdentidadResguardada = identidad_resguardada;
        NombreParejaConyugue = nombre_pareja_conyugue;
        ObservacionesFechaNacimiento = observaciones_fecha_nacimiento;
        OcupacionPrincipal = ocupacion_principal;
        OcupacionSecundaria = ocupacion_secundaria;
        OtrasEspecificacionesOcupacion = otras_especificaciones_ocupacion;
        OtroDerechoHumano = otro_derecho_humano;
        Persona = persona;
        PrendasDeVestir = prendas_de_vestir;
        ReporteId = reporte_id;
        SabeEscribir = sabe_escribir;
        SabeLeer = sabe_leer;
        UrlBoletin = url_boletin;
        CreatedAt = created_at;
        UpdatedAt = updated_at;
    }

    public Desaparecido() { }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "ocupacion_principal")]
    private Ocupacion _ocupacionPrincipal;
    
    [ObservableProperty, JsonProperty(PropertyName = "ocupacion_secundaria")]
    private Ocupacion _ocupacionSecundaria;

    [ObservableProperty, JsonProperty(PropertyName = "reporte_id")]
    private string? _reporteId;

    [ObservableProperty, JsonProperty(PropertyName = "persona")]
    private Persona _persona = new();

    [ObservableProperty, JsonProperty(PropertyName = "estatus_rpdno")]
    private EstatusPersona? _estatusRpdno;

    [ObservableProperty, JsonProperty(PropertyName = "estatus_cebv")]
    private EstatusPersona? _estatusCebv;

    [ObservableProperty, JsonProperty(PropertyName = "documentos_legales")]
    private ObservableCollection<DocumentoLegal>? _documentosLegales = new();
    
    [ObservableProperty, JsonProperty(PropertyName = "prendas_de_vestir")]
    private ObservableCollection<PrendaDeVestir>? _prendasDeVestir = new();

    [ObservableProperty, JsonProperty(PropertyName = "clasificacion_persona")]
    private string? _clasificacionPersona;

    [ObservableProperty, JsonProperty(PropertyName = "habla_espanhol")]
    private bool? _hablaEspanhol;

    [ObservableProperty, JsonProperty(PropertyName = "sabe_leer")]
    private bool? _sabeLeer;

    [ObservableProperty, JsonProperty(PropertyName = "sabe_escribir")]
    private bool? _sabeEscribir;

    [ObservableProperty, JsonProperty(PropertyName = "url_boletin")]
    private string? _urlBoletin;

    [ObservableProperty, JsonProperty(PropertyName = "declaracion_especial_ausencia")]
    private bool _declaracionEspecialAusencia;

    [ObservableProperty, JsonProperty(PropertyName = "accion_urgente")]
    private bool _accionUrgente;

    [ObservableProperty, JsonProperty(PropertyName = "dictamen")]
    private bool _dictamen;

    [ObservableProperty, JsonProperty(PropertyName = "ci_nivel_federal")]
    private bool _ciNivelFederal;

    [ObservableProperty, JsonProperty(PropertyName = "otro_derecho_humano")]
    private string _otroDerechoHumano = string.Empty;

    [ObservableProperty, JsonProperty(PropertyName = "folio_cebv")]
    private string? _folioCebv;

    [ObservableProperty, JsonProperty(PropertyName = "identidad_resguardada")]
    private string? _identidadResguardada;

    [ObservableProperty, JsonProperty(PropertyName = "alias")]
    private string? _alias;

    [ObservableProperty, JsonProperty(PropertyName = "descripcion_ocupacion_principal")]
    private string? _descripcionOcupacionPrincipal;

    [ObservableProperty, JsonProperty(PropertyName = "descripcion_ocupacion_secundaria")]
    private string? _descripcionOcupacionSecundaria;

    [ObservableProperty, JsonProperty(PropertyName = "otras_especificaciones_ocupacion")]
    private string? _otrasEspecificacionesOcupacion;

    [ObservableProperty, JsonProperty(PropertyName = "nombre_pareja_conyugue")]
    private string? _nombreParejaConyugue;
    
    [ObservableProperty, JsonProperty(PropertyName = "fecha_nacimiento_aproximada")]
    private DateTime? _fechaNacimientoAproximada;
    
    [ObservableProperty, JsonProperty(PropertyName = "fecha_nacimiento_cebv")]
    private string? _fechaNacimientoCebv;
    
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_fecha_nacimiento")]
    private string? _observacionesFechaNacimiento;
    
    [ObservableProperty, JsonProperty(PropertyName = "edad_momento_desaparicion_anos")]
    private int? _edadMomentoDesaparicionAnos;
    
    [ObservableProperty, JsonProperty(PropertyName = "edad_momento_desaparicion_meses")]
    private int? _edadMomentoDesaparicionMeses;
    
    [ObservableProperty, JsonProperty(PropertyName = "edad_momento_desaparicion_dias")]
    private int? _edadMomentoDesaparicionDias;
    
    [ObservableProperty, JsonProperty(PropertyName = "created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty(PropertyName = "updated_at")]
    private DateTime? _updatedAt;
}