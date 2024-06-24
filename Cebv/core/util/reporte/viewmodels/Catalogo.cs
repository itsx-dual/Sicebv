using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.util.reporte.viewmodels;

public partial class Catalogo : ObservableObject
{
    [JsonConstructor]
    public Catalogo(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

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

    [ObservableProperty] private int? _id;
    [ObservableProperty] private string? _nombre;
}