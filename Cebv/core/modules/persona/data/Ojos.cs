using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Ojos : ObservableObject
{
    [JsonConstructor]
    public Ojos(
        int? id,
        int? personaId,
        Catalogo? colorOjos,
        Catalogo? formaOjos,
        Catalogo? tamanoOjos,
        string? especificacionesOjos
    )
    {
        Id = id;
        PersonaId = personaId;
        ColorOjos = colorOjos;
        FormaOjos = formaOjos;
        TamanoOjos = tamanoOjos;
        EspecificacionesOjos = especificacionesOjos;
    }

    public Ojos()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("color_ojos")]
    private Catalogo? _colorOjos;

    [ObservableProperty, JsonProperty("forma_ojos")]
    private Catalogo? _formaOjos;

    [ObservableProperty, JsonProperty("tamano_ojos")]
    private Catalogo? _tamanoOjos;

    [ObservableProperty, JsonProperty("especificaciones_ojos")]
    private string? _especificacionesOjos;
}