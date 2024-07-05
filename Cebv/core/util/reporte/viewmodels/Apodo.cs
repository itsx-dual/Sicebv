using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Apodo : ObservableObject
{
    [JsonConstructor]
    public Apodo(int id, int persona_id, string apodo)
    {
        Id = id;
        PersonaId = persona_id;
        ApodoNombre = apodo;
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
               ApodoNombre == apodo.ApodoNombre;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int? _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int? _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "apodo")] private string? _apodoNombre;
}