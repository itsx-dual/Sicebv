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
        string? reporteId,
        Persona? persona,
        EstatusPersona? estatusRpdno,
        EstatusPersona? estatusCebv,
        ObservableCollection<DocumentoLegal>? documentosLegales,
        ObservableCollection<PrendaDeVestir>? prendasDeVestir,
        bool? hablaEspanhol,
        bool? sabeLeerEscribir,
        string? urlBoletin,
        bool declaracionEspecialAusencia,
        bool accionUrgente,
        bool dictamen,
        bool ciNivelFederal,
        string? clasificacionPersona,
        string otroDerechoHumano,
        string? folioCebv,
        string? identidadResguardada,
        string? alias,
        string? otrasEspecificacionesOcupacion,
        string? nombreParejaConyugue,
        DateTime? fechaNacimientoAproximada,
        string? fechaNacimientoCebv,
        string? observacionesFechaNacimiento,
        int? edadMomentoDesaparicionAnos,
        int? edadMomentoDesaparicionMeses,
        int? edadMomentoDesaparicionDias,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        AccionUrgente = accionUrgente;
        CiNivelFederal = ciNivelFederal;
        ClasificacionPersona = clasificacionPersona;
        DeclaracionEspecialAusencia = declaracionEspecialAusencia;
        Dictamen = dictamen;
        DocumentosLegales = documentosLegales;
        EdadMomentoDesaparicionAnos = edadMomentoDesaparicionAnos;
        EdadMomentoDesaparicionDias = edadMomentoDesaparicionDias;
        EdadMomentoDesaparicionMeses = edadMomentoDesaparicionMeses;
        EstatusCebv = estatusCebv;
        EstatusRpdno = estatusRpdno;
        FechaNacimientoAproximada = fechaNacimientoAproximada;
        FechaNacimientoCebv = fechaNacimientoCebv;
        FolioCebv = folioCebv;
        IdentidadResguardada = identidadResguardada;
        ObservacionesFechaNacimiento = observacionesFechaNacimiento;
        OtrasEspecificacionesOcupacion = otrasEspecificacionesOcupacion;
        OtroDerechoHumano = otroDerechoHumano;
        Persona = persona;
        PrendasDeVestir = prendasDeVestir;
        ReporteId = reporteId;
        UrlBoletin = urlBoletin;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Desaparecido() { }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;

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
    
    [ObservableProperty, JsonProperty(PropertyName = "prendas_de_vestir")]
    private ObservableCollection<PrendaDeVestir>? _prendasDeVestir = new();

    [ObservableProperty, JsonProperty(PropertyName = "clasificacion_persona")]
    private string? _clasificacionPersona;

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
    
    [ObservableProperty, JsonProperty(PropertyName = "otras_especificaciones_ocupacion")]
    private string? _otrasEspecificacionesOcupacion;
    
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