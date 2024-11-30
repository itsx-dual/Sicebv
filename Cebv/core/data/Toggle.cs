using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Toggle : ObservableObject
{
    [JsonConstructor]
    public Toggle(bool esFavorito)
    {
        _esFavorito = esFavorito;
    }

    public Toggle()
    {
        
    }

    [ObservableProperty, JsonProperty("es_favorito")]
    private bool _esFavorito;
    
}