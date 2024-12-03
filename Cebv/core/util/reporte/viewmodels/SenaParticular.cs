using System.ComponentModel.DataAnnotations;
using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class SenaParticular : ObservableValidator
{
    [JsonConstructor]
    public SenaParticular(
        int id,
        int? personaId,
        int? cantidad,
        string? descripcion,
        string? foto,
        CatalogoColor? regionCuerpo,
        CatalogoColor? vista,
        CatalogoColor? lado,
        Catalogo? tipo
    )
    {
        Id = id;
        PersonaId = personaId;
        Cantidad = cantidad;
        Descripcion = descripcion;
        Foto = foto; 
        RegionCuerpo = regionCuerpo;
        Vista = vista;
        Lado = lado;
        Tipo = tipo;
    }

    public SenaParticular(SenaParticular senaParticular)
    {
        Id = senaParticular.Id;
        PersonaId = senaParticular.PersonaId;
        Cantidad = senaParticular.Cantidad;
        Descripcion = senaParticular.Descripcion;
        Foto = senaParticular.Foto; 
        RegionCuerpo = senaParticular.RegionCuerpo;
        Vista = senaParticular.Vista;
        Lado = senaParticular.Lado;
        Tipo = senaParticular.Tipo;
        EncodedImage = senaParticular.EncodedImage;
        Imagen = senaParticular.Imagen;
    }
    
    public SenaParticular() { }
    
    async partial void OnFotoChanged(string? value)
    {
        if (value is null or "") return;
        Imagen = await ReporteServiceNetwork.GetImage(Id);
    }

    partial void OnImagenChanged(BitmapImage? value)
    {
        if (value is null) return;
        EncodedImage = ImageUtils.BitmapImageToBase64(value);
    }

    public void Validar() => ValidateAllProperties();

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")]
    private int? _personaId;
    
    [ObservableProperty, JsonProperty(PropertyName = "cantidad")]
    [Required(ErrorMessage = "Se debe definir una cantidad de señas particulares")]
    private int? _cantidad = 1;
    
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")]
    private string? _descripcion;
    
    [ObservableProperty, JsonProperty(PropertyName = "foto")]
    private string? _foto;
    
    [ObservableProperty, JsonProperty(PropertyName = "region_cuerpo")]
    [Required(ErrorMessage = "Se debe seleccionar la region del cuerpo de la seña particular")]
    private CatalogoColor? _regionCuerpo;
    
    [ObservableProperty, JsonProperty(PropertyName = "vista")]
    [Required(ErrorMessage = "Se debe seleccionar la vista de la seña particular")]
    private CatalogoColor? _vista;
    
    [ObservableProperty, JsonProperty(PropertyName = "lado")]
    [Required(ErrorMessage = "Se debe seleccionar el lado de la seña particular")]
    private CatalogoColor? _lado;
    
    [ObservableProperty, JsonProperty(PropertyName = "tipo")]
    private Catalogo? _tipo;
    
    [ObservableProperty, JsonProperty(PropertyName = "encoded_image")]
    private string? _encodedImage;
    
    [ObservableProperty]
    private BitmapImage? _imagen;
}