using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Pertenencia : ObservableObject
{
    [JsonConstructor]
    public Pertenencia(int id, string? nombre, Catalogo? grupoPertenencia)
    {
        Id = id;
        Nombre = nombre;
        GrupoPertenencia = grupoPertenencia;
    }

    public Pertenencia(Pertenencia pertenencia)
    {
        Id = pertenencia.Id;
        Nombre = pertenencia.Nombre;
        if (pertenencia?.GrupoPertenencia != null) GrupoPertenencia = new Catalogo(pertenencia.GrupoPertenencia);
    }
    
    public Pertenencia() { }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Pertenencia)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nombre, GrupoPertenencia);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nombre, GrupoPertenencia?.GetHashCode());
    }

    private bool Equals(Pertenencia pertenencia)
    {
        var grupoPertenencia = GrupoPertenencia?.Equals(pertenencia.GrupoPertenencia) ?? false;
        return Id == pertenencia.Id &&
               Nombre == pertenencia.Nombre &&
               grupoPertenencia;
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "grupo_pertenencia")]
    private Catalogo? _grupoPertenencia;
}