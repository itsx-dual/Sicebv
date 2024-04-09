using System.Text.Json.Serialization;

namespace Cebv.features.reporte.data;

public class ZonaEstadoWrapped
{
    public List<ZonaEstado> data { get; set; }
}

public class ZonaEstado
{
    [property: JsonPropertyName("id")] public int Id { get; set; }
    [property: JsonPropertyName("nombre")] public string Nombre { get; set; }

    [property: JsonPropertyName("abreviatura")]
    public string Abreviatura { get; set; }
}