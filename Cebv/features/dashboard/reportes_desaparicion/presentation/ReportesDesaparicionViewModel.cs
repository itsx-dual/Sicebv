using System.Collections.ObjectModel;
using Cebv.features.dashboard.reportes_desaparicion.data;
using Cebv.features.dashboard.reportes_desaparicion.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.dashboard.reportes_desaparicion.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ReporteCompactResource> _reportes = new();
    [ObservableProperty] private int _totalPaginas;
    [ObservableProperty] private int _paginaActual = 1;

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
}