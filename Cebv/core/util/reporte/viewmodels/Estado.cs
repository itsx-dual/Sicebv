using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class Estado : ObservableObject
{
    [JsonConstructor]
    public Estado(
        string? id,
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

    [ObservableProperty]
    private string? _id;

    [ObservableProperty]
    private string? _nombre;

    [ObservableProperty]
    private string? _abreviaturaInegi;

    [ObservableProperty]
    private string? _abreviaturaCebv;

    [ObservableProperty]
    private string? _municipiosCount;
}