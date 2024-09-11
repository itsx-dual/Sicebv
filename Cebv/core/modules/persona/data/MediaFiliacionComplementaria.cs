using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class MediaFiliacionComplementaria : ObservableObject
{
    [JsonConstructor]
    public MediaFiliacionComplementaria(
        int? id,
        int? personaId,
        bool? tieneAusenciaDental,
        string? descripcionAusenciaDental,
        bool? tieneTratamientoDental,
        string? descripcionTratamientoDental,
        Catalogo? tipoMenton,
        string? especificacionesMenton,
        string? observacionesGenerales
    )
    {
        Id = id;
        PersonaId = personaId;
        TieneAusenciaDental = tieneAusenciaDental;
        DescripcionAusenciaDental = descripcionAusenciaDental;
        TieneTratamientoDental = tieneTratamientoDental;
        DescripcionTratamientoDental = descripcionTratamientoDental;
        TipoMenton = tipoMenton;
        EspecificacionesMenton = especificacionesMenton;
        ObservacionesGenerales = observacionesGenerales;
    }

    public MediaFiliacionComplementaria()
    {
    }


    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tiene_ausencia_dental")]
    private bool? _tieneAusenciaDental = false;

    [ObservableProperty, JsonProperty("descripcion_ausencia_dental")]
    private string? _descripcionAusenciaDental;

    [ObservableProperty, JsonProperty("tiene_tratamiento_dental")]
    private bool? _tieneTratamientoDental = false;

    [ObservableProperty, JsonProperty("descripcion_tratamiento_dental")]
    private string? _descripcionTratamientoDental;

    [ObservableProperty, JsonProperty("tipo_menton")]
    private Catalogo? _tipoMenton;

    [ObservableProperty, JsonProperty("especificaciones_menton")]
    private string? _especificacionesMenton;

    [ObservableProperty, JsonProperty("observaciones_generales")]
    private string? _observacionesGenerales;
}