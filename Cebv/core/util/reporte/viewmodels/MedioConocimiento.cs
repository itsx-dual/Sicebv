using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class MedioConocimiento : ObservableObject
{
    [JsonConstructor]
    public MedioConocimiento(
        int? id,
        Catalogo tipo_medio, 
        string? nombre)
    {
        Id = id;
        TipoMedio = tipo_medio;
        Nombre = nombre;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((MedioConocimiento) obj);
    }

    private bool Equals(MedioConocimiento medio)
    {
        return Id == medio.Id &&
               Nombre == medio.Nombre;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "tipo_medio")]
    private Catalogo _tipoMedio;
    
    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;
}