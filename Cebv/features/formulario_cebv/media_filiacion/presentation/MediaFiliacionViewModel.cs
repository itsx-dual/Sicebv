using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Cebv.core.data;
using Cebv.features.formulario_cebv.media_filiacion.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public MediaFiliacionViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    // Perfil corporal
    [ObservableProperty] private float _estatura;

    [ObservableProperty] private float _peso;

    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();
    [ObservableProperty] private Catalogo _complexion = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPieles = new();
    [ObservableProperty] private Catalogo _colorPiel = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasCaras = new();
    [ObservableProperty] private Catalogo _formaCara = new();

    // Ojos
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos = new();
    [ObservableProperty] private Catalogo _colorOjo = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasOjos = new();
    [ObservableProperty] private Catalogo _formaOjo = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOjos = new();
    [ObservableProperty] private Catalogo _tamanoOjo = new();

    [ObservableProperty] private string _descripcionOjos = String.Empty;

    // Cabello
    [ObservableProperty] private ObservableCollection<Catalogo> _calvicies = new();
    [ObservableProperty] private Catalogo _calvicie = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabellos = new();
    [ObservableProperty] private Catalogo _colorCabello = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabellos = new();
    [ObservableProperty] private Catalogo _tamanoCabello = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabellos = new();
    [ObservableProperty] private Catalogo _tipoCabello = new();

    [ObservableProperty] private string _descripcionCabello = String.Empty;

    // Vello facial
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCejas = new();
    [ObservableProperty] private Catalogo _tipoCeja = new();
    [ObservableProperty] private string _descripcionCejas = String.Empty;

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _bigoteOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _bigote = false;
    [ObservableProperty] private string _bigoteDescripcion = String.Empty;

    [ObservableProperty] private string _barbaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _barba = false;
    [ObservableProperty] private string _barbateDescripcion = String.Empty;

    // Nariz
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposNarices = new();
    [ObservableProperty] private Catalogo _tipoNariz = new();
    [ObservableProperty] private string _descripcionNariz = String.Empty;

    // Boca
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosBocas = new();
    [ObservableProperty] private Catalogo _tamanoBoca = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosLabios = new();
    [ObservableProperty] private Catalogo _tamanoLabio = new();

    [ObservableProperty] private string _descripcionBoca = String.Empty;

    // Orejas
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOrejas = new();
    [ObservableProperty] private Catalogo _tamanoOreja = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasOrejas = new();
    [ObservableProperty] private Catalogo _formaOreja = new();

    [ObservableProperty] private string _descripcionOreja = String.Empty;


    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void CargarCatalogos()
    {
        Complexiones = await MediaFiliacionNetwork.GetComplexiones();
        ColoresPieles = await MediaFiliacionNetwork.GetColoresPieles();
        FormasCaras = await MediaFiliacionNetwork.GetFormasCaras();
        ColoresOjos = await MediaFiliacionNetwork.GetColoresOjos();
        FormasOjos = await MediaFiliacionNetwork.GetFormasOjos();
        TamanosOjos = await MediaFiliacionNetwork.GetTamanosOjos();
        Calvicies = await MediaFiliacionNetwork.GetTiposCalvicies();
        ColoresCabellos = await MediaFiliacionNetwork.GetColoresCabellos();
        TamanosCabellos = await MediaFiliacionNetwork.GetTamanosCabellos();
        TiposCabellos = await MediaFiliacionNetwork.GetTiposCabellos();
        TiposCejas = await MediaFiliacionNetwork.GetTiposCejas();
        TiposNarices = await MediaFiliacionNetwork.GetTiposNarices();
        TamanosBocas = await MediaFiliacionNetwork.GetTamanosBocas();
        TamanosLabios = await MediaFiliacionNetwork.GetTamanosLabios();
        TamanosOrejas = await MediaFiliacionNetwork.GetTamanosOrejas();
        FormasOrejas = await MediaFiliacionNetwork.GetFormasOrejas();
    }
}