using Cebv.features.dashboard.reportes_no_terminados.data;
using Cebv.features.dashboard.reportes_no_terminados.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.reportes_no_terminados.presentation;

public partial class ReportesNoTerminadosViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reportes;

    public ReportesNoTerminadosViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Reportes = await ReportesNoTerminadosRequest.GET();
    }
}