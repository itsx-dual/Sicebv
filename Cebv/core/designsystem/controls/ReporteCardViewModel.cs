using Cebv.core.data;
using Cebv.core.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.core.designsystem.controls;

public partial class ReporteCardViewModel : ObservableObject
{
    private int ReporteId { get; set; }
    [ObservableProperty] private Toggle _reporteToggle = new();

    public ReporteCardViewModel(int reporteId)
    {
        ReporteId = reporteId;
    }

    [RelayCommand]
    private async Task OnToggleReporte()
    {
        ReporteToggle = await CebvNetwork.GetObject<Toggle>("reportes", $"{ReporteId}/toggle");
    }
}