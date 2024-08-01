using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Apodo : ObservableObject
{
    [JsonConstructor]
    public Apodo(int id, int? persona_id, string? nombre, string? apellido_paterno, string? apellido_materno)
    {
        Id = id;
        PersonaId = persona_id;
        Nombre = nombre;
        ApellidoPaterno = apellido_paterno;
        ApellidoMaterno = apellido_materno;
    }

    public Apodo() { }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Apodo) obj);
    }

    private bool Equals(Apodo apodo)
    {
        return Id == apodo.Id &&
               PersonaId == apodo.PersonaId &&
               Nombre == apodo.Nombre &&
               ApellidoPaterno == apodo.ApellidoPaterno &&
               ApellidoMaterno == apodo.ApellidoMaterno;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int? _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] private string? _nombre;
    [ObservableProperty, JsonProperty(PropertyName = "apellido_paterno")] private string? _apellidoPaterno;
    [ObservableProperty, JsonProperty(PropertyName = "apellido_materno")] private string? _apellidoMaterno;
}