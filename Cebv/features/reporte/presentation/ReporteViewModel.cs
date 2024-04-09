using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.reporte.presentation;

public partial class ReporteViewModel : ObservableObject
{
    [ObservableProperty] private ReporteWrapped _reportes;

    [RelayCommand]
    public async void Catalogos()
    {
        Reportes = await ReporteNetwork.GetReportes();
    }
}