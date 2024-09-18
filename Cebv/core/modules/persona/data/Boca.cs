using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Boca : ObservableObject
{
    [JsonConstructor]
    public Boca(
        int? id,
        int? personaId,
        Catalogo? tamanoBoca,
        Catalogo? tamanoLabios,
        string? especificacionesBoca
    )
    {
        Id = id;
        PersonaId = personaId;
        TamanoBoca = tamanoBoca;
        TamanoLabios = tamanoLabios;
        EspecificacionesBoca = especificacionesBoca;
    }

    public Boca()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tamano_boca")]
    private Catalogo? _tamanoBoca;

    [ObservableProperty, JsonProperty("tamano_labios")]
    private Catalogo? _tamanoLabios;

    [ObservableProperty, JsonProperty("especificaciones_boca")]
    private string? _especificacionesBoca;
}