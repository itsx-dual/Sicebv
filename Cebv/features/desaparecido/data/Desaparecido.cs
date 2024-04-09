using System.Text.Json.Serialization;
using Cebv.features.persona.data;

namespace Cebv.features.desaparecido.data;

public sealed record Desaparecido
{
    [property: JsonPropertyName("id")] public int? Id { get; set; }

    [property: JsonPropertyName("reporte_id")]
    public int? ReporteId { get; set; }

    [property: JsonPropertyName("persona")]
    public Persona? Persona { get; set; }

    [property: JsonPropertyName("habla_espanhol")]
    public bool? HablaEspanhol { get; set; }

    [property: JsonPropertyName("sabe_leer")]
    public bool? SabeLeer { get; set; }

    [property: JsonPropertyName("sabe_escribir")]
    public bool? SabeEscribir { get; set; }

    [property: JsonPropertyName("url_boletin")]
    public string? UrlBoletin { get; set; }

    [property: JsonPropertyName("folio")] public string? Folio { get; set; }
}