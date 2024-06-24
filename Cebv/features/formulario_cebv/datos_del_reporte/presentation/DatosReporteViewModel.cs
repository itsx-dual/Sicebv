using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.modules.ubicacion.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
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
        // El orden de carga de los catalogos es irrelevante, pero lo ultimo que hay que cargar
        // Es el reporte, lo cual activara todos los SelectedItems.
        //
        // Si se asigna el reporte varias veces, los SelectedItems pierden lsa referencias, asi que hay que cargar
        // una vez se cersiora uno de que los catalogos tienen datos que hay dentro de Reporte.
        
        var tipoMedioId = _reporteService.GetReporteActual().MedioConocimiento?.TipoMedio?.Id;
        Medios = await DatosReporteNetwork.GetMedios(tipoMedioId);
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
        Estados = await UbicacionNetwork.GetEstados();
        
        Reporte = _reporteService.GetReporteActual();
    }

    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    [ObservableProperty] private Reporte _reporte;
    
    /**
     * Fuente de información.
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios;
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados;

    /**
     * Información de consentimiento.
     */
    [ObservableProperty] private Dictionary<string, bool?> _informacionExclusivaBusquedaList = OpcionesCebv.Ops;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelectedKey = "No";
    
    [ObservableProperty] private Dictionary<string, bool?> _publicacionInformacionList = OpcionesCebv.Ops;
    [ObservableProperty] private string _publicacionInformacionSelectedKey = "No";

    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
    {
        var informacion = new InicioPostObject
        {
            TipoMedio = (int) Reporte.MedioConocimiento?.TipoMedio.Id!,
            Medio = (int) Reporte.MedioConocimiento.Id!,
            DependenciaOrigen = Reporte.InstitucionOrigen!,
            TipoReporte = (int) Reporte.TipoReporte?.Id!,
            Estado = Reporte.Estado?.Id!,
            InformacionExclusivaBusqueda = Reporte.Reportantes?[0].InformacionExclusivaBusqueda,
            PublicacionInformacion = Reporte.Reportantes?[0].PublicacionBoletin
        };
        
        
        if (_reporteService.SendInformacionInicio(informacion)) _navigationService.Navigate(pageType);
    }
}