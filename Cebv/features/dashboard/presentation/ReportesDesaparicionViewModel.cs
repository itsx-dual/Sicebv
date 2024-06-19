using System.Collections.ObjectModel;
using System.Reflection;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.persona.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

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
        var dashboardNavigation = App.Current.Services.GetService<IDashboardNavigationService>();
        if (dashboardNavigation == null) return;

        var reporteService = App.Current.Services.GetService<IReporteService>();
        reporteService?.ClearReporteActual();
        reporteService?.SetStatusReporteActual(EstadoReporte.Cargado);
        reporteService?.SetReporteActual(ReporteSelected);

        var reportado = ReporteSelected.Desaparecidos.First();

        FormularioCebvViewModel dataContext = new()
        {
            NombreCompleto = $"{reportado.Persona.Nombre} {reportado.Persona.ApellidoPaterno} {reportado.Persona.ApellidoMaterno}",
            callerType = GetType()
        };
        
        dashboardNavigation.Navigate(typeof(FormularioCebvPage), dataContext, GetType());
    }
}