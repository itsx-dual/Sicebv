using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Reportante : ObservableObject
{
    [JsonConstructor]
    public Reportante(int? id,
        int? reporteId,
        Persona? persona,
        Catalogo? parentesco,
        Catalogo? colectivo,
        bool denunciaAnonima,
        bool? informacionConsentimiento,
        bool? informacionExclusivaBusqueda,
        bool? publicacionRegistroNacional,
        bool? publicacionBoletin,
        bool? pertenenciaColectivo,
        string? participacionBusquedas,
        string? descripcionExtorsion,
        string? descripcionDondeProviene,
        string? informacionRelevante,
        int? edadEstimada,
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
        PertenenciaColectivo = pertenenciaColectivo;
        InformacionRelevante = informacionRelevante;
        ParticipacionBusquedas = participacionBusquedas;
        DescripcionExtorsion = descripcionExtorsion;
        DescripcionDondeProviene = descripcionDondeProviene;
        EdadEstimada = edadEstimada;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Reportante()
    {
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty(PropertyName = "persona")]
    private Persona? _persona;

    [ObservableProperty, JsonProperty(PropertyName = "colectivo")]
    private Catalogo? _colectivo;

    [ObservableProperty, JsonProperty(PropertyName = "pertenencia_colectivo")]
    private bool? _pertenenciaColectivo;

    [ObservableProperty, JsonProperty(PropertyName = "parentesco")]
    private Catalogo? _parentesco;

    [ObservableProperty, JsonProperty(PropertyName = "denuncia_anonima")]
    private bool _denunciaAnonima;

    [ObservableProperty, JsonProperty(PropertyName = "informacion_consentimiento")]
    private bool? _informacionConsentimiento;

    [ObservableProperty, JsonProperty(PropertyName = "informacion_exclusiva_busqueda")]
    private bool? _informacionExclusivaBusqueda;

    [ObservableProperty, JsonProperty(PropertyName = "publicacion_registro_nacional")]
    private bool? _publicacionRegistroNacional;

    [ObservableProperty, JsonProperty(PropertyName = "publicacion_boletin")]
    private bool? _publicacionBoletin;

    [ObservableProperty, JsonProperty(PropertyName = "informacion_relevante")]
    private string? _informacionRelevante;

    [ObservableProperty, JsonProperty(PropertyName = "participacion_busquedas")]
    private string? _participacionBusquedas;

    [ObservableProperty, JsonProperty(PropertyName = "descripcion_extorsion")]
    private string? _descripcionExtorsion;

    [ObservableProperty, JsonProperty(PropertyName = "descripcion_donde_proviene")]
    private string? _descripcionDondeProviene;

    [ObservableProperty, JsonProperty(PropertyName = "edad_estimada")]
    private int? _edadEstimada;

    [ObservableProperty, JsonProperty(PropertyName = "created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty(PropertyName = "updated_at")]
    private DateTime? _updatedAt;
}