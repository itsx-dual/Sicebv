using System.Collections.ObjectModel;
using static Cebv.core.data.OpcionesCebv;
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

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    public ContextoViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");

        Reporte = _reporteService.GetReporte();
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();


    // Contexto economico - laboral
    [ObservableProperty] private string _gustaTrabajo = No;

    [ObservableProperty] private string _trabajarFuera = No;

    [ObservableProperty] private string _violenciaTrabajo = No;

    [ObservableProperty] private string _tieneDeudas = No;

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