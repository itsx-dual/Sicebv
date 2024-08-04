using Cebv.core.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.contexto.presentation;

public partial class ContextoViewModel : ObservableObject
{
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    
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
    
    /**
     * Guardar y continuar
     */
    [RelayCommand]
    private void GuardarContinuar(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}