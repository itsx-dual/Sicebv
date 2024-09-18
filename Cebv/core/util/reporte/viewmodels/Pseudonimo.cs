using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Pseudonimo : ObservableObject
{
    [JsonConstructor]
    public Pseudonimo(
        int? id,
        int? personaId,
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno
    )
    {
        Id = id;
        PersonaId = personaId;
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
    }

    public Pseudonimo()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Pseudonimo)obj);
    }

    private bool Equals(Pseudonimo pseudonimo)
    {
        return Id == pseudonimo.Id &&
               PersonaId == pseudonimo.PersonaId &&
               Nombre == pseudonimo.Nombre &&
               ApellidoPaterno == pseudonimo.ApellidoPaterno &&
               ApellidoMaterno == pseudonimo.ApellidoMaterno;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_paterno")]
    private string? _apellidoPaterno;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_materno")]
    private string? _apellidoMaterno;
}