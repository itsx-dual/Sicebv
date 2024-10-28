using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Expediente : ObservableObject
{
    [JsonConstructor]
    public Expediente(
        int? id,
        string? tipo,
        Catalogo? parentesco,
        int? reporteUnoId,
        ReporteHechos reporte
    )
    {
        _id = id;
        _tipo = tipo;
        _parentesco = parentesco;
        _reporteUnoId = reporteUnoId;
        _reporte = reporte;
    }

    public Expediente()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("tipo")]
    private string? _tipo;

    [ObservableProperty, JsonProperty("parentesco")]
    private Catalogo? _parentesco;

    [ObservableProperty, JsonProperty("reporte_uno_id")]
    private int? _reporteUnoId;

    [ObservableProperty, JsonProperty("reporte")]
    private ReporteHechos _reporte = new();
}