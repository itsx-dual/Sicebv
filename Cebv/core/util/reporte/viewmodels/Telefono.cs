using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Telefono : ObservableValidator
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
    [Required(ErrorMessage = "Se debe seleccionar una compania de telefono.")]
    private Catalogo? _compania;

    [ObservableProperty, JsonProperty(PropertyName = "numero")]
    [Required(ErrorMessage = "Se necesita un numero de telefono para poder agregar.")]
    [MinLength(8, ErrorMessage = "El numero de telefono debe contener al menos 8 caracteres")]
    private string? _numero;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones")]
    private string? _observaciones;

    [ObservableProperty, JsonProperty(PropertyName = "es_movil")]
    private bool? _esMovil;

    // --------------------------------------------------------------
    // Parametros por defecto de los diferentes tipos de documentos
    // --------------------------------------------------------------
    public readonly Dictionary<string, object> TelefonoMovilDefault = new()
    {
        { nameof(EsMovil), true },
    };
    
    public readonly Dictionary<string, object> TelefonoFijoDefault = new()
    {
        { nameof(EsMovil), false },
    };

    public void ValidarReportante()
    {
        ValidateProperty(Numero, nameof(Numero));
    }

    public void ValidarDesaparecido()
    {
        ValidateProperty(Numero, nameof(Numero));
        ValidateProperty(Compania, nameof(Compania));
    }
}