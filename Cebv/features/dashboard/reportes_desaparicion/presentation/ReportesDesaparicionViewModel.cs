using System.Collections.ObjectModel;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.dashboard.reportes_desaparicion.data;
using Cebv.features.dashboard.reportes_desaparicion.domain;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.dashboard.reportes_desaparicion.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    private static IDashboardNavigationService _dashboardNavigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    [ObservableProperty] private ObservableCollection<ReporteCompactResource> _reportes = new();
    [ObservableProperty] private int _totalPaginas;
    [ObservableProperty] private int _paginaActual = 1;
    [ObservableProperty] private ReporteCompactResource _reporteSelected;

    public ReportesDesaparicionViewModel()
    {
        InitAsync();
    }

    private async Task InitAsync()
    {
        var reportes =  await ReportesDesaparicionNetwork.GetReportes(PaginaActual);
        TotalPaginas = reportes.Meta.LastPage ?? 0;
        
        foreach (var reporte in reportes.Data)
        {
            Reportes.Add(reporte);
        }
    }

    [RelayCommand]
    private async Task OnEndingScrolling()
    {
        if (PaginaActual >= TotalPaginas) return;
        PaginaActual++;
        var reportes = await ReportesDesaparicionNetwork.GetReportes(PaginaActual);
        foreach (var reporte in reportes.Data)
        {
            Reportes.Add(reporte);
        }
    }

    [RelayCommand]
    private async void OnReporteClick()
    {
        if (ReporteSelected == null) return;
        await _reporteService.Reload(ReporteSelected.Id);
        _reporteService.SetStatusReporte(EstadoReporte.Cargado);
        _dashboardNavigationService.Navigate(typeof(FormularioCebvPage));
    }
}