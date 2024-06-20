using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    /**
     * Constructor de la clase.
     */
    public DatosReporteViewModel()
    {
        CargarCatalogos();
    }

    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>();
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>();
    
    [ObservableProperty] private DateTime? _fecha = DateTime.Now;
    
    /**
     * Fuente de información.
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private Catalogo _tipoMedio = new();
    [ObservableProperty] private ObservableCollection<Medio> _medios = new();
    [ObservableProperty] private Medio _medio = new();
    [ObservableProperty] private string _dependenciaOrigen = string.Empty;
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    /**
     * Información de consentimiento.
     */
    [ObservableProperty] private Dictionary<string, bool?> _informacionExclusivaBusquedaList = OpcionesCebv.Ops;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelectedKey = "No";
    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;
    [ObservableProperty] private Dictionary<string, bool?> _publicacionInformacionList = OpcionesCebv.Ops;
    [ObservableProperty] private string _publicacionInformacionSelectedKey = "No";
    [ObservableProperty] private bool? _publicacionInformacion = false;
    
    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos() {
        TiposMedios = await ReporteNetwork.GetTiposMedios();
        
        if (_reporteService.HayReporte())
        {
            var reporte = _reporteService.GetReporteActual();
            var reportante = reporte.Reportantes.First();
            
            Fecha = reporte.FechaCreacion;
            TipoMedio = TiposMedios.Where(catalogo => catalogo.Id == reporte.MedioConocimiento.TipoMedio.Id).First();
            Medios = await ReporteNetwork.GetMedios(reporte.MedioConocimiento.TipoMedio.Id);
            Medio = Medios.Where(medio => medio.Id == reporte.MedioConocimiento.Id).First();
            Ubicacion.Estado = Ubicacion.Estados.Where(estado => estado.Id == reporte.Estado.Id).First();

            InformacionExclusivaBusqueda = reportante.InformacionExclusivaBusqueda;
            PublicacionInformacion = reportante.PublicacionBoletin;
        }
    }

    async partial void OnTipoMedioChanged(Catalogo value) =>
        Medios = await ReporteNetwork.GetMedios(value.Id);

    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
    {
        var informacion = new InicioPostObject
        {
            Medio = Medio.Id,
            TipoReporte = 1,
            Estado = Ubicacion.Estado.Id,
            InformacionExclusivaBusqueda = InformacionExclusivaBusqueda,
            PublicacionInformacion = PublicacionInformacion
        };
        
        if (_reporteService.SendInformacionInicio(informacion)) _navigationService.Navigate(pageType);
    }
}