using Cebv.core.modules.ubicacion.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.datos_complementarios.presentation;

public partial class DatosComplemenatiosViewModel : ObservableObject
{
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();
}