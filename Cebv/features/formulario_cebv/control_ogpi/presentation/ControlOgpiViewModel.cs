using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.control_ogpi.Data;
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
    
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = ControlOgpiDictionary.GetControlOgpi(Reporte);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            if (dialogo.Confirmacion == "Guardar") confirmacion = true;
            else if (dialogo.Confirmacion == "No guardar") confirmacion = false;
        }
        else confirmacion = true;

        return confirmacion;
    }
    
    [RelayCommand]
    private async Task Guardar()
    {
        if (!await EnlistarCampos())
            return;
        
        _reporteService.Sync();
    }
}