using System.Text.Json.Serialization;

namespace Cebv.features.reporte.data;

public class TipoHipotesisWrapped
{
    public List<TipoHipotesis> data { get; set; }
}

public class TipoHipotesis
{
    [property: JsonPropertyName("id")] public int Id { get; set; }

    [property: JsonPropertyName("abreviatura")]
    public string Abreviatura { get; set; }

    [property: JsonPropertyName("descripcion")]
    public string Descripcion { get; set; }

    [property: JsonPropertyName("circunstancia_id")]
    public int CircunstanciaId { get; set; }
}