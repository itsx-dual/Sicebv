using System.Collections.ObjectModel;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.util.navigation;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.dashboard.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = [];
    [ObservableProperty] private ReporteResponse _reporteSelected;

    public ReportesDesaparicionViewModel()
    {
        CargarReportes();
    }

    private async void CargarReportes()
    {
        Reportes = await ReporteNetwork.GetReportes();
    }

    [RelayCommand]
    public void OnReporteClick()
    {
        var dashboardNavigarion = App.Current.Services.GetService<IDashboardNavigationService>();
        if (dashboardNavigarion == null) return;

        var desaparecido = ReporteSelected.Desaparecidos.First();
        
        FormularioCebvViewModel dataContext = new()
        {
            NombreCompleto = $"{desaparecido.Persona.Nombre} {desaparecido.Persona.ApellidoPaterno} {desaparecido.Persona.ApellidoMaterno}",
        };

        dashboardNavigarion.Navigate(typeof(FormularioCebvPage), dataContext);
    }
}