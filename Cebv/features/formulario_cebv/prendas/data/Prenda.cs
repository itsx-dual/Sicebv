using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.prendas.data;

public partial class Prenda : ObservableObject
{
    [ObservableProperty] private Catalogo? _grupoPertenencia;
    [ObservableProperty] private Catalogo _pertenencia = new();
    [ObservableProperty] private Catalogo? _color;
    [ObservableProperty] private string? _marca;
    [ObservableProperty] private string? _descripcion;
}