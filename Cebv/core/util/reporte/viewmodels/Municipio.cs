using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Municipio : ObservableObject
{
   [JsonConstructor]
   public Municipio(string id, string? nombre, Estado? estado)
   {
      Id = id;
      Nombre = nombre;
      Estado = estado;
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
}