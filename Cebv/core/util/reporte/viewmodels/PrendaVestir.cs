using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class PrendaVestir : ObservableValidator 
{
    [JsonConstructor]
    public PrendaVestir(int id, string? marca, string? descripcion, Pertenencia? pertenencia, Catalogo? color)
    {
        Id = id;
        Marca = marca;
        Descripcion = descripcion;
        Pertenencia = pertenencia;
        Color = color;
    }

    public PrendaVestir(PrendaVestir prenda)
    {
        Id = prenda.Id;
        DesaparecidoId = prenda.DesaparecidoId;
        Marca = prenda.Marca;
        Descripcion = prenda.Descripcion;
        if (prenda.Pertenencia != null) Pertenencia = new Pertenencia(prenda.Pertenencia);
        Color = prenda.Color;
    }
    
    public PrendaVestir() { }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((PrendaVestir) obj);
    }

    private bool Equals(PrendaVestir prenda)
    {
        var pertenencia =  Pertenencia?.Equals(prenda.Pertenencia) ?? false;
        var color =  Color?.Equals(prenda.Color) ?? false;
        
        return Id == prenda.Id &&
               Descripcion == prenda.Descripcion &&
               Marca == prenda.Marca &&
               pertenencia &&
               color ;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "desaparecido_id")]
    private int _desaparecidoId;
    
    [ObservableProperty, JsonProperty(PropertyName = "marca")]
    private string? _marca;
    
    [ObservableProperty, JsonProperty(PropertyName = "descripcion")]
    private string? _descripcion;
    
    [ObservableProperty, JsonProperty(PropertyName = "pertenencia")]
    [Required(ErrorMessage = "Se necesita seleccionar el tipo de la pertenencia.")]
    private Pertenencia? _pertenencia;
    
    [ObservableProperty, JsonProperty(PropertyName = "color")]
    private Catalogo? _color;

    public void Validar()
    {
        ValidateProperty(Pertenencia, nameof(Pertenencia));
    }
}