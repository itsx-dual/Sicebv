using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class TipoReporte : ObservableObject
{
    [JsonConstructor]
    public TipoReporte(int? id, string? nombre, string? abreviatura)
    {
        Id = id;
        Nombre = nombre;
        Abreviatura = abreviatura;
    }
    
    [ObservableProperty]
    private int? _id;
    
    [ObservableProperty]
    private string? _nombre;
    
    [ObservableProperty]
    private string? _abreviatura;
}