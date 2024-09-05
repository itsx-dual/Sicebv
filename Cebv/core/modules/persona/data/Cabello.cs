using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Cabello : ObservableObject
{
    public Cabello(
        int? id,
        int? personaId,
        Catalogo? calvicie,
        Catalogo? colorCabello,
        Catalogo? tamanoCabello,
        Catalogo? tipoCabello,
        string? especificacionesCabello
    )
    {
        Id = id;
        PersonaId = personaId;
        Calvicie = calvicie;
        ColorCabello = colorCabello;
        TamanoCabello = tamanoCabello;
        TipoCabello = tipoCabello;
        EspecificacionesCabello = especificacionesCabello;
    }

    public Cabello()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("calvicie")]
    private Catalogo? _calvicie;

    [ObservableProperty, JsonProperty("color_cabello")]
    private Catalogo? _colorCabello;

    [ObservableProperty, JsonProperty("tamano_cabello")]
    private Catalogo? _tamanoCabello;

    [ObservableProperty, JsonProperty("tipo_cabello")]
    private Catalogo? _tipoCabello;

    [ObservableProperty, JsonProperty("especificaciones_cabello")]
    private string? _especificacionesCabello;
}