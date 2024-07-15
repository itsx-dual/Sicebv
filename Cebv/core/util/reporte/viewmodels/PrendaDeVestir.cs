using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class PrendaDeVestir : ObservableObject 
{
    [JsonConstructor]
    public PrendaDeVestir(int id, string? marca, string? descripcion, Pertenencia? pertenencia, Catalogo? color)
    {
        Id = id;
        Marca = marca;
        Descripcion = descripcion;
        Pertenencia = pertenencia;
        Color = color;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((PrendaDeVestir) obj);
    }

    private bool Equals(PrendaDeVestir prenda)
    {
        return Id == prenda.Id &&
               Descripcion == prenda.Descripcion &&
               Marca == prenda.Marca &&
               (bool) Pertenencia?.Equals(prenda.Pertenencia) &&
               (bool) Color?.Equals(prenda.Color);
    }

    public PrendaDeVestir() { }

    [ObservableProperty, JsonProperty(PropertyName = "id")] private int _id;
    [ObservableProperty, JsonProperty(PropertyName = "desaparecido_id")] private int _desaparecidoId;
    [ObservableProperty, JsonProperty(PropertyName = "marca")] private string? _marca;
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")] private string? _descripcion;
    [ObservableProperty, JsonProperty(PropertyName = "pertenencia")] private Pertenencia? _pertenencia;
    [ObservableProperty, JsonProperty(PropertyName = "color")] private Catalogo? _color;
}