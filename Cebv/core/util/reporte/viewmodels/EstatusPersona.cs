using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.util.reporte.viewmodels;

public partial class EstatusPersona : ObservableObject
{
    [JsonConstructor]
    public EstatusPersona(
        int? id,
        string? nombre,
        string? abreviatura)
    {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
    }
    
    [ObservableProperty] int? _id;
    [ObservableProperty] string? _nombre;
    [ObservableProperty] string? _abreviatura;
}