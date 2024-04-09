using System.Text.Json.Serialization;

namespace Cebv.core.data;

public class CatalogoWrapped
{
    public List<Catalogo> data { get; set; }
}

public class Catalogo
{
    [property: JsonPropertyName("id")] public int? Id { get; set; }
    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }
}