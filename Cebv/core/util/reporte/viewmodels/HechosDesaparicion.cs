using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class HechosDesaparicion : ObservableObject
{
    [JsonConstructor]
    public HechosDesaparicion(
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
        int? contadorDesaparicion,
        string? situacionPrevia,
        string? informacionRelevante,
        string? hechosDesaparicionDescripcion,
        string? sintesisDesaparicionDescripcion,
        DateTime? fecha_desaparicion_aproximada,
        string? fecha_desaparicion_cebv,
        string? observaciones_fecha_desaparicion,
        DateTime? createdAt,
        DateTime? updatedAt)
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

    [ObservableProperty] private int? _id;
    [ObservableProperty] private int? _reporteId;
    [ObservableProperty] private DateTime? _fechaDesaparicion;
    [ObservableProperty] private string? _fechaDesaparicionCebv;
    [ObservableProperty] private DateTime? _fechaPercato;
    [ObservableProperty] private string? _fechaPercatoCebv;
    [ObservableProperty] private string? _aclaracionesFechaHechos;
    [ObservableProperty] private bool? _cambioComportamiento;
    [ObservableProperty] private string? _descripcionCambioComportamiento;
    [ObservableProperty] private bool? _fueAmenazado;
    [ObservableProperty] private string? _descripcionAmenaza;
    [ObservableProperty] private int? _contadorDesaparicion;
    [ObservableProperty] private string? _situacionPrevia;
    [ObservableProperty] private string? _informacionRelevante;
    [ObservableProperty] private string? _hechosDesaparicionDescripcion;
    [ObservableProperty] private string? _sintesisDesaparicionDescripcion;
    [ObservableProperty] private DateTime? _createdAt;
    [ObservableProperty] private DateTime? _updatedAt;
}