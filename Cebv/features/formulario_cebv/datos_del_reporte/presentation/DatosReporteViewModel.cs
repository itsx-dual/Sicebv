using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
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

        var reporte = _reporteService.GetReporte();
        
        var tipoMedio = reporte.MedioConocimiento?.TipoMedio;

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

    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}