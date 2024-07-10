using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class HechosDesaparicionResponse : ObservableObject
{
    public HechosDesaparicionResponse(
        int? id,
        int? reporteId,
        DateTime? fechaDesaparicion,
        string? fechaDesaparicionCebv,
        DateTime? fechaPercato,
        string? fechaPercatoCebv,
        string? aclaracionesFechaHechos,
        bool? cambioComportamiento,
        string? descripcionCambioComportamiento,
        bool? fueAmenazado,
        string? descripcionAmenaza,
        int? contadorDesapariciones,
        string? situacionPrevia,
        string? informacionRelevante,
        string? hechosDesaparicion,
        string? sintesisDesaparicion
    )
    {
        Id = id;
        ReporteId = reporteId;
        FechaDesaparicion = fechaDesaparicion;
        FechaDesaparicionCebv = fechaDesaparicionCebv;
        FechaPercato = fechaPercato;
        FechaPercatoCebv = fechaPercatoCebv;
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

    [ObservableProperty, JsonProperty("fecha_percato")]
    private DateTime? _fechaPercato;

    [ObservableProperty, JsonProperty("fecha_percato_cebv")]
    private string? _fechaPercatoCebv;

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
}