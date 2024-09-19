using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Nariz : ObservableObject
{
    [JsonConstructor]
    public Nariz(
        int? id,
        int? personaId,
        Catalogo? tipoNariz,
        string? espesificacionesNariz
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoNariz = tipoNariz;
        EspesificacionesNariz = espesificacionesNariz;
    }

    public Nariz()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_nariz")]
    private Catalogo? _tipoNariz;

    [ObservableProperty, JsonProperty("especificaciones_nariz")]
    private string? _espesificacionesNariz;
}