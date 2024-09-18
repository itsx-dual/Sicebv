using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Cebv.core.data;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.util.reporte.data;

namespace Cebv.core.modules.reporte.data;

public class ReportesQueryResponse
{
    public ReportesQueryResponse(ObservableCollection<ReporteResponse>? data)
    {
        Data = data;
    }

    [property: JsonPropertyName("data")] public ObservableCollection<ReporteResponse>? Data { get; set; }
}

public class ReporteQueryResponse
{
    [property: JsonPropertyName("data")] public ReporteResponse? Data { get; set; }
}

public class ReporteResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("tipo_reporte")] public Catalogo? TipoReporte { get; set; }

    [JsonPropertyName("area_atiende")] public Catalogo? AreaAtiende { get; set; }

    [JsonPropertyName("medio_conocimiento")]
    public Medio? MedioConocimiento { get; set; }

    [JsonPropertyName("estado")] public Estado? Estado { get; set; }

    [JsonPropertyName("zona_estado")] public BasicResource? ZonaEstado { get; set; }

    [JsonPropertyName("hipotesis_oficial_id")]
    public int? HipotesisOficialId { get; set; }

    [JsonPropertyName("tipo_desaparicion")]
    public string? TipoDesaparicion { get; set; }

    //[JsonPropertyName("reportantes")] public List<ReportanteResponse>? Reportantes { get; set; }

    [JsonPropertyName("desaparecidos")] public List<DesaparecidoResponse>? Desaparecidos { get; set; }

    [JsonPropertyName("fecha_creacion")] public DateTime? FechaCreacion { get; set; }

    [JsonPropertyName("fecha_actualizacion")]
    public string FechaActualizacion { get; set; } = null!;
}

public class ReporteRequest
{
    [JsonPropertyName("tipo_reporte")] public Catalogo? TipoReporte { get; set; }

    [JsonPropertyName("area_atiende_id")] public int? AreaAtiendeId { get; set; }

    [JsonPropertyName("medio_conocimiento")]
    public Medio? MedioConocimiento { get; set; }

    [JsonPropertyName("estado")] public Estado? Estado { get; set; }

    [JsonPropertyName("zona_estado_id")] public int? ZonaEstadoId { get; set; }

    [JsonPropertyName("hipotesis_oficial_id")]
    public int? HipotesisOficialId { get; set; }

    [JsonPropertyName("tipo_desaparicion")]
    public string? TipoDesaparicion { get; set; }

    [JsonPropertyName("fecha_localizacion")]
    public DateTime? FechaLocalizacion { get; set; }

    [JsonPropertyName("sintesis_localizacion")]
    public string? SintesisLocalizacion { get; set; }

    //[JsonPropertyName("reportantes")] public List<ReportanteResponse>? Reportantes { get; set; }

    [JsonPropertyName("desaparecidos")] public List<DesaparecidoResponse>? Desaparecidos { get; set; }
}