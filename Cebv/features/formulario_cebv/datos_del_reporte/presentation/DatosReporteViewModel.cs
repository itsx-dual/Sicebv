using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.ubicacion.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    /**
     * Constructor de la clase.
     */
    public DatosReporteViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        var tipoMedioId = _reporteService.GetReporte().MedioConocimiento?.TipoMedio.Id;
        Medios = await DatosReporteNetwork.GetMedios(tipoMedioId ?? 1);
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
        Estados = await UbicacionNetwork.GetEstados();
        
        Reporte = _reporteService.GetReporte();
        TipoMedio = Reporte.MedioConocimiento?.TipoMedio!;
        
        // Esta seccion del formulario lidia con dos atributos de reportante.
        if (Reporte.Reportantes?.Count == 0)
        {
            Reporte.Reportantes?.Add(new Reportante());
        }
    }

    [ObservableProperty] private Reporte _reporte;
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    
    /**
     * Fuente de información.
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private Catalogo _tipoMedio;
    
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados;

    /**
     * Información de consentimiento.
     */
    [ObservableProperty] private Dictionary<string, bool?> _informacionExclusivaBusquedaList = OpcionesCebv.Ops;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelectedKey = "No";
    
    [ObservableProperty] private Dictionary<string, bool?> _publicacionInformacionList = OpcionesCebv.Ops;
    [ObservableProperty] private string _publicacionInformacionSelectedKey = "No";

    async partial void OnTipoMedioChanged(Catalogo value)
    {
        Medios = await DatosReporteNetwork.GetMedios(value.Id);
    }

    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}