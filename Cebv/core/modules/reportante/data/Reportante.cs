using System.Text.Json.Serialization;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.core.modules.reportante.data;

public class ReportanteResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reporte_id")]
    public int? ReporteId { get; set; }

    [JsonPropertyName("persona")]
    public Persona? Persona { get; set; }

    [JsonPropertyName("parentesco_id")]
    public int? ParentescoId { get; set; }

    [JsonPropertyName("denuncia_anonima")]
    public bool? DenunciaAnonima { get; set; }

    [JsonPropertyName("informacion_consentimiento")]
    public bool? InformacionConsentimiento { get; set; }

    [JsonPropertyName("informacion_exclusiva_busqueda")]
    public bool? InformacionExclusivaBusqueda { get; set; }

    [JsonPropertyName("publicacion_registro_nacional")]
    public bool? PublicacionRegistroNacional { get; set; }

    [JsonPropertyName("publicacion_boletin")]
    public bool? PublicacionBoletin { get; set; }

    [JsonPropertyName("pertenencia_colectivo")]
    public bool? PertenenciaColectivo { get; set; }

    [JsonPropertyName("nombre_colectivo")]
    public string? NombreColectivo { get; set; }

    [JsonPropertyName("informacion_relevante")]
    public string? InformacionRelevante { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; } = null!;

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; } = null!;
}

public class ReportanteRequest
{

    [JsonPropertyName("parentesco_id")]
    public int ParentescoId { get; set; }

    [JsonPropertyName("denuncia_anonima")]
    public bool DenunciaAnonima { get; set; }

    [JsonPropertyName("informacion_consentimiento")]
    public bool? InformacionConsentimiento { get; set; }

    [JsonPropertyName("informacion_exclusiva_busqueda")]
    public bool? InformacionExclusivaBusqueda { get; set; }

    [JsonPropertyName("publicacion_registro_nacional")]
    public bool? PublicacionRegistroNacional { get; set; }

    [JsonPropertyName("publicacion_boletin")]
    public bool? PublicacionBoletin { get; set; }

    [JsonPropertyName("pertenencia_colectivo")]
    public bool? PertenenciaColectivo { get; set; }

    [JsonPropertyName("nombre_colectivo")]
    public string? NombreColectivo { get; set; }

    [JsonPropertyName("informacion_relevante")]
    public string? InformacionRelevante { get; set; }
}