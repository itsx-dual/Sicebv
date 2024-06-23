using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Cebv.core.util.reporte.domain;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public class HechosDesaparicionQueryResponse
{
    [JsonPropertyName("data")] public ObservableCollection<HechosDesaparicionResponse>? Data { get; set; }
}

public class HechosDesaparicionResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("reporte_id")] public int ReporteId { get; set; }
    [JsonPropertyName("fecha_desaparicion")] public DateTime? FechaDesaparicion { get; set; }
    [JsonPropertyName("fecha_desaparicion_cebv")] public string? FechaDesaparicionCebv { get; set; }
    [JsonPropertyName("fecha_percato")] public DateTime? FechaPercato { get; set; }
    [JsonPropertyName("fecha_percato_cebv")] public string? FechaPercatoCebv { get; set; }
    [JsonPropertyName("aclaraciones_fecha_hechos")] public string? AclaracionesFechaHechos { get; set; }
    [JsonPropertyName("cambio_comportamiento")] public bool? CambioComportamiento { get; set; }
    [JsonPropertyName("descripcion_cambio_comportamiento")] public string? DescripcionCambioComportamiento { get; set; }
    [JsonPropertyName("fue_amenazado")] public bool? FueAmenazado { get; set; }
    [JsonPropertyName("descripcion_amenaza")] public string? DescripcionAmenaza { get; set; }
    [JsonPropertyName("contador_desapariciones")] public int ContadorDesapariciones { get; set; }
    [JsonPropertyName("situacion_previa")] public string? SituacionPrevia { get; set; }
    [JsonPropertyName("informacion_relevante")] public string? InformacionRelevante { get; set; }
    [JsonPropertyName("hechos_desaparicion")] public string? HechosDesaparicion { get; set; }
    [JsonPropertyName("sintesis_desaparicion")] public string? SintesisDesaparicion { get; set; }
    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
    
}