using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class MediaFiliacion : ObservableObject
{
    [JsonConstructor]
    public MediaFiliacion(
        int id,
        int personaId,
        int? estatura,
        int? peso,
        Catalogo? complexion,
        Catalogo? colorPiel,
        Catalogo? formaCara,
        Catalogo? colorOjos,
        Catalogo? formaOjos,
        Catalogo? tamanoOjos,
        Catalogo? calvicie,
        Catalogo? colorCabello,
        Catalogo? tamanoCabello,
        Catalogo? tipoCabello,
        Catalogo? tipoCeja,
        Catalogo? tipoNariz,
        Catalogo? tamanoBoca,
        Catalogo? tamanoLabios,
        Catalogo? tamanoOrejas,
        Catalogo? formaOrejas,
        string? observaciones_ojos,
        string? observaciones_cabello,
        string? bigote,
        string? barba,
        string? observaciones_cejas,
        string? observaciones_barba,
        string? observaciones_bigote,
        string? observaciones_nariz,
        string? observaciones_boca,
        string? observaciones_oreja
    )
    {
        Id = id;
        PersonaId = personaId;
        Estatura = estatura;
        Peso = peso;
        Complexion = complexion; 
        ColorPiel = colorPiel; 
        FormaCara = formaCara; 
        ColorOjos = colorOjos; 
        FormaOjos = formaOjos; 
        TamanoOjos = tamanoOjos; 
        Calvicie = calvicie; 
        ColorCabello = colorCabello; 
        TamanoCabello = tamanoCabello; 
        TipoCabello = tipoCabello; 
        TipoCeja = tipoCeja; 
        TipoNariz = tipoNariz; 
        TamanoBoca = tamanoBoca; 
        TamanoLabios = tamanoLabios; 
        TamanoOrejas = tamanoOrejas; 
        FormaOrejas = formaOrejas; 
        ObservacionesOjos = observaciones_ojos; 
        ObservacionesCabello = observaciones_cabello; 
        Bigote = bigote; 
        Barba = barba; 
        ObservacionesCejas = observaciones_cejas; 
        ObservacionesBarba = observaciones_barba; 
        ObservacionesBigote = observaciones_bigote; 
        ObservacionesNariz = observaciones_nariz; 
        ObservacionesBoca = observaciones_boca; 
        ObservacionesOreja = observaciones_oreja; 
    }

    public MediaFiliacion() { }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "estatura")] private int? _estatura;
    [ObservableProperty, JsonProperty(PropertyName = "peso")] private int? _peso;
    
    [ObservableProperty, JsonProperty(PropertyName = "complexion")] private Catalogo? _complexion; 
    [ObservableProperty, JsonProperty(PropertyName = "color_piel")] private Catalogo? _colorPiel;
    [ObservableProperty, JsonProperty(PropertyName = "forma_cara")] private Catalogo? _formaCara;
    [ObservableProperty, JsonProperty(PropertyName = "color_ojos")] private Catalogo? _colorOjos;
    [ObservableProperty, JsonProperty(PropertyName = "forma_ojos")] private Catalogo? _formaOjos;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_ojos")] private Catalogo? _tamanoOjos;
    [ObservableProperty, JsonProperty(PropertyName = "calvicie")] private Catalogo? _calvicie;
    [ObservableProperty, JsonProperty(PropertyName = "color_cabello")] private Catalogo? _colorCabello;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_cabello")] private Catalogo? _tamanoCabello;
    [ObservableProperty, JsonProperty(PropertyName = "tipo_cabello")] private Catalogo? _tipoCabello;
    [ObservableProperty, JsonProperty(PropertyName = "tipo_ceja")] private Catalogo? _tipoCeja;
    [ObservableProperty, JsonProperty(PropertyName = "tipo_nariz")] private Catalogo? _tipoNariz;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_boca")] private Catalogo? _tamanoBoca;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_labios")] private Catalogo? _tamanoLabios;
    [ObservableProperty, JsonProperty(PropertyName = "tamano_orejas")] private Catalogo? _tamanoOrejas;
    [ObservableProperty, JsonProperty(PropertyName = "forma_orejas")] private Catalogo? _formaOrejas;
    
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_ojos")] private string? _observacionesOjos;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_cabello")] private string? _observacionesCabello;
    [ObservableProperty, JsonProperty(PropertyName = "bigote")] private string? _bigote;
    [ObservableProperty, JsonProperty(PropertyName = "barba")] private string? _barba;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_cejas")] private string? _observacionesCejas;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_barba")] private string? _observacionesBarba;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_bigote")] private string? _observacionesBigote;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_nariz")] private string? _observacionesNariz;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_boca")] private string? _observacionesBoca;
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_oreja")] private string? _observacionesOreja;
}