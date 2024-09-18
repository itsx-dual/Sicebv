using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Orejas : ObservableObject
{
    [JsonConstructor]
    public Orejas(
        int? id,
        int? personaId,
        Catalogo? tamanoOrejas,
        Catalogo? formaOrejas,
        string? espesificacionesOrejas
    )
    {
        Id = id;
        PersonaId = personaId;
        TamanoOrejas = tamanoOrejas;
        FormaOrejas = formaOrejas;
        EspesificacionesOrejas = espesificacionesOrejas;
    }

    public Orejas()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tamano_orejas")]
    private Catalogo? _tamanoOrejas;

    [ObservableProperty, JsonProperty("forma_orejas")]
    private Catalogo? _formaOrejas;

    [ObservableProperty, JsonProperty("especificaciones_orejas")]
    private string? _espesificacionesOrejas;
}