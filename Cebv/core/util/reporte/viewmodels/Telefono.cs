using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Telefono : ObservableObject
{
    [JsonConstructor]
    public Telefono(
        int id,
        int? personaId,
        Catalogo? compania,
        string? numero,
        string? observaciones,
        bool? esMovil
    )
    {
        Id = id;
        PersonaId = personaId;
        Compania = compania;
        Numero = numero;
        Observaciones = observaciones;
        EsMovil = esMovil;
    }

    public Telefono()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((Telefono)obj);
    }

    private bool Equals(Telefono telefono)
    {
        return Id == telefono.Id &&
               PersonaId == telefono.PersonaId &&
               Numero == telefono.Numero &&
               Observaciones == telefono.Observaciones &&
               EsMovil == telefono.EsMovil;
    }
    
    public override int GetHashCode() //wao
    {
        return HashCode.Combine(Id, PersonaId, Numero, Observaciones, EsMovil, Compania);
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty(PropertyName = "compania")]
    private Catalogo? _compania;

    [ObservableProperty, JsonProperty(PropertyName = "numero")]
    private string? _numero;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones")]
    private string? _observaciones;

    [ObservableProperty, JsonProperty(PropertyName = "es_movil")]
    private bool? _esMovil;
}