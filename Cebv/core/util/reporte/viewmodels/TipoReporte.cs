using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class TipoReporte : ObservableObject
{
    [JsonConstructor]
    public TipoReporte(int? id, string? nombre, string? abreviatura)
    {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;
    
    [ObservableProperty, JsonProperty(PropertyName = "abreviatura")]
    private string? _abreviatura;
}