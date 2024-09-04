using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Direccion : ObservableObject
{
    [JsonConstructor]
    public Direccion(
        int id,
        string? calle,
        string? colonia,
        string? numero_exterior,
        string? numero_interior,
        string? calle_1,
        string? calle_2,
        string? tramo_carretero,
        string? codigo_postal,
        string? referencia,
        Asentamiento? asentamiento
        )
    {
        Id = id;
        Calle = calle;
        Colonia = colonia;
        NumeroExterior = numero_exterior;
        NumeroInterior = numero_interior;
        Calle1 = calle_1;
        Calle2 = calle_2;
        TramoCarretero = tramo_carretero;
        CodigoPostal = codigo_postal;
        Referencia = referencia;
        Asentamiento = asentamiento;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Direccion) obj);
    }

    private bool Equals(Direccion direccion)
    {
        return Id == direccion.Id;
    }
    
    public override int GetHashCode() //wao
    {
        return HashCode.Combine(Id, Calle);
    }

    public Direccion() { }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "calle")]
    private string? _calle;
    
    [ObservableProperty, JsonProperty(PropertyName = "colonia")]
    private string? _colonia;
    
    [ObservableProperty, JsonProperty(PropertyName = "numero_exterior")]
    private string? _numeroExterior;
    
    [ObservableProperty, JsonProperty(PropertyName = "numero_interior")]
    private string? _numeroInterior;
    
    [ObservableProperty, JsonProperty(PropertyName = "calle_1")]
    private string? _calle1;
    
    [ObservableProperty, JsonProperty(PropertyName = "calle_2")]
    private string? _calle2;
    
    [ObservableProperty, JsonProperty(PropertyName = "tramo_carretero")]
    private string? _tramoCarretero;
    
    [ObservableProperty, JsonProperty(PropertyName = "codigo_postal")]
    private string? _codigoPostal;
    
    [ObservableProperty, JsonProperty(PropertyName = "referencia")]
    private string? _referencia;
    
    [ObservableProperty, JsonProperty(PropertyName = "asentamiento")]
    private Asentamiento? _asentamiento;
}