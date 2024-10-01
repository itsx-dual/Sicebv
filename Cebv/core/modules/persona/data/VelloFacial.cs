using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class VelloFacial : ObservableObject
{
    [JsonConstructor]
    public VelloFacial(
        int? id,
        int? personaId,
        Catalogo? cejas,
        string? especificacionesCejas,
        bool? tieneBigote,
        string? espescificacionesBigote,
        bool? tieneBarba,
        string? especificacionesBarba
    )
    {
        Id = id;
        PersonaId = personaId;
        Cejas = cejas;
        EspecificacionesCejas = especificacionesCejas;
        TieneBigote = tieneBigote;
        EspescificacionesBigote = espescificacionesBigote;
        TieneBarba = tieneBarba;
        EspecificacionesBarba = especificacionesBarba;
    }

    public VelloFacial()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("cejas")]
    private Catalogo? _cejas;

    [ObservableProperty, JsonProperty("especificaciones_cejas")]
    private string? _especificacionesCejas;

    [ObservableProperty, JsonProperty("tiene_bigote")]
    private bool? _tieneBigote = false;

    [ObservableProperty, JsonProperty("especificaciones_bigote")]
    private string? _espescificacionesBigote;

    [ObservableProperty, JsonProperty("tiene_barba")]
    private bool? _tieneBarba = false;

    [ObservableProperty, JsonProperty("especificaciones_barba")]
    private string? _especificacionesBarba;
}