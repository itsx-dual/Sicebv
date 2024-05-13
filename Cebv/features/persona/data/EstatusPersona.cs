using System.Text.Json.Serialization;

namespace Cebv.features.persona.data;

public class EstatusPersona
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("nombre")] public string Nombre { get; set; } = null!;
    [JsonPropertyName("abreviatura")] public string Abreviatura { get; set; } = null!;
}