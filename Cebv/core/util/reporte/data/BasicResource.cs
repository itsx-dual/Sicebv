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
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (ReferenceEquals(obj, null)) return false;
        if (obj.GetType() != GetType()) return false;

        return Equals((BasicResource) obj);
    }

    public override int GetHashCode() //wao
    {
        return HashCode.Combine(Id, Nombre, Abreviatura);
    }

    private bool Equals(BasicResource basicResource)
    {
        return Id == basicResource.Id &&
               Nombre == basicResource.Nombre &&
               Abreviatura == basicResource.Abreviatura;
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }
}