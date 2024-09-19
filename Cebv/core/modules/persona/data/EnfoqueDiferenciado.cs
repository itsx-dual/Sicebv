using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class EnfoqueDiferenciado : ObservableObject
{
    [JsonConstructor]
    public EnfoqueDiferenciado(
        int? id,
        int? personaId,
        bool? pertenenciaGrupalEtnica,
        string? descripcionVulnerabilidad,
        string? informacionRelevanteBusqueda
    )
    {
        Id = id;
        PersonaId = personaId;
        PertenenciaGrupalEtnica = pertenenciaGrupalEtnica;
        DescripcionVulnerabilidad = descripcionVulnerabilidad;
        InformacionRelevanteBusqueda = informacionRelevanteBusqueda;
    }

    public EnfoqueDiferenciado()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("pertenencia_grupal_etnica")]
    private bool? _pertenenciaGrupalEtnica;

    [ObservableProperty, JsonProperty("descripcion_vulnerabilidad")]
    private string? _descripcionVulnerabilidad;

    [ObservableProperty, JsonProperty("informacion_relevante_busqueda")]
    private string? _informacionRelevanteBusqueda;
}