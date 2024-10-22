using System.Collections.ObjectModel;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public enum TipoExpediente
{
    Directo,
    Indirecto
}

[JsonObject(MemberSerialization.OptIn)]
public partial class Expediente : ObservableObject
{
    [JsonConstructor]
    public Expediente(
        int? id,
        string? tipo,
        Catalogo? parentesco,
        ReporteHechos? reporteUno,
        ReporteHechos? reporteDos
    )
    {
        _id = id;
        _tipo = tipo;
        _parentesco = parentesco;
        _reporteUno = reporteUno;
        _reporteDos = reporteDos;
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

    [ObservableProperty, JsonProperty("reporte_uno")]
    private ReporteHechos? _reporteUno;

    [ObservableProperty, JsonProperty("reporte_dos")]
    private ReporteHechos? _reporteDos;
}