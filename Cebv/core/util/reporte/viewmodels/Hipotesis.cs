using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using static Cebv.core.util.enums.EtapaHipotesis;

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
    
    // --------------------------------------------------------------
    // Parametros por defecto de los diferentes tipos de hipotesis
    // --------------------------------------------------------------
    
    public readonly Dictionary<string, object> ParametrosInicialPrimaria = new()
    {
        { nameof(Etapa), InicialPrimaria }
    };
    
    public readonly Dictionary<string, object> ParametrosInicialSecundaria = new()
    {
        { nameof(Etapa), InicialSecundaria }
    };
}