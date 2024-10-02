using Cebv.core.modules.sistema.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.folio_expediente.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class ExpedienteFisico : ObservableObject
{
    [JsonConstructor]
    public ExpedienteFisico(
        int? id,
        int? reporteId,
        Catalogo? areaReceptora,
        UserAdmin? solicitanteExpediente,
        DateTime? fechaCambioArea,
        DateTime? fechaPrestamo,
        DateTime? fechaDevolucion,
        string? observaciones
    )
    {
        Id = id;
        ReporteId = reporteId;
        AreaReceptora = areaReceptora;
        SolicitanteExpediente = solicitanteExpediente;
        FechaCambioArea = fechaCambioArea;
        FechaPrestamo = fechaPrestamo;
        FechaDevolucion = fechaDevolucion;
        Observaciones = observaciones;
    }

    public ExpedienteFisico()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("area_receptora")]
    private Catalogo? _areaReceptora;

    [ObservableProperty, JsonProperty("solicitante_expediente")]
    private UserAdmin? _solicitanteExpediente;

    [ObservableProperty, JsonProperty("fecha_cambio_area")]
    private DateTime? _fechaCambioArea;

    [ObservableProperty, JsonProperty("fecha_prestamo")]
    private DateTime? _fechaPrestamo;

    [ObservableProperty, JsonProperty("fecha_devolucion")]
    private DateTime? _fechaDevolucion;

    [ObservableProperty, JsonProperty("observaciones")]
    private string? _observaciones;
}