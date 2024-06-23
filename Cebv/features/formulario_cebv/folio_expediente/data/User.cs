using System.Text.Json.Serialization;

namespace Cebv.features.formulario_cebv.folio_expediente.data;

public class User
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = String.Empty;
    [JsonPropertyName("email")] public string Email { get; set; } = String.Empty;
    [JsonPropertyName("status")] public string Status { get; set; } = String.Empty;
}