using System.Text.Json.Serialization;

namespace Cebv.features.persona.data;

public class ApodoResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("persona_id")] public int PersonaId { get; set; }
    [JsonPropertyName("apodo")] public string Apodo { get; set; } = null!;
}