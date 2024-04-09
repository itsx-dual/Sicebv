using System.Text.Json.Serialization;

namespace Cebv.features.reporte.data;

public class MedioWrapped
{
    public List<Medio> data { get; set; }
}

public class Medio
{
    [property: JsonPropertyName("id")] public int Id { get; set; }

    [property: JsonPropertyName("tipo_medio_id")]
    public int TipoMedioId { get; set; }

    [property: JsonPropertyName("nombre")] public string Nombre { get; set; }
}