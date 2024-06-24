using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.util.reporte.viewmodels;

public partial class TipoHipotesis : ObservableObject
{
    [JsonConstructor]
    public TipoHipotesis(int? id,
        string? abreviatura,
        string? descripcion,
        (int Id, int Descripcion)? circunstancia)
    {
        Id = id;
        Abreviatura = abreviatura;
        Descripcion = descripcion;
        Circunstancia = circunstancia;
    }
    
    [ObservableProperty] int? _id;
    [ObservableProperty] string? _abreviatura;
    [ObservableProperty] string? _descripcion;
    [ObservableProperty] (int Id, int Descripcion)? _circunstancia;
    
    
}