using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.datos_complementarios.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class DatoComplementario : ObservableObject
{
    [JsonConstructor]
    public DatoComplementario(
        int? id,
        int? reporteId,
        Direccion direccion
    )
    {
        Id = id;
        ReporteId = reporteId;
        Direccion = direccion;
    }

    public DatoComplementario()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("direccion")]
    private Direccion _direccion = new();
}