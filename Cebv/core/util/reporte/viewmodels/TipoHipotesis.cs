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
        Catalogo? circunstancia)
    {
        Id = id;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
        Circunstancia = circunstancia;
    }

    public TipoHipotesis()
    {
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "abreviatura")]
    string? _abreviatura;

    [ObservableProperty, JsonProperty(PropertyName = "descripcion")]
    string? _descripcion;

    [ObservableProperty, JsonProperty(PropertyName = "circunstancia")]
    Catalogo? _circunstancia;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((TipoHipotesis)obj);
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