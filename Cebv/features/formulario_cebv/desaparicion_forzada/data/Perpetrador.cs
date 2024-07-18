using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Perpetrador : ObservableObject
{
    [JsonConstructor]
    public Perpetrador(
        int? id,
        string? nombre,
        string? descripcion,
        Catalogo? sexo,
        Catalogo? estatusPerpetrador,
        int? reporteId
    )
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Sexo = sexo;
        EstatusPerpetrador = estatusPerpetrador;
        ReporteId = reporteId;
    }

    public Perpetrador()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty("descripcion")]
    private string? _descripcion;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;
    
    [ObservableProperty, JsonProperty("sexo")]
    private Catalogo? _sexo;

    [ObservableProperty, JsonProperty("estatus_perpetrador")]
    private Catalogo? _estatusPerpetrador;
}