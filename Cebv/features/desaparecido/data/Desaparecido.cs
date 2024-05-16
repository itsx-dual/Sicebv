using System.Text.Json.Serialization;
using Cebv.features.persona.data;

namespace Cebv.features.desaparecido.data;

public sealed record DesaparecidoResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("reporte_id")] public int ReporteId { get; set; }

    [JsonPropertyName("persona")] public Persona Persona { get; set; } = null!;

    [JsonPropertyName("estatus_rpdno")] public EstatusPersona? EstatusRpdno { get; set; }

    [JsonPropertyName("estatus_cebv")] public EstatusPersona? EstatusCebv { get; set; }

    [JsonPropertyName("clasificacion_persona")]
    public string? ClasificacionPersona { get; set; }

    [JsonPropertyName("habla_espanhol")] public bool HablaEspanhol { get; set; }

    [JsonPropertyName("sabe_leer")] public bool SabeLeer { get; set; }

    [JsonPropertyName("sabe_escribir")] public bool SabeEscribir { get; set; }

    [JsonPropertyName("url_boletin")] public string? UrlBoletin { get; set; }

    [JsonPropertyName("amparo_buscador")] public string? AmparoBuscador { get; set; }

    [JsonPropertyName("ubicacion_amparo_buscador")]
    public string? UbicacionAmparoBuscador { get; set; }

    [JsonPropertyName("nombre_juez")] public string? NombreJuez { get; set; }

    [JsonPropertyName("fecha_amparo")] public string? FechaAmparo { get; set; }

    [JsonPropertyName("derechos_humanos")] public string? DerechosHumanos { get; set; }

    [JsonPropertyName("folio_cebv")] public string? FolioCebv { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = null!;

    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = null!;
}

public class DesaparecidoRequest
{
    [JsonPropertyName("reporte_id")] public int ReporteId { get; set; }

    [JsonPropertyName("persona_id")] public int PersonaId { get; set; }

    [JsonPropertyName("estatus_rpdno_id")] public int? EstatusRpdnoId { get; set; }

    [JsonPropertyName("estatus_cebv_id")] public int? EstatusCebvId { get; set; }

    [JsonPropertyName("clasificacion_persona")]
    public string? ClasificacionPersona { get; set; }

    [JsonPropertyName("habla_espanhol")] public bool? HablaEspanhol { get; set; }

    [JsonPropertyName("sabe_leer")] public bool? SabeLeer { get; set; }

    [JsonPropertyName("sabe_escribir")] public bool? SabeEscribir { get; set; }

    [JsonPropertyName("url_boletin")] public string? UrlBoletin { get; set; }

    [JsonPropertyName("amparo_buscador")] public string? AmparoBuscador { get; set; }

    [JsonPropertyName("ubicacion_amparo_buscador")]
    public string? UbicacionAmparoBuscador { get; set; }

    [JsonPropertyName("nombre_juez")] public string? NombreJuez { get; set; }

    [JsonPropertyName("fecha_amparo")] public string? FechaAmparo { get; set; }

    [JsonPropertyName("derechos_humanos")] public string? DerechosHumanos { get; set; }

    [JsonPropertyName("folio_cebv")] public string? FolioCebv { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = null!;

    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = null!;
}

public class DocumentoLegal
{
    [property: JsonPropertyName("tipo_documento")]
    public string? TipoDocumento { get; set; }

    [property: JsonPropertyName("numero_documento")]
    public string? NumeroDocumento { get; set; }

    [property: JsonPropertyName("donde_radica")]
    public string? DondeRadica { get; set; }

    [property: JsonPropertyName("nombre_servidor_publico")]
    public string? NombreServidorPublico { get; set; }

    [property: JsonPropertyName("fecha_recepcion")]
    public DateTime? FechaRecepcion { get; set; }
}