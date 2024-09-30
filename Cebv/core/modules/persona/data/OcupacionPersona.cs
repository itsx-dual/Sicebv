using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using static Cebv.core.util.enums.PrioridadOcupacion;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class OcupacionPersona : ObservableObject
{
    [JsonConstructor]
    public OcupacionPersona(
        int? id,
        Ocupacion? ocupacion,
        int? personaId,
        string? prioridad,
        string? observaciones
    )
    {
        Id = id;
        Ocupacion = ocupacion;
        PersonaId = personaId;
        Prioridad = prioridad;
        Observaciones = observaciones;
    }

    public OcupacionPersona()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("ocupacion")]
    private Ocupacion? _ocupacion;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("prioridad")]
    private string? _prioridad;

    [ObservableProperty, JsonProperty("observaciones")]
    private string? _observaciones;

    // --------------------------------------------------------------
    // Parametros por defecto de los diferentes prioirdades de ocupacion
    // --------------------------------------------------------------

    public readonly Dictionary<string, object> ParametrosPrincipal = new()
    {
        { nameof(Prioridad), Principal },
    };

    public readonly Dictionary<string, object> ParametrosSecundaria = new()
    {
        { nameof(Prioridad), Secundaria },
    };
}