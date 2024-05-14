using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    // Información del reporte.
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes = new();
    [ObservableProperty] private Catalogo _tipoReporte = new();

    // Información del RNPDNO.

    // Información de consentimiento.
    [ObservableProperty] private List<string> _informacionConsentimientoList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _informacionConsentimientoSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionConsentimiento;

    [ObservableProperty] private List<string> _informacionExclusivaBusquedaList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;

    [ObservableProperty] private List<string> _publicacionInformacionList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _publicacionInformacionSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _publicacionInformacion = false;

    // Información de carpeta de investigación.
    [ObservableProperty] private List<string> _carpetaInvestigacionList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _carpetaInvestigacionSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _carpetaInvestigacion = false;

    // Información de amparo de buscador.
    [ObservableProperty] private List<string> _amparoBuscadorList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _amparoBuscadorSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _amparoBuscador = false;

    // Información de recomendación de derechos humanos.
    [ObservableProperty] private List<string> _derechosHumanosList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _derechosHumanosSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _derechosHumanos = false;
    [ObservableProperty] private bool? _otroDerechoHumano = false;

    /**
     * Constructor de la clase.
     */
    public DatosReporteViewModel()
    {
        CargarCatalogos();
    }

    /**
     *  Mapeo de los valores string a boolean.
     */
    partial void OnInformacionConsentimientoSelectedChanged(string value) =>
        InformacionConsentimiento = OpcionesCebv.MappingToBool(value);

    partial void OnInformacionExclusivaBusquedaSelectedChanged(string value) =>
        InformacionExclusivaBusqueda = OpcionesCebv.MappingToBool(value);

    partial void OnPublicacionInformacionSelectedChanged(string value) =>
        PublicacionInformacion = OpcionesCebv.MappingToBool(value);

    partial void OnCarpetaInvestigacionSelectedChanged(string value) =>
        CarpetaInvestigacion = OpcionesCebv.MappingToBool(value);

    partial void OnAmparoBuscadorSelectedChanged(string value) =>
        AmparoBuscador = OpcionesCebv.MappingToBool(value);

    partial void OnDerechosHumanosSelectedChanged(string value) =>
        DerechosHumanos = OpcionesCebv.MappingToBool(value);
    
    /**
     * Comandos de la vista.
     */
    [RelayCommand]
    public void GuardarReporte()
    {
        ReporteRequest reporte = new()
        {
            TipoReporte = TipoReporte,
        };

        WeakReferenceMessenger.Default.Send(new AddReporteMessage(reporte));
    }

    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos()
    {
        TiposReportes = await ReporteNetwork.GetTiposReportes();
    }
}