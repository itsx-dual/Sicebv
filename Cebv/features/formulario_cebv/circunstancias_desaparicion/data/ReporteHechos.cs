using System.Collections.ObjectModel;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class ReporteHechos : ObservableObject
{
    [JsonConstructor]
    public ReporteHechos(
        int? id,
        ObservableCollection<Folio> folios,
        HechosDesaparicion hechosDesaparicion
    )
    {
        Id = id;
        Folios = folios;
        HechosDesaparicion = hechosDesaparicion;
    }

    public ReporteHechos()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("folios")]
    private ObservableCollection<Folio> _folios = [];

    [ObservableProperty, JsonProperty("hechos_desaparicion")]
    private HechosDesaparicion? _hechosDesaparicion;
}