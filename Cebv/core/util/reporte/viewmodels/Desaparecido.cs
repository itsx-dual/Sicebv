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
        string? reporte_id,
        Persona? persona,
        EstatusPersona? estatus_rpdno,
        EstatusPersona? estatus_cebv,
        ObservableCollection<DocumentoLegal>? documentos_legales,
        string? clasificacion_persona,
        bool? habla_espanhol,
        bool? sabe_leer,
        bool? sabe_escribir,
        string? url_boletin,
        bool declaracion_especial_ausencia,
        bool accion_urgente,
        bool dictamen,
        bool ci_nivel_federal,
        string otro_derecho_humano,
        string? folio_cebv,
        DateTime? created_at,
        DateTime? updated_at)
    {
        Id = id;
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
        CreatedAt = created_at;
        UpdatedAt = updated_at;
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
    private ObservableCollection<DocumentoLegal> _documentosLegales = new();

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

    [ObservableProperty, JsonProperty(PropertyName = "created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty(PropertyName = "updated_at")]
    private DateTime? _updatedAt;
}