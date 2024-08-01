using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class ControlOgpi : ObservableObject
{
    [JsonConstructor]
    public ControlOgpi(
        int? id,
        int? reporteId,
        DateTime? fechaCodificacion,
        string? nombreCodificador,
        string? observaciones,
        string? numeroTarjeta
    )
    {
        Id = id;
        ReporteId = reporteId;
        FechaCodificacion = fechaCodificacion;
        NombreCodificador = nombreCodificador;
        Observaciones = observaciones;
        NumeroTarjeta = numeroTarjeta;
    }

    public ControlOgpi()
    {
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty(PropertyName = "fecha_codificacion")]
    private DateTime? _fechaCodificacion;

    [ObservableProperty, JsonProperty(PropertyName = "nombre_codificador")]
    private string? _nombreCodificador;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones")]
    private string? _observaciones;

    [ObservableProperty, JsonProperty(PropertyName = "numero_tarjeta")]
    private string? _numeroTarjeta;
}