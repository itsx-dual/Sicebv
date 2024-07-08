using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public class TiposHipotesisWrapped
{
    [JsonPropertyName("data")] public ObservableCollection<TipoHipotesis> Data { get; set; } = new();
}

public class TipoHipotesisWrapped
{
    [JsonPropertyName("data")] public TipoHipotesis Data { get; set; } = new();
}

public class TipoHipotesis
{
    [property: JsonPropertyName("id")] public int Id { get; set; }

    [property: JsonPropertyName("abreviatura")]
    public string Abreviatura { get; set; } = String.Empty;

    [property: JsonPropertyName("descripcion")]
    public string Descripcion { get; set; } = String.Empty;

    [property: JsonPropertyName("circunstancia")]
    public Circunstancia Circunstancia { get; set; }

    public override string ToString()
    {
        return Abreviatura + " - " + Descripcion;
    }
}

public class Circunstancia
{
}