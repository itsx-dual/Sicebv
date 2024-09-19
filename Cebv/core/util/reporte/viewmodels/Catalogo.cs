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
        if (ReferenceEquals(this, obj)) return true;
        if (ReferenceEquals(obj, null)) return false;
        if (obj.GetType() != GetType()) return false;

        return Equals((Catalogo) obj);
    }

    public override int GetHashCode() //wao
    {
        return HashCode.Combine(Id, Nombre);
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