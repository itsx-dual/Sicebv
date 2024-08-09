using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class HechosDesaparicion : ObservableObject
{
    [JsonConstructor]
    public HechosDesaparicion(int? id,
        int? reporteId,
        DateTime? fechaDesaparicion,
        string? fechaDesaparicionCebv,
        DateTime? fechaPercato,
        string? fechaPercatoCebv,
        string? aclaracionesFechaHechos,
        string? descripcionCambioComportamiento,
        int? contadorDesaparicion,
        string? situacionPrevia,
        string? informacionRelevante,
        string? hechosDesaparicionDescripcion,
        string? sintesisDesaparicionDescripcion,
        DateTime? fecha_desaparicion_aproximada,
        string? fecha_desaparicion_cebv,
        string? observaciones_fecha_desaparicion,
        DateTime? createdAt,
        DateTime? updatedAt,
        bool? cambioComportamiento = false)
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
        ContadorDesaparicion = contadorDesaparicion;
        SituacionPrevia = situacionPrevia;
        InformacionRelevante = informacionRelevante;
        HechosDesaparicionDescripcion = hechosDesaparicionDescripcion;
        SintesisDesaparicionDescripcion = sintesisDesaparicionDescripcion;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public HechosDesaparicion()
    {
    }

    [ObservableProperty, JsonProperty("id")] private int? _id;
    [ObservableProperty, JsonProperty("reporte_id")] private int? _reporteId;
    [ObservableProperty, JsonProperty("fecha_desaparicion")] private DateTime? _fechaDesaparicion;
    [ObservableProperty, JsonProperty("fecha_desaparicion_cebv")] private string? _fechaDesaparicionCebv;
    [ObservableProperty, JsonProperty("")] private DateTime? _fechaPercato;
    [ObservableProperty, JsonProperty("")] private string? _fechaPercatoCebv;
    [ObservableProperty, JsonProperty("")] private string? _aclaracionesFechaHechos;
    [ObservableProperty, JsonProperty("")] private bool? _cambioComportamiento;
    [ObservableProperty, JsonProperty("")] private string? _descripcionCambioComportamiento;
    [ObservableProperty, JsonProperty("")] private int? _contadorDesaparicion;
    [ObservableProperty, JsonProperty("")] private string? _situacionPrevia;
    [ObservableProperty, JsonProperty("")] private string? _informacionRelevante;
    [ObservableProperty, JsonProperty("")] private string? _hechosDesaparicionDescripcion;
    [ObservableProperty, JsonProperty("")] private string? _sintesisDesaparicionDescripcion;
    [ObservableProperty, JsonProperty("")] private DateTime? _createdAt;
    [ObservableProperty, JsonProperty("")] private DateTime? _updatedAt;
}