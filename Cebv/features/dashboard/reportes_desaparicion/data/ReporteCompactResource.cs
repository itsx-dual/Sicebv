using Newtonsoft.Json;

namespace Cebv.features.dashboard.reportes_desaparicion.data;

[JsonObject]
public class ReporteCompactResource
{
    [System.Text.Json.Serialization.JsonConstructor]
    public ReporteCompactResource(
        int id,
        string? medio_conocimiento_generico, 
        string? medio_conocimiento_especifico, 
        string? tipo_reporte, 
        DateTime? fecha_creacion, 
        string? estado, 
        string? abreviatura_estado_cebv, 
        string? tipo_medio,    
        List<DesaparecidoCompactResource>? desaparecidos    
        )
    {
        Id = id;
        MedioConocimientoGenerico = medio_conocimiento_generico;
        MedioConocimientoEspecifico = medio_conocimiento_especifico;
        TipoReporte = tipo_reporte;
        FechaCreacion = fecha_creacion;
        Estado = estado;
        AbreviaturaEstadoCebv = abreviatura_estado_cebv;
        TipoMedio = tipo_medio;
        Desaparecidos = desaparecidos;
    }
    
    public ReporteCompactResource() { }
    
    [JsonProperty(PropertyName = "id")] 
    public int Id { get; set; }
    
    [JsonProperty(PropertyName = "medio_conocimiento_generico")] 
    public string? MedioConocimientoGenerico { get; set; }
    
    [JsonProperty(PropertyName = "medio_conocimiento_especifico")] 
    public string? MedioConocimientoEspecifico { get; set; }
    
    [JsonProperty(PropertyName = "tipo_reporte")] 
    public string? TipoReporte { get; set; }
    
    [JsonProperty(PropertyName = "fecha_creacion")] 
    public DateTime? FechaCreacion { get; set; }
    
    [JsonProperty(PropertyName = "estado")] 
    public string? Estado { get; set; }
    
    [JsonProperty(PropertyName = "abreviatura_estado_cebv")] 
    public string? AbreviaturaEstadoCebv { get; set; }
    
    [JsonProperty(PropertyName = "tipo_medio")] 
    public string? TipoMedio { get; set; }
    
    [JsonProperty(PropertyName = "desaparecidos")] 
    public List<DesaparecidoCompactResource>? Desaparecidos { get; set; }
}