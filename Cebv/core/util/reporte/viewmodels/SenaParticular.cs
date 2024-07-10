using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class SenaParticular : ObservableObject
{
    [JsonConstructor]
    public SenaParticular(
        int id,
        int? persona_id,
        int? cantidad,
        string? descripcion,
        string? foto,
        CatalogoColor? region_cuerpo,
        Catalogo? vista,
        CatalogoColor? lado,
        Catalogo? tipo
    )
    {
        Id = id;
        PersonaId = persona_id;
        Cantidad = cantidad;
        Descripcion = descripcion;
        Foto = foto; 
        RegionCuerpo = region_cuerpo;
        Vista = vista;
        Lado = lado;
        Tipo = tipo;
    }

    public SenaParticular() { }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((SenaParticular) obj);
    }

    private bool Equals(SenaParticular sena)
    {
        return Id == sena.Id;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "persona_id")] private int? _personaId;
    [ObservableProperty, JsonProperty(PropertyName = "cantidad")] private int? _cantidad;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] private string? _descripcion;
    [ObservableProperty, JsonProperty(PropertyName = "foto")] private string? _foto;
    [ObservableProperty, JsonProperty(PropertyName = "region_cuerpo")] private CatalogoColor? _regionCuerpo;
    [ObservableProperty, JsonProperty(PropertyName = "vista")] private Catalogo? _vista;
    [ObservableProperty, JsonProperty(PropertyName = "lado")] private CatalogoColor? _lado;
    [ObservableProperty, JsonProperty(PropertyName = "tipo")] private Catalogo? _tipo;
}