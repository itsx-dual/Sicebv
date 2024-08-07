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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((TipoHipotesis) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Abreviatura, Descripcion, Circunstancia);
    }
    
    private bool Equals(TipoHipotesis hipotesis)
    {
        return Id == hipotesis.Id &&
               Abreviatura == hipotesis.Abreviatura &&
               Descripcion == hipotesis.Descripcion &&
               Equals(Circunstancia, hipotesis.Circunstancia);
    }

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
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Circunstancia) obj);
    }

    protected bool Equals(Circunstancia circunstancia)
    {
        return Id == circunstancia.Id && 
               Descripcion == circunstancia.Descripcion;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Descripcion);
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] private string? _descripcion;
}