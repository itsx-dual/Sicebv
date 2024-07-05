using System.Collections.ObjectModel;
using System.Reflection;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.persona.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = [];
    [ObservableProperty] private ReporteResponse _reporteSelected;
    
    public IDashboardNavigationService DashboardNavigationService { get; set; }
    public IReporteService ReporteService { get; set; }

    public ReportesDesaparicionViewModel( IDashboardNavigationService navigationService, IReporteService reporteService)
    {
        DashboardNavigationService = navigationService;
        ReporteService = reporteService;
        CargarReportes();
    }

    private async void CargarReportes()
    {
        Reportes = await ReporteNetwork.GetReportes();
    }

    [RelayCommand]
    public void OnReporteClick()
    {
        ReporteService.ClearReporteActual();
        ReporteService.SetStatusReporteActual(EstadoReporte.Cargado);
        ReporteService.SetReporteActual(ReporteSelected);

        string nombre = string.Empty;
        if (ReporteSelected.Desaparecidos.Count > 0)
        {
            var reportado = ReporteSelected.Desaparecidos?.FirstOrDefault();
            nombre = $"{reportado.Persona?.Nombre} {reportado.Persona?.ApellidoPaterno} {reportado.Persona?.ApellidoMaterno}";
        }
        
        FormularioCebvViewModel dataContext = new()
        {
            NombreCompleto = nombre,
            callerType = GetType()
        };
        
        DashboardNavigationService.Navigate(typeof(FormularioCebvPage), dataContext, GetType());
    }
}