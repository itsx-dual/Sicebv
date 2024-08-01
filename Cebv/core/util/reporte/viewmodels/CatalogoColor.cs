using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class CatalogoColor : ObservableObject
{
    [JsonConstructor]
    public CatalogoColor(int id, string nombre, string color)
    {
        Id = id;
        Nombre = nombre;
        Color = color;
    }

    public CatalogoColor() { }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((CatalogoColor) obj);
    }

    private bool Equals(CatalogoColor catalogo)
    {
        return Id == catalogo.Id &&
               Nombre == catalogo.Nombre &&
               Color == catalogo.Color;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] private string? _nombre;
    [ObservableProperty, JsonProperty(PropertyName = "color")] private string? _color;
}