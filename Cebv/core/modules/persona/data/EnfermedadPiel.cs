using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class EnfermedadPiel : ObservableObject
{
    [JsonConstructor]
    public EnfermedadPiel(
        int? id,
        int? personaId,
        Catalogo? tipoEnfermedadPiel,
        string? descripcion
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoEnfermedadPiel = tipoEnfermedadPiel;
        Descripcion = descripcion;
    }

    public EnfermedadPiel()
    {
    }


    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_enfermedad_piel")]
    private Catalogo? _tipoEnfermedadPiel;

    [ObservableProperty, JsonProperty("descripcion")]
    private string? _descripcion;
}