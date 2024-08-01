using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Catalogo : ObservableObject
{
    [JsonConstructor]
    public Catalogo(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public Catalogo() { }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Catalogo) obj);
    }

    private bool Equals(Catalogo catalogo)
    {
        return Id == catalogo.Id &&
               Nombre == catalogo.Nombre;
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] private string? _nombre;
}