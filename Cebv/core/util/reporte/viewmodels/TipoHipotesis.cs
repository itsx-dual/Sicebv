using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class TipoHipotesis : ObservableObject
{
    [JsonConstructor]
    public TipoHipotesis(
        int? id,
        string? abreviatura,
        string? descripcion,
        Circunstancia? circunstancia)
    {
        Id = id;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
        Circunstancia = circunstancia;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")] int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "abreviatura")] string? _abreviatura;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] string? _descripcion;
    [ObservableProperty, JsonProperty(PropertyName = "circunstancia")] Circunstancia? _circunstancia;

    public override string ToString()
    {
        return $"{Abreviatura} - {Descripcion}";
    }
}

[JsonObject(MemberSerialization.OptIn)]
public partial class Circunstancia : ObservableObject
{
    [JsonConstructor]
    public Circunstancia(int? id, string? descripcion)
    {
        Id = id;
        Descripcion = descripcion;
    }
    public Circunstancia()
    {
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] private string? _descripcion;
}