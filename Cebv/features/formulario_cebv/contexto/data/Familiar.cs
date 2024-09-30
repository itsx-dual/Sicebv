using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.contexto.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Familiar : ObservableObject
{
    [JsonConstructor]
    public Familiar(
        int? id,
        int? personaId,
        Catalogo? parentesco,
        string? nombre,
        bool haEjercidoViolencia,
        bool esFamiliarCercano,
        string? observaciones
    )
    {
        Id = id;
        PersonaId = personaId;
        Parentesco = parentesco;
        Nombre = nombre;
        HaEjercidoViolencia = haEjercidoViolencia;
        EsFamiliarCercano = esFamiliarCercano;
        Observaciones = observaciones;
    }

    public Familiar()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("parentesco")]
    private Catalogo? _parentesco;

    [ObservableProperty, JsonProperty("nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty("ha_ejercido_violencia")]
    private bool _haEjercidoViolencia;

    [ObservableProperty, JsonProperty("es_familiar_cercano")]
    private bool _esFamiliarCercano;

    [ObservableProperty, JsonProperty("observaciones")]
    private string? _observaciones;
}