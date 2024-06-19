using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.media_filiacion_complementaria.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;

public partial class MediaFiliacionComplementariaViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public MediaFiliacionComplementariaViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    // Dientes
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _dienteOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _diente = false;
    [ObservableProperty] private string _dienteDescripcion = string.Empty;

    [ObservableProperty] private string _tratamientoDentalOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _tratamientoDental = false;
    [ObservableProperty] private string _tratamientoDentalDescripcion = string.Empty;

    // Proyección del mentón
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMentones = new();
    [ObservableProperty] private Catalogo _tipoMenton = new();
    [ObservableProperty] private string _tipoMentonDescripcion = string.Empty;

    // Intervenciones quirúrgicas
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposIntervenciones = new();
    [ObservableProperty] private Catalogo _tipoIntervencion = new();
    [ObservableProperty] private string _tipoIntervencionDescripcion = string.Empty;

    // Enfermedades en la piel
    [ObservableProperty] private ObservableCollection<Catalogo> _enfermedadesPieles = new();
    [ObservableProperty] private Catalogo _enfermedadPiel = new();
    [ObservableProperty] private string _enfermedadPielDescripcion = string.Empty;

    // Observaciones generales
    [ObservableProperty] private string _observacionesGenerales = string.Empty;

    /**
     * Peticiones a la API para cargar los catálogos
     */
    private async void CargarCatalogos()
    {
        TiposMentones = await MediaFiliacionComplementariaNetwork.GetTiposMentones();
        TiposIntervenciones = await MediaFiliacionComplementariaNetwork.GetIntervencionesQuirurgicas();
        EnfermedadesPieles = await MediaFiliacionComplementariaNetwork.GetEnfermedadesPiel();
    }


    partial void OnDienteOpcionChanged(string value) =>
        Diente = OpcionesCebv.MappingToBool(value);
    
    partial void OnTratamientoDentalOpcionChanged(string value) =>
        TratamientoDental = OpcionesCebv.MappingToBool(value);
}