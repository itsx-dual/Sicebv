using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Ocupacion : ObservableObject
{
    [JsonConstructor]
    public Ocupacion(int id, string? nombre, Catalogo? tipoOcupacion)
    {
        Id = id;
        Nombre = nombre;
        TipoOcupacion = tipoOcupacion;
    }

    public Ocupacion()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Ocupacion)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nombre, TipoOcupacion);
    }

    private bool Equals(Ocupacion ocupacion)
    {
        return Id == ocupacion.Id &&
               Nombre == ocupacion.Nombre;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_ocupacion")]
    private Catalogo? _tipoOcupacion;
}