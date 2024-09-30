using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.contexto.data;

[JsonObject]
public partial class ContextoEconomico : ObservableObject
{
    [JsonConstructor]
    public ContextoEconomico(
        int? id,
        int? personaId,
        string? dondeTrabaja,
        int? antiguedadTrabajo,
        bool? gustaTrabajo,
        bool? deseaTrabajoForaneo,
        string? ubicacionTrabajoForaneo,
        bool? violenciaLaboral,
        string? violentadorLaboral,
        bool? tieneDeudas,
        float? montoDeuda,
        string? deudaCon,
        string? otrasEspecificacionesOcupacion
    )
    {
        Id = id;
        PersonaId = personaId;
        DondeTrabaja = dondeTrabaja;
        AntiguedadTrabajo = antiguedadTrabajo;
        GustaTrabajo = gustaTrabajo;
        DeseaTrabajoForaneo = deseaTrabajoForaneo;
        UbicacionTrabajoForaneo = ubicacionTrabajoForaneo;
        ViolenciaLaboral = violenciaLaboral;
        ViolentadorLaboral = violentadorLaboral;
        TieneDeudas = tieneDeudas;
        MontoDeuda = montoDeuda;
        DeudaCon = deudaCon;
        OtrasEspecificacionesOcupacion = otrasEspecificacionesOcupacion;
    }

    public ContextoEconomico()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("donde_trabaja")]
    private string? _dondeTrabaja;

    [ObservableProperty, JsonProperty("antiguedad_trabajo")]
    private int? _antiguedadTrabajo;

    [ObservableProperty, JsonProperty("gusta_trabajo")]
    private bool? _gustaTrabajo = false;

    [ObservableProperty, JsonProperty("desea_trabajo_foraneo")]
    private bool? _deseaTrabajoForaneo = false;

    [ObservableProperty, JsonProperty("ubicacion_trabajo_foraneo")]
    private string? _ubicacionTrabajoForaneo;

    [ObservableProperty, JsonProperty("violencia_laboral")]
    private bool? _violenciaLaboral = false;

    [ObservableProperty, JsonProperty("violentador_laboral")]
    private string? _violentadorLaboral;

    [ObservableProperty, JsonProperty("tiene_deudas")]
    private bool? _tieneDeudas = false;

    [ObservableProperty, JsonProperty("monto_deuda")]
    private float? _montoDeuda;

    [ObservableProperty, JsonProperty("deuda_con")]
    private string? _deudaCon;

    [ObservableProperty, JsonProperty("otras_especificaciones_ocupacion")]
    private string? _otrasEspecificacionesOcupacion;
}