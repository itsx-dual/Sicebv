using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Hipotesis : ObservableObject
{
    public Hipotesis(
        int? id,
        TipoHipotesis? tipoHipotesis,
        Catalogo? sitio,
        Catalogo? area,
        string? etapa
    )
    {
        Id = id;
        TipoHipotesis = tipoHipotesis;
        Sitio = sitio;
        Area = area;
        Etapa = etapa;
    }

    public Hipotesis()
    {
    }
    

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _id;

    [ObservableProperty, JsonProperty("tipo_hipotesis")]
    private TipoHipotesis? _tipoHipotesis;

    [ObservableProperty, JsonProperty("sitio")]
    private Catalogo? _sitio;

    [ObservableProperty, JsonProperty("area_asigna_sitio")]
    private Catalogo? _area;

    [ObservableProperty, JsonProperty("etapa")]
    private string? _etapa;
}

public enum EtapaHipotesis {
    Inicial,
    Final
}