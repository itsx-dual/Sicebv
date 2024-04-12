using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Cebv.features.desaparecido.data;
using Cebv.features.reportante.data;

namespace Cebv.features.reporte.data;

public class ReporteWrapped
{
    [property: JsonPropertyName("data")]
    public List<Reporte> Data { get; set; } = null!;
}

public class ReporteById
{
    public Reporte data { get; set; }
}

public sealed record Reporte
{
    [property: JsonPropertyName("id")] public int Id { get; set; }

    [property: JsonPropertyName("tipo_reporte_id")]
    public int? TipoReporteId { get; set; }

    [property: JsonPropertyName("area_atiende_id")]
    public int? AreaAtiendeId { get; set; }

    [property: JsonPropertyName("medio_conocimiento_id")]
    public int? MedioConocimientoId { get; set; }

    [property: JsonPropertyName("zona_estado_id")]
    public int? ZonaEstadoId { get; set; }

    [property: JsonPropertyName("hipotesis_oficial_id")]
    public int? HipotesisOficialId { get; set; }

    [property: JsonPropertyName("tipo_desaparicion")]
    public string? TipoDesaparicion { get; set; }

    [property: JsonPropertyName("fecha_localizacion")]
    public DateOnly? FechaLocalizacion { get; set; }

    [property: JsonPropertyName("sintesis_localizacion")]
    public string? SintesisLocalizacion { get; set; }

    [property: JsonPropertyName("clasificacion_persona")]
    public string? ClasificacionPersona { get; set; }

    [property: JsonPropertyName("reportantes")]
    public ObservableCollection<Reportante> Reportantes { get; set; }

    [property: JsonPropertyName("desaparecidos")]
    public List<Desaparecido>? Desaparecidos { get; set; }
}