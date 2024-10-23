using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    // Referente a servicios.
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = new();

    public DatosReporteViewModel()
    {
        InitAsync();
    }

    private async void InitAsync()
    {
        TiposMedios = await CebvNetwork.GetRoute<Catalogo>("tipos-medios");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        Instituciones = await CebvNetwork.GetRoute<Catalogo>("instituciones");

        var tipoMedio = _reporteService.GetReporte().MedioConocimiento?.TipoMedio;

        if (tipoMedio is not null)
        {
            Medios = await CebvNetwork
                .GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", tipoMedio.Id.ToString()!);
            TipoMedio = tipoMedio;
        }
        
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Reportantes.Any()) Reporte.Reportantes.Add(Reportante);

        Reportante = Reporte.Reportantes.FirstOrDefault()!;
    }

    // Catalogos.
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _instituciones = [];
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = [];
    [ObservableProperty] private ObservableCollection<Estado> _estados = [];
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Opciones;

    // Valores seleccionados.
    [ObservableProperty] private Catalogo? _tipoMedio;

    async partial void OnTipoMedioChanged(Catalogo? value)
    {
        if (value?.Id is null) return;
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", value.Id.ToString()!);
    }
    
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion;

        var properties = ListEmptyElements.GetDatosReporte(Reporte, this, Reportante);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            confirmacion = dialogo.Confirmacion;
        }
        else confirmacion = true;

        return confirmacion;
    }

    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        if (!await EnlistarCampos())
            return;
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}