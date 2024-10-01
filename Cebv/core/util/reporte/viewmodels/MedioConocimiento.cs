using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class MedioConocimiento : ObservableObject
{
    [JsonConstructor]
    public MedioConocimiento(
        int? id,
        Catalogo tipoMedio,
        string? nombre)
    {
        Id = id;
        TipoMedio = tipoMedio;
        Nombre = nombre;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((MedioConocimiento)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, TipoMedio, Nombre);
    }

    private bool Equals(MedioConocimiento medio)
    {
        return Id == medio.Id &&
               Nombre == medio.Nombre;
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_medio")]
    private Catalogo _tipoMedio;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;
}