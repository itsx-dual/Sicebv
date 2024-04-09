using System.Text.Json.Serialization;
using Cebv.features.persona.data;

namespace Cebv.features.reportante.data;

public class Reportante
{
    [property: JsonPropertyName("id")] public int Id { get; set; }

    [property: JsonPropertyName("reporte_id")]
    public int? ReporteId { get; set; }

    [property: JsonPropertyName("persona")]
    public Persona? Persona { get; set; }

    [property: JsonPropertyName("parentesco_id")]
    public int? ParentescoId { get; set; }

    [property: JsonPropertyName("denuncia_anonima")]
    public bool? DenunciaAnonima { get; set; }

    [property: JsonPropertyName("informacion_consentimiento")]
    public bool? InformacionConsentimiento { get; set; }

    [property: JsonPropertyName("informacion_exclusiva_busqueda")]
    public bool? InformacionExclusivaBusqueda { get; set; }

    [property: JsonPropertyName("publicacion_registro_nacional")]
    public bool? PublicacionRegistroNacional { get; set; }

    [property: JsonPropertyName("publicacion_boletin")]
    public bool? PublicacionBoletin { get; set; }

    [property: JsonPropertyName("pertenencia_colectivo")]
    public bool? PertenenciaColectivo { get; set; }

    [property: JsonPropertyName("nombre_colectivo")]
    public string? NombreColectivo { get; set; }

    [property: JsonPropertyName("informacion_relevante")]
    public string? InformacionRelevante { get; set; }

    [property: JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [property: JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}