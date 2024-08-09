using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Municipio : ObservableObject
{
   [JsonConstructor]
   public Municipio(string id, string? nombre, Estado? estado, Catalogo? area_atiende)
   {
      Id = id;
      Nombre = nombre;
      Estado = estado;
      AreaAtiende = area_atiende;
   }

   public Municipio() { }

   public override string ToString()
   {
      return $"{Nombre} - {Id}";
   }

   public override bool Equals(object? obj)
   {
      if (ReferenceEquals(this, obj)) return true; // Same object reference
      if (ReferenceEquals(obj, null)) return false; // Other object is null
      if (obj.GetType() != GetType()) return false; // Different types

      return Equals((Municipio) obj);
   }

   private bool Equals(Municipio municipio)
   {
      return Id == municipio.Id &&
             Nombre == municipio.Nombre;
   }
   
   [ObservableProperty, JsonProperty(PropertyName = "id")]
   private string _id;
   
   [ObservableProperty, JsonProperty(PropertyName = "nombre")]
   private string? _nombre;
   
   [ObservableProperty, JsonProperty(PropertyName = "estado")]
   private Estado? _estado;
   
   [ObservableProperty, JsonProperty(PropertyName = "area_atiende")]
   private Catalogo? _areaAtiende;
}