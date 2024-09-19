using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class EnfoquePersonal : ObservableObject
{
    [JsonConstructor]
    public EnfoquePersonal(
        int? id,
        int? personaId,
        Catalogo? tipoEnfoqueDiferenciado
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoEnfoqueDiferenciado = tipoEnfoqueDiferenciado;
    }

    public EnfoquePersonal()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_enfoque_diferenciado")]
    private Catalogo? _tipoEnfoqueDiferenciado;
}