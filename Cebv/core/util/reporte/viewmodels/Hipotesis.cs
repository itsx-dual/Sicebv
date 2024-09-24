using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Hipotesis : ObservableObject
{
    [JsonConstructor]
    public Hipotesis(
        int? id,
        int? reporteId,
        TipoHipotesis? tipoHipotesis,
        string? etapa
    )
    {
        Id = id;
        ReporteId = reporteId;
        TipoHipotesis = tipoHipotesis;
        Etapa = etapa;
    }

    public Hipotesis()
    {
    }
    

    [ObservableProperty, JsonProperty("id")]
    private int? _id;
    
    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("tipo_hipotesis")]
    private TipoHipotesis? _tipoHipotesis;

    [ObservableProperty, JsonProperty("etapa")]
    private string? _etapa;
}