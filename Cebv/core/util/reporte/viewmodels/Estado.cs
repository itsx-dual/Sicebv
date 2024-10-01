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
        string? abreviaturaInegi,
        string? abreviaturaCebv
    )
    {
        Id = id;
        Nombre = nombre;
        AbreviaturaInegi = abreviaturaInegi;
        AbreviaturaCebv = abreviaturaCebv;
    }

    public Estado()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != this.GetType()) return false; // Different types

        return Equals((Estado)obj);
    }

    private bool Equals(Estado estado)
    {
        return Id == estado.Id &&
               Nombre == estado.Nombre &&
               AbreviaturaInegi == estado.AbreviaturaInegi &&
               AbreviaturaCebv == estado.AbreviaturaCebv;
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
}