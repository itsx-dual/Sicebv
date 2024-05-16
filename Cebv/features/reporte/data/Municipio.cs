using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.reporte.data;

public class MunicipiosWrapped
{
    [property: JsonPropertyName("data")] public ObservableCollection<Municipio> Data { get; set; }
}

public class Municipio
{
    [property: JsonPropertyName("id")] public string Id { get; set; }

    [property: JsonPropertyName("estado_id")]
    public string? EstadoId { get; set; }

    [property: JsonPropertyName("estado_nombre")]
    public string? EstadoNombre { get; set; }

    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }
    
    public override string ToString()
    {
        return Id + " - " + Nombre;
    }
}