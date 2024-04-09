using System.Text.Json.Serialization;

namespace Cebv.features.reporte.data;

public class EstadoWrapped
{
    public List<Estado> data { get; set; }
}

public class Estado
{
    [property: JsonPropertyName("id")] public string Id { get; set; }
    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }

    [property: JsonPropertyName("abreviatura_inegi")]
    public string? AbreviaturaInegi { get; set; }

    [property: JsonPropertyName("abreviatura_cebv")]
    public string? AbreviaturaCebv { get; set; }
}