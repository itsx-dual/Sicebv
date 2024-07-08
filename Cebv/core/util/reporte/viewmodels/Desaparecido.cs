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
        DateTime? created_at,
        DateTime? updated_at)
    {
        Id = id;
        OcupacionPrincipal = ocupacion_principal;
        OcupacionSecundaria = ocupacion_secundaria;
        ReporteId = reporte_id;
        Persona = persona;
        EstatusRpdno = estatus_rpdno;
        EstatusCebv = estatus_cebv;
        DocumentosLegales = documentos_legales;
        ClasificacionPersona = clasificacion_persona;
        HablaEspanhol = habla_espanhol;
        SabeLeer = sabe_leer;
        SabeEscribir = sabe_escribir;
        UrlBoletin = url_boletin;
        DeclaracionEspecialAusencia = declaracion_especial_ausencia;
        AccionUrgente = accion_urgente;
        Dictamen = dictamen;
        CiNivelFederal = ci_nivel_federal;
        OtroDerechoHumano = otro_derecho_humano;
        FolioCebv = folio_cebv;
        IdentidadResguardada = identidad_resguardada;
        Alias = alias;
        DescripcionOcupacionPrincipal = descripcion_ocupacion_principal;
        DescripcionOcupacionSecundaria = descripcion_ocupacion_secundaria;
        OtrasEspecificacionesOcupacion = otras_especificaciones_ocupacion;
        NombreParejaConyugue = nombre_pareja_conyugue;
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
    private Persona? _persona;

    [ObservableProperty, JsonProperty(PropertyName = "estatus_rpdno")]
    private EstatusPersona? _estatusRpdno;

    [ObservableProperty, JsonProperty(PropertyName = "estatus_cebv")]
    private EstatusPersona? _estatusCebv;

    [ObservableProperty, JsonProperty(PropertyName = "documentos_legales")]
    private ObservableCollection<DocumentoLegal>? _documentosLegales = new();

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
    
    [ObservableProperty, JsonProperty(PropertyName = "created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty(PropertyName = "updated_at")]
    private DateTime? _updatedAt;
}