using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.generacion_documentos.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class GeneracionDocumento : ObservableObject
{
    [JsonConstructor]
    public GeneracionDocumento(
        int? id,
        int? reporteId,
        Catalogo? medioDifusion,
        string? resultadoRnd,
        bool firmaAusencia
    )
    {
        _id = id;
        _reporteId = reporteId;
        _medioDifusion = medioDifusion;
        _resultadoRnd = resultadoRnd;
        _firmaAusencia = firmaAusencia;
    }

    public GeneracionDocumento()
    {
        
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("medio_difusion")]
    private Catalogo? _medioDifusion;

    [ObservableProperty, JsonProperty("resultado_rnd")]
    private string? _resultadoRnd;

    [ObservableProperty, JsonProperty("firma_ausencia")]
    private bool _firmaAusencia;
}