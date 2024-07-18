using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Pertenencia : ObservableObject
{
    [JsonConstructor]
    public Pertenencia(int id, string? nombre, Catalogo? grupo_pertenencia)
    {
        Id = id;
        Nombre = nombre;
        GrupoPertenencia = grupo_pertenencia;
    }
    
    public Pertenencia() { }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Pertenencia) obj);
    }

    private bool Equals(Pertenencia pertenencia)
    {
        return Id == pertenencia.Id &&
               Nombre == pertenencia.Nombre &&
               (bool) GrupoPertenencia?.Equals(pertenencia.GrupoPertenencia);
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] private string? _nombre;
    [ObservableProperty, JsonProperty(PropertyName = "grupo_pertenencia")] private Catalogo? _grupoPertenencia;
}