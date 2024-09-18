using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class CondicionSalud : ObservableObject
{
    [JsonConstructor]
    public CondicionSalud(
        int? id,
        int? personaId,
        Catalogo? tipoCondicionSalud,
        string? indoleSalud,
        string? tratamiento,
        string? observaciones
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoCondicionSalud = tipoCondicionSalud;
        IndoleSalud = indoleSalud;
        Tratamiento = tratamiento;
        Observaciones = observaciones;
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_condicion_salud")]
    private Catalogo? _tipoCondicionSalud;

    [ObservableProperty, JsonProperty("indole_salud")]
    private string? _indoleSalud;

    [ObservableProperty, JsonProperty("tratamiento")]
    private string? _tratamiento;

    [ObservableProperty, JsonProperty("observaciones")]
    private string? _observaciones;
}