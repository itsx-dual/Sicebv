using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.contexto.presentation;

public partial class ContextoViewModel : ObservableObject
{
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;
    
    // Contexto economico - laboral
    [ObservableProperty] private string _gustaTrabajoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _gustaTrabajo = false;
    
    [ObservableProperty] private string _trabajarFueraOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _trabajarFuera = false;
    
    [ObservableProperty] private string _violenciaTrabajoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _violenciaTrabajo = false;
    
    [ObservableProperty] private string _tieneDeudasOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _tieneDeudas = false;
    
}