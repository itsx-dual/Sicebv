using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class TipoHipotesis : ObservableObject
{
    [JsonConstructor]
    public TipoHipotesis(int? id,
        string? abreviatura,
        string? descripcion,
        (int Id, int Descripcion)? circunstancia)
    {
        Id = id;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
        Circunstancia = circunstancia;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")] int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "abreviatura")] string? _abreviatura;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] string? _descripcion;
    [ObservableProperty, JsonProperty(PropertyName = "circunstancia")] (int Id, int Descripcion)? _circunstancia;
    
    
}