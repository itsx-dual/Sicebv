using System.Collections.ObjectModel;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ReporteResponse> _reportes = new();

    public ReportesDesaparicionViewModel()
    {
        CargarReportes();
    }

    private async void CargarReportes()
    {
        Reportes = await ReporteNetwork.GetReportes();
    }
}