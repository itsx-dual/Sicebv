using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class HechosDesaparicionResponse : ObservableObject
{
    [JsonConstructor]
    public HechosDesaparicionResponse(
        int? id,
        int? reporteId,
        DateTime? fechaDesaparicion,
        string? fechaDesaparicionCebv,
        string? horaDesaparicion,
        DateTime? fechaPercato,
        string? fechaPercatoCebv,
        string? horaPercato,
        string? aclaracionesFechaHechos,
        bool? cambioComportamiento,
        string? descripcionCambioComportamiento,
        bool? fueAmenazado,
        string? descripcionAmenaza,
        int? contadorDesapariciones,
        string? situacionPrevia,
        string? informacionRelevante,
        string? hechosDesaparicion,
        string? sintesisDesaparicion,
        bool? desaparecioAcompanado,
        int? personasMismoEvento
    )
    {
        Id = id;
        ReporteId = reporteId;
        FechaDesaparicion = fechaDesaparicion;
        FechaDesaparicionCebv = fechaDesaparicionCebv;
        HoraDesaparicion = horaDesaparicion;
        FechaPercato = fechaPercato;
        FechaPercatoCebv = fechaPercatoCebv;
        HoraPercato = horaPercato;
        AclaracionesFechaHechos = aclaracionesFechaHechos;
        CambioComportamiento = cambioComportamiento;
        DescripcionCambioComportamiento = descripcionCambioComportamiento;
        FueAmenazado = fueAmenazado;
        DescripcionAmenaza = descripcionAmenaza;
        ContadorDesapariciones = contadorDesapariciones;
        SituacionPrevia = situacionPrevia;
        InformacionRelevante = informacionRelevante;
        HechosDesaparicion = hechosDesaparicion;
        SintesisDesaparicion = sintesisDesaparicion;
        DesaparecioAcompanado = desaparecioAcompanado;
        PersonasMismoEvento = personasMismoEvento;
    }

    public HechosDesaparicionResponse()
    {
    }

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

    [ObservableProperty, JsonProperty("desaparecio_acompanado")]
    private bool? _desaparecioAcompanado;

    [ObservableProperty, JsonProperty("personas_mismo_evento")]
    private int? _personasMismoEvento;
}