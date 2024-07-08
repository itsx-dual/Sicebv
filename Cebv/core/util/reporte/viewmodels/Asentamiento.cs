using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Asentamiento : ObservableObject
{
    [JsonConstructor]
    public Asentamiento(
        string id,
        string? nombre,
        string? ambito,
        double? latitud,
        double? longitud,
        int? altitud,
        Municipio? municipio
        )
    {
        Id = id;
        Nombre = nombre;
        Ambito = ambito;
        Latitud = latitud;
        Longitud = longitud;
        Altitud = altitud;
        Municipio = municipio;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Asentamiento) obj);
    }

    private bool Equals(Asentamiento asentamiento)
    {
        return Id == asentamiento.Id &&
               Nombre == asentamiento.Nombre;
    }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private string _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;
    
    [ObservableProperty, JsonProperty(PropertyName = "ambito")]
    private string? _ambito;
    
    [ObservableProperty, JsonProperty(PropertyName = "latitud")]
    private double? _latitud;
    
    [ObservableProperty, JsonProperty(PropertyName = "longitud")]
    private double? _longitud;
    
    [ObservableProperty, JsonProperty(PropertyName = "altitud")]
    private int? _altitud;
    
    [ObservableProperty, JsonProperty(PropertyName = "municipio")]
    private Municipio? _municipio;
}