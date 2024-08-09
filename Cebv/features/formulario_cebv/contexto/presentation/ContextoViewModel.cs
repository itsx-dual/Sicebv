using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.contexto.presentation;

public partial class ContextoViewModel : ObservableObject
{
    private IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    public ContextoViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Parentescos = await CebvNetwork.GetCatalogo("parentescos");

        Reporte = _reporteService.GetReporte();
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();


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
     * Comando para guardar y navegar a la siguiente pagina
     */
    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}