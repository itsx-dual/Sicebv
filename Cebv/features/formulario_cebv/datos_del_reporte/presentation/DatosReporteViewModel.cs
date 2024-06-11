using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.modules.ubicacion.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

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
    [ObservableProperty] private List<string> _informacionExclusivaBusquedaList = OpcionesCebv.Opciones;

    [ObservableProperty] private string _informacionExclusivaBusquedaSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;

    [ObservableProperty] private List<string> _publicacionInformacionList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _publicacionInformacionSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _publicacionInformacion = false;

    /**
     *  Mapeo de los valores string a boolean.
     */
    partial void OnInformacionExclusivaBusquedaSelectedChanged(string value) =>
        InformacionExclusivaBusqueda = OpcionesCebv.MappingToBool(value);

    partial void OnPublicacionInformacionSelectedChanged(string value) =>
        PublicacionInformacion = OpcionesCebv.MappingToBool(value);
    
    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos() =>
        TiposMedios = await ReporteNetwork.GetTiposMedios();

    async partial void OnTipoMedioChanged(Catalogo value) =>
        Medios = await ReporteNetwork.GetMedios(value.Id);
}