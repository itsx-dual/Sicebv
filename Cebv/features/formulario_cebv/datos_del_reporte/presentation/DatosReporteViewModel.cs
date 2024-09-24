using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.ubicacion.domain;
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
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    
    // Catalogos.
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _instituciones = [];
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = [];
    [ObservableProperty] private ObservableCollection<Estado> _estados = [];
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Opciones;
    
    // Valores seleccionados.
    [ObservableProperty] private Catalogo _tipoMedio;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelectedKey = "No";
    [ObservableProperty] private string _publicacionInformacionSelectedKey = "No";
    
    public DatosReporteViewModel()
    {
        LoadAsync();
    }

    private async Task CargarCatalogos(int tipoMedioId = 1)
    {
        TiposMedios = await CebvNetwork.GetRoute<Catalogo>("tipos-medios");
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", tipoMedioId.ToString());
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        Instituciones = await CebvNetwork.GetRoute<Catalogo>("instituciones");
    }
    
    private async void LoadAsync()
    {
        var reporte = _reporteService.GetReporte();
        await CargarCatalogos(reporte.MedioConocimiento?.TipoMedio.Id ?? 1);
        
        Reporte = reporte;
        TipoMedio = Reporte.MedioConocimiento?.TipoMedio!;

        if (!Reporte.Reportantes.Any())
        {
            Reporte.Reportantes.Add(new Reportante());
        }
        Reportante = Reporte.Reportantes.FirstOrDefault()!;
    }

    async partial void OnTipoMedioChanged(Catalogo value)
    {
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", value.Id.ToString()!);
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}