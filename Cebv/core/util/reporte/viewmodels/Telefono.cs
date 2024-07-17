using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Telefono : ObservableObject
{
    [JsonConstructor]
    public Telefono(
        int id,
        int? persona_id,
        string? numero,
        string? observaciones,
        bool? es_movil,
        Catalogo? compania)
    {
        Id = id;
        PersonaId = persona_id;
        Numero = numero;
        Observaciones = observaciones;
        EsMovil = es_movil;
        Compania = compania;
    }

    public Telefono() { }

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
               Numero ==  telefono.Numero &&
               Observaciones == telefono.Observaciones &&
               EsMovil == telefono.EsMovil;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;

    [ObservableProperty, JsonProperty(PropertyName = "persona_id")]
    private int? _personaId;
    
    [ObservableProperty, JsonProperty(PropertyName = "numero")]
    private string? _numero;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones")]
    private string? _observaciones;
    
    [ObservableProperty, JsonProperty(PropertyName = "es_movil")]
    private bool? _esMovil;

    [ObservableProperty, JsonProperty(PropertyName = "compania")]
    private Catalogo? _compania;
}