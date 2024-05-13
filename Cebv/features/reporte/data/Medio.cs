using System.Text.Json.Serialization;
using Cebv.core.data;

namespace Cebv.features.reporte.data;

public class MedioWrapped
{
    [property: JsonPropertyName("data")] public List<Medio> Data { get; set; } = null!;
}

public class Medio
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("tipo_medio")] public Catalogo TipoMedio { get; set; } = null!;

    [JsonPropertyName("nombre")] public string Nombre { get; set; } = null!;
}