using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Cebv.features.formulario_cebv.folio_expediente.data;


public class FoliosWrapped
{
    [JsonPropertyName("data")] public ObservableCollection<Folio> Data { get; set; } = new();
}
public class Folio
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("persona_id")] public int PersonaId { get; set; }
    [JsonPropertyName("reporte_id")] public int ReporteId { get; set; }
    [JsonPropertyName("user")] public User User { get; set; } = new();
    [JsonPropertyName("folio_cebv")] public string FolioCebv { get; set; } = String.Empty;
    [JsonPropertyName("folio_fub")] public string FolioCFub { get; set; } = String.Empty;
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
}