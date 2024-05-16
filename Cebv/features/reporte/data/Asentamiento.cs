using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Cebv.features.reporte.data;

public class AsentamientosWrapped
{
    [property: JsonPropertyName("data")] public ObservableCollection<Asentamiento> Data { get; set; }
}

public class Asentamiento
{
    [property: JsonPropertyName("id")] public string? Id { get; set; }

    [property: JsonPropertyName("muncipio_id")]
    public string? MuncipioId { get; set; }

    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }
    [property: JsonPropertyName("ambito")] public string? Ambito { get; set; }

    [property: JsonPropertyName("latitud")]
    public float? Latitud { get; set; }

    [property: JsonPropertyName("longitud")]
    public float? Longitud { get; set; }

    [property: JsonPropertyName("altitud")]
    public int? Altitud { get; set; }
}