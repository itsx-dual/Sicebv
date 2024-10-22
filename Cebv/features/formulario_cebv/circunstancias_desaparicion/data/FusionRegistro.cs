using Cebv.core.modules.persona.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class FusionRegistro : ObservableObject
{
    [JsonConstructor]
    public FusionRegistro(
        int? id,
        PersonaCompact personaUno,
        PersonaCompact personaDos
    )
    {
        Id = id;
        PersonaUno = personaUno;
        PersonaDos = personaDos;
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_uno")]
    private PersonaCompact? _personaUno;

    [ObservableProperty, JsonProperty("persona_dos")]
    private PersonaCompact? _personaDos;
}