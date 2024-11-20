using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class SenaParticular : ObservableObject
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
        if (value is null || value == "") return;
        Imagen = await ReporteServiceNetwork.GetImage(Id);
    }

    partial void OnImagenChanged(BitmapImage? value)
    {
        if (value is null) return;
        EncodedImage = ImageUtils.BitmapImageToBase64(value);
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int? _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "cantidad")] private int? _cantidad = 1;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] private string? _descripcion;
    [ObservableProperty, JsonProperty(PropertyName = "foto")] private string? _foto;
    [ObservableProperty, JsonProperty(PropertyName = "region_cuerpo")] private CatalogoColor? _regionCuerpo;
    [ObservableProperty, JsonProperty(PropertyName = "vista")] private CatalogoColor? _vista;
    [ObservableProperty, JsonProperty(PropertyName = "lado")] private CatalogoColor? _lado;
    [ObservableProperty, JsonProperty(PropertyName = "tipo")] private Catalogo? _tipo;
    [ObservableProperty, JsonProperty(PropertyName = "encoded_image")] private string? _encodedImage;
    [ObservableProperty] private BitmapImage? _imagen;
}