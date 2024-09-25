using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.contexto.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class ClubPersona : ObservableObject
{
    [JsonConstructor]
    public ClubPersona(
        int? id,
        int? personaId,
        Catalogo club
    )
    {
        Id = id;
        PersonaId = personaId;
        Club = club;
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("club")]
    private Catalogo? _club;
}