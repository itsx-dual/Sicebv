using System.Collections.ObjectModel;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.util.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Desaparecido : ObservableObject
{
    [JsonConstructor]
    public Desaparecido(
        int? id,
        int? reporteId,
        string? identidadResguardada,
        Persona persona,
        BasicResource? estatusRpdno,
        BasicResource? estatusCebv,
        string? clasificacionPersona,
        bool declaracionEspecialAusencia,
        bool accionUrgente,
        bool dictamen,
        bool ciNivelFederal,
        string? otroDerechoHumano,
        DateTime? fechaNacimientoAproximada,
        string? fechaNacimientoCebv,
        string? observacionesFechaNacimiento,
        int? edadMomentoDesaparicionAnos,
        int? edadMomentoDesaparicionMeses,
        int? edadMomentoDesaparicionDias,
        string? urlBoletin,
        DateTime? createdAt,
        DateTime? updatedAt,
        ObservableCollection<DocumentoLegal> documentosLegales,
        Folio? folios,
        ObservableCollection<PrendaVestir> prendasVestir
    )
    {
        Id = id;
        ReporteId = reporteId;
        IdentidadResguardada = identidadResguardada;
        Persona = persona;
        EstatusRpdno = estatusRpdno;
        EstatusCebv = estatusCebv;
        ClasificacionPersona = clasificacionPersona;
        DeclaracionEspecialAusencia = declaracionEspecialAusencia;
        AccionUrgente = accionUrgente;
        Dictamen = dictamen;
        CiNivelFederal = ciNivelFederal;
        OtroDerechoHumano = otroDerechoHumano;
        FechaNacimientoAproximada = fechaNacimientoAproximada;
        FechaNacimientoCebv = fechaNacimientoCebv;
        ObservacionesFechaNacimiento = observacionesFechaNacimiento;
        EdadMomentoDesaparicionAnos = edadMomentoDesaparicionAnos;
        EdadMomentoDesaparicionMeses = edadMomentoDesaparicionMeses;
        EdadMomentoDesaparicionDias = edadMomentoDesaparicionDias;
        UrlBoletin = urlBoletin;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DocumentosLegales = documentosLegales;
        Folios = folios;
        PrendasVestir = prendasVestir;
    }

    public Desaparecido()
    {
    }

    /**
     * Atributos de desaparecido
     */
    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("identidad_resguardada")]
    private string? _identidadResguardada;

    [ObservableProperty, JsonProperty("persona")]
    private Persona _persona = new();

    [ObservableProperty, JsonProperty("estatus_rpdno")]
    private BasicResource? _estatusRpdno;

    [ObservableProperty, JsonProperty("estatus_cebv")]
    private BasicResource? _estatusCebv;

    [ObservableProperty, JsonProperty("clasificacion_persona")]
    private string? _clasificacionPersona;

    [ObservableProperty, JsonProperty("declaracion_especial_ausencia")]
    private bool _declaracionEspecialAusencia;

    [ObservableProperty, JsonProperty("accion_urgente")]
    private bool _accionUrgente;

    [ObservableProperty, JsonProperty("dictamen")]
    private bool _dictamen;

    [ObservableProperty, JsonProperty("ci_nivel_federal")]
    private bool _ciNivelFederal;

    [ObservableProperty, JsonProperty("otro_derecho_humano")]
    private string? _otroDerechoHumano;

    [ObservableProperty, JsonProperty("fecha_nacimiento_aproximada")]
    private DateTime? _fechaNacimientoAproximada;

    [ObservableProperty, JsonProperty("fecha_nacimiento_cebv")]
    private string? _fechaNacimientoCebv;

    [ObservableProperty, JsonProperty("observaciones_fecha_nacimiento")]
    private string? _observacionesFechaNacimiento;

    [ObservableProperty, JsonProperty("edad_momento_desaparicion_anos")]
    private int? _edadMomentoDesaparicionAnos;

    [ObservableProperty, JsonProperty("edad_momento_desaparicion_meses")]
    private int? _edadMomentoDesaparicionMeses;

    [ObservableProperty, JsonProperty("edad_momento_desaparicion_dias")]
    private int? _edadMomentoDesaparicionDias;

    [ObservableProperty, JsonProperty("url_boletin")]
    private string? _urlBoletin;

    [ObservableProperty, JsonProperty("created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty("updated_at")]
    private DateTime? _updatedAt;

    /**
     * Relaciones de desaparecido
     */
    [ObservableProperty, JsonProperty(PropertyName = "documentos_legales")]
    private ObservableCollection<DocumentoLegal> _documentosLegales = [];

    [ObservableProperty, JsonProperty("folios")]
    private Folio? _folios;

    [ObservableProperty, JsonProperty("prendas_vestir")]
    private ObservableCollection<PrendaVestir> _prendasVestir = [];
}