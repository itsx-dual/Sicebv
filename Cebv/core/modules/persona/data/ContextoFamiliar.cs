using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class ContextoFamiliar : ObservableObject
{
    [JsonConstructor]
    public ContextoFamiliar(
        int? id,
        int? personaId,
        Catalogo? estadoConyugal,
        string? nombreParejaConyugue,
        int? numeroPersonasVive
    )
    {
        Id = id;
        PersonaId = personaId;
        EstadoConyugal = estadoConyugal;
        NombreParejaConyugue = nombreParejaConyugue;
        NumeroPersonasVive = numeroPersonasVive;
    }

    public ContextoFamiliar()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("estado_conyugal")]
    private Catalogo? _estadoConyugal;

    [ObservableProperty, JsonProperty("numero_personas_vive")]
    private int? _numeroPersonasVive;
    
    [ObservableProperty, JsonProperty("nombre_pareja_conyugue")]
    private string? _nombreParejaConyugue;
}