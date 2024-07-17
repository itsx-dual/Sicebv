using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

/// <summary>
/// Representacion del reporte dado por el enpoint de
/// "/api/reportes".
/// </summary>
public partial class Reportes : ObservableObject
{
    [JsonConstructor]
    public Reportes(Reporte data)
    {
        Data = data;
    }
    
    [ObservableProperty]
    private Reporte _data;
}