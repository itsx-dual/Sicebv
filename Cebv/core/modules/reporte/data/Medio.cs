using System.Text.Json.Serialization;
using Cebv.core.data;

namespace Cebv.core.modules.reporte.data;

public class MediosWrapped
{
    [property: JsonPropertyName("data")] public List<Medio> Data { get; set; } = null!;
}

public class Medio
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("tipo_medio")] public Catalogo TipoMedio { get; set; } = null!;

    [JsonPropertyName("nombre")] public string Nombre { get; set; } = null!;
}