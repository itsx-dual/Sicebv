using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class ContextoSocial : ObservableObject
{
    [JsonConstructor]
    public ContextoSocial(
        int? id,
        int? personaId,
        Catalogo? situacionMigratoria,
        bool? estaTransitoEstadosUnidos,
        string? descripcionProcesoMigratorio
    )
    {
        Id = id;
        PersonaId = personaId;
        SituacionMigratoria = situacionMigratoria;
        EstaTransitoEstadosUnidos = estaTransitoEstadosUnidos;
        DescripcionProcesoMigratorio = descripcionProcesoMigratorio;
    }

    public ContextoSocial()
    {
    }


    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("situacion_migratoria")]
    private Catalogo? _situacionMigratoria;

    [ObservableProperty, JsonProperty("esta_transito_estados_unidos")]
    private bool? _estaTransitoEstadosUnidos;

    [ObservableProperty, JsonProperty("descripcion_proceso_migratorio")]
    private string? _descripcionProcesoMigratorio;
}