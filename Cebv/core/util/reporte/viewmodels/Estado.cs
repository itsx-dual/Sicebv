using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Estado : ObservableObject
{
    [JsonConstructor]
    public Estado(
        string id,
        string? nombre,
        string? abreviatura_inegi,
        string? abreviatura_cebv,
        string? municipios_count)
    {
        Id = id;
        Nombre = nombre;
        AbreviaturaInegi = abreviatura_inegi;
        AbreviaturaCebv = abreviatura_cebv;
        MunicipiosCount = municipios_count;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((Estado) obj);
    }

    private bool Equals(Estado estado)
    {
        return Id == estado.Id &&
               Nombre == estado.Nombre &&
               AbreviaturaInegi == estado.AbreviaturaInegi &&
               AbreviaturaCebv == estado.AbreviaturaCebv &&
               MunicipiosCount == estado.MunicipiosCount;
    }

    public override string ToString()
    {
        return $"{Nombre}";
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private string _id;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "abreviatura_inegi")]
    private string? _abreviaturaInegi;

    [ObservableProperty, JsonProperty(PropertyName = "abreviatura_cebv")]
    private string? _abreviaturaCebv;

    [ObservableProperty, JsonProperty(PropertyName = "municipios_count")]
    private string? _municipiosCount;
}