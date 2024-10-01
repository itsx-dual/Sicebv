using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Reportante : ObservableObject
{
    [JsonConstructor]
    public Reportante(
        int? id,
        int? reporteId,
        Persona persona,
        Catalogo? parentesco,
        Catalogo? colectivo,
        bool denunciaAnonima,
        bool? informacionConsentimiento,
        bool? informacionExclusivaBusqueda,
        bool? publicacionRegistroNacional,
        bool? publicacionBoletin,
        string? informacionRelevante,
        bool? pertenenciaColectivo,
        bool? participacionPreviaBusquedas,
        string? descripcionParticipacionBusquedas,
        bool? victimaExtorsionFraude,
        string? descripcionExtorsionFraude,
        bool? recibioAmenazas,
        string? descripcionOrigenAmenazas,
        int? edadEstimadaAnhos,
        DateTime? createdAt,
        DateTime? updatedAt
    )
    {
        Id = id;
        ReporteId = reporteId;
        Persona = persona;
        Parentesco = parentesco;
        Colectivo = colectivo;
        DenunciaAnonima = denunciaAnonima;
        InformacionConsentimiento = informacionConsentimiento;
        InformacionExclusivaBusqueda = informacionExclusivaBusqueda;
        PublicacionRegistroNacional = publicacionRegistroNacional;
        PublicacionBoletin = publicacionBoletin;
        InformacionRelevante = informacionRelevante;
        PertenenciaColectivo = pertenenciaColectivo;
        ParticipacionPreviaBusquedas = participacionPreviaBusquedas;
        DescripcionParticipacionBusquedas = descripcionParticipacionBusquedas;
        VictimaExtorsionFraude = victimaExtorsionFraude;
        DescripcionExtorsionFraude = descripcionExtorsionFraude;
        RecibioAmenazas = recibioAmenazas;
        DescripcionOrigenAmenazas = descripcionOrigenAmenazas;
        EdadEstimadaAnhos = edadEstimadaAnhos;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Reportante()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("persona")]
    private Persona _persona = new();

    [ObservableProperty, JsonProperty("parentesco")]
    private Catalogo? _parentesco;

    [ObservableProperty, JsonProperty("colectivo")]
    private Catalogo? _colectivo;

    [ObservableProperty, JsonProperty("denuncia_anonima")]
    private bool _denunciaAnonima;

    [ObservableProperty, JsonProperty("informacion_consentimiento")]
    private bool? _informacionConsentimiento = false;

    [ObservableProperty, JsonProperty("informacion_exclusiva_busqueda")]
    private bool? _informacionExclusivaBusqueda = false;

    [ObservableProperty, JsonProperty("publicacion_registro_nacional")]
    private bool? _publicacionRegistroNacional = false;

    [ObservableProperty, JsonProperty("publicacion_boletin")]
    private bool? _publicacionBoletin = false;

    [ObservableProperty, JsonProperty("informacion_relevante")]
    private string? _informacionRelevante;

    [ObservableProperty, JsonProperty("pertenencia_colectivo")]
    private bool? _pertenenciaColectivo = false;

    [ObservableProperty, JsonProperty("participacion_previa_busquedas")]
    private bool? _participacionPreviaBusquedas = false;

    [ObservableProperty, JsonProperty("descripcion_participacion_busquedas")]
    private string? _descripcionParticipacionBusquedas;

    [ObservableProperty, JsonProperty("victima_extorsion_fraude")]
    private bool? _victimaExtorsionFraude = false;

    [ObservableProperty, JsonProperty("descripcion_extorsion_fraude")]
    private string? _descripcionExtorsionFraude;

    [ObservableProperty, JsonProperty("recibio_amenazas")]
    private bool? _recibioAmenazas = false;

    [ObservableProperty, JsonProperty("descripcion_origen_amenazas")]
    private string? _descripcionOrigenAmenazas;

    [ObservableProperty, JsonProperty("edad_estimada_anhos")]
    private int? _edadEstimadaAnhos;

    [ObservableProperty, JsonProperty("created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty("updated_at")]
    private DateTime? _updatedAt;
}