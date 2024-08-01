using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Cebv.core.modules.ubicacion.data;

public class EstadosWrapped
{
    [Newtonsoft.Json.JsonConstructor]
    public EstadosWrapped(List<util.reporte.viewmodels.Estado> data)
    {
        Data = data;
    }

    public List<util.reporte.viewmodels.Estado> Data;
}

public class Estado
{
    [property: JsonPropertyName("id")] public string Id { get; set; }
    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }

    [property: JsonPropertyName("abreviatura_inegi")]
    public string? AbreviaturaInegi { get; set; }

    [property: JsonPropertyName("abreviatura_cebv")]
    public string? AbreviaturaCebv { get; set; }
}