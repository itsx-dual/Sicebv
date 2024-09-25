using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.contexto.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class PasatiempoPersona : ObservableObject
{
    [JsonConstructor]
    public PasatiempoPersona(
        int? id,
        int? personaId,
        Catalogo pasatiempo
    )
    {
        Id = id;
        PersonaId = personaId;
        Pasatiempo = pasatiempo;
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("pasatiempo")]
    private Catalogo? _pasatiempo;
}