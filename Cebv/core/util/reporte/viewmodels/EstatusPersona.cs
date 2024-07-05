using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class EstatusPersona : ObservableObject
{
    [JsonConstructor]
    public EstatusPersona(
        int? id,
        string? nombre,
        string? abreviatura)
    {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")] int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] string? _nombre;
    [ObservableProperty, JsonProperty(PropertyName = "abreviatura")] string? _abreviatura;
}