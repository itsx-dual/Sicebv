using Cebv.features.dashboard.data;
using Cebv.features.dashboard.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.presentation;

public partial class ReportesDesaparicionViewModel : ObservableObject
{
    [ObservableProperty] private List<Reporte> _reportes;

    public ReportesDesaparicionViewModel()
    {
        _cargarReportes();
    }

    private async void _cargarReportes()
    {
        Reportes = await DashboardNetwork.GetReportesRequest();
    }
}