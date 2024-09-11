using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class IntervencionQuirurgica : ObservableObject
{
    [JsonConstructor]
    public IntervencionQuirurgica(
        int? id,
        int? personaId,
        Catalogo? tipoIntervencionQuirurgica,
        string? descripcion
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoIntervencionQuirurgica = tipoIntervencionQuirurgica;
        Descripcion = descripcion;
    }

    public IntervencionQuirurgica()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_intervencion_quirurgica")]
    private Catalogo? _tipoIntervencionQuirurgica;

    [ObservableProperty, JsonProperty("descripcion")]
    private string? _descripcion;
}