using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class HechosDesaparicionResponse : ObservableObject
{
    [JsonConstructor]
    public HechosDesaparicionResponse(
        int? id,
        int? reporte_id,
        DateTime? fecha_desaparicion,
        string? fecha_desaparicion_cebv,
        string? hora_desaparicion,
        DateTime? fecha_percato,
        string? fecha_percato_cebv,
        string? hora_percato,
        string? aclaraciones_fecha_hechos,
        bool? cambio_comportamiento,
        string? descripcion_cambio_comportamiento,
        bool? fue_amenazado,
        string? descripcion_amenaza,
        int? contador_desapariciones,
        string? situacion_previa,
        string? informacion_relevante,
        string? hechos_desaparicion,
        string? sintesis_desaparicion,
        bool? desaparecio_acompanado,
        int? personas_mismo_evento
    )
    {
        Id = id;
        ReporteId = reporte_id;
        FechaDesaparicion = fecha_desaparicion;
        FechaDesaparicionCebv = fecha_desaparicion_cebv;
        HoraDesaparicion = hora_desaparicion;
        FechaPercato = fecha_percato;
        FechaPercatoCebv = fecha_percato_cebv;
        HoraPercato = hora_percato;
        AclaracionesFechaHechos = aclaraciones_fecha_hechos;
        CambioComportamiento = cambio_comportamiento;
        DescripcionCambioComportamiento = descripcion_cambio_comportamiento;
        FueAmenazado = fue_amenazado;
        DescripcionAmenaza = descripcion_amenaza;
        ContadorDesapariciones = contador_desapariciones;
        SituacionPrevia = situacion_previa;
        InformacionRelevante = informacion_relevante;
        HechosDesaparicion = hechos_desaparicion;
        SintesisDesaparicion = sintesis_desaparicion;
        DesaparecioAcompanado = desaparecio_acompanado;
        PersonasMismoEvento = personas_mismo_evento;;
    }

    public HechosDesaparicionResponse() { }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("fecha_desaparicion")]
    private DateTime? _fechaDesaparicion;

    [ObservableProperty, JsonProperty("fecha_desaparicion_cebv")]
    private string? _fechaDesaparicionCebv;

    [ObservableProperty, JsonProperty("hora_desaparicion")]
    private string? _horaDesaparicion;

    [ObservableProperty, JsonProperty("fecha_percato")]
    private DateTime? _fechaPercato;

    [ObservableProperty, JsonProperty("fecha_percato_cebv")]
    private string? _fechaPercatoCebv;

    [ObservableProperty, JsonProperty("hora_percato")]
    private string? _horaPercato;

    [ObservableProperty, JsonProperty("aclaraciones_fecha_hechos")]
    private string? _aclaracionesFechaHechos;

    [ObservableProperty, JsonProperty("cambio_comportamiento")]
    private bool? _cambioComportamiento;

    [ObservableProperty, JsonProperty("descripcion_cambio_comportamiento")]
    private string? _descripcionCambioComportamiento;

    [ObservableProperty, JsonProperty("fue_amenazado")]
    private bool? _fueAmenazado;

    [ObservableProperty, JsonProperty("descripcion_amenaza")]
    private string? _descripcionAmenaza;

    [ObservableProperty, JsonProperty("contador_desapariciones")]
    private int? _contadorDesapariciones;

    [ObservableProperty, JsonProperty("situacion_previa")]
    private string? _situacionPrevia;

    [ObservableProperty, JsonProperty("informacion_relevante")]
    private string? _informacionRelevante;

    [ObservableProperty, JsonProperty("hechos_desaparicion")]
    private string? _hechosDesaparicion;

    [ObservableProperty, JsonProperty("sintesis_desaparicion")]
    private string? _sintesisDesaparicion;

    [ObservableProperty, JsonProperty("lugar_hechos")]
    private Direccion? _lugarHechos = new();
    
    [ObservableProperty, JsonProperty("desaparecio_acompanado")]
    private bool? _desaparecioAcompanado;

    [ObservableProperty, JsonProperty("personas_mismo_evento")]
    private int? _personasMismoEvento;
    
    [ObservableProperty, JsonProperty(PropertyName = "fecha_desaparicion_aproximada")]
    private DateTime? _fechaDesaparicionAproximada;
    
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_fecha_desaparicion")]
    private string? _observacionesFechaDesaparicion;
}