using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class BasicResource : ObservableObject
{
    [JsonConstructor]
    public BasicResource(
        int? id,
        string? nombre,
        string? abreviatura
    )
    {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
    }

    public BasicResource()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty("abreviatura")]
    private string? _abreviatura;

    public override string ToString()
    {
        return $"{Nombre}";
    }
}