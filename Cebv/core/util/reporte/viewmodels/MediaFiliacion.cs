using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class MediaFiliacion : ObservableObject
{
    [JsonConstructor]
    public MediaFiliacion(
        int id,
        int persona_id,
        int? estatura,
        int? peso,
        Catalogo? complexion,
        Catalogo? color_piel,
        Catalogo? color_ojos,
        Catalogo? color_cabello,
        Catalogo? tamano_cabello,
        Catalogo? tipo_cabello
    )
    {
        Id = id;
        PersonaId = persona_id;
        Estatura = estatura;
        Peso = peso;
        Complexion = complexion;
        ColorPiel = color_piel;
        ColorOjos = color_ojos;
        ColorCabello = color_cabello;
        TamanoCabello = tamano_cabello;
        TipoCabello = tipo_cabello;
    }

    public MediaFiliacion() { }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "estatura")] private int? _estatura;
    [ObservableProperty, JsonProperty(PropertyName = "peso")] private int? _peso;
    [ObservableProperty, JsonProperty(PropertyName = "complexion")] private Catalogo? _complexion;
    [ObservableProperty, JsonProperty(PropertyName = "color_piel")] private Catalogo? _colorPiel;
    [ObservableProperty, JsonProperty(PropertyName = "color_ojos")] private Catalogo? _colorOjos;
    [ObservableProperty, JsonProperty(PropertyName = "color_cabello")] private Catalogo? _colorCabello;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_cabello")] private Catalogo? _tamanoCabello;
    [ObservableProperty, JsonProperty(PropertyName = "tipo_cabello")] private Catalogo? _tipoCabello;
}