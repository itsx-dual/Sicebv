using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.control_ogpi.presentation;

public partial class ControlOgpiViewModel : ObservableObject
{
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    public ControlOgpiViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        EstatusPersonas = await CebvNetwork.GetRoute<BasicResource>("estatus-personas");
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Reporte.ControlOgpi ??= new();
    }

    [ObservableProperty] private ObservableCollection<BasicResource> _estatusPersonas = new();


    [RelayCommand]
    private void Guardar()
    {
        _reporteService.Sync();
    }
}