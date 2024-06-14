using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciaDesaparicionViewModel : ObservableObject
{
    public CircunstanciaDesaparicionViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private DateTime? _fechaDesaparicion;
    [ObservableProperty] private string _fechaDesaparicionCebv = String.Empty;
    [ObservableProperty] private string _horaDesaparicion = String.Empty;

    [ObservableProperty] private DateTime? _fechaPercato;
    [ObservableProperty] private string _fechaPercatoCebv = String.Empty;
    [ObservableProperty] private string _horaPercato = String.Empty;

    [ObservableProperty] private string _aclaracionHechos = String.Empty;

    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _amenazaCambioComportamientoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _amenazaCambioComportamiento = false;
    [ObservableProperty] private string _amenazaDescripcion = String.Empty;

    [ObservableProperty] private int _contadorDesaparicion;
    [ObservableProperty] private string _situacionPreviaDescripcion = String.Empty;
    [ObservableProperty] private string _foliosPrevios = String.Empty;
    [ObservableProperty] private string _datosPersonasRealacionadas = String.Empty;
    [ObservableProperty] private string _descripcionHechosDesaparicion = String.Empty;
    [ObservableProperty] private string _sintesisHechosDesaparicion = String.Empty;

    // Hipotesis
    [ObservableProperty] private ObservableCollection<TipoHipotesis> _tiposHipotesis = new();
    [ObservableProperty] private TipoHipotesis _tipoHipotesisUno = new();
    [ObservableProperty] private TipoHipotesis _tipoHipotesisDos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _sitios = new();
    [ObservableProperty] private Catalogo _sitio = new();

    [ObservableProperty] private string _areaCodifica = String.Empty;

    // Desaparicion asociada
    [ObservableProperty] private string _desaparecioAcompanadoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _desaparecioAcompanado = false;

    [ObservableProperty] private int _numeroPersonasMismoEvento = 1;


    /**
     * Peticiones a la red
     */
    private async void CargarCatalogos()
    {
        TiposHipotesis = await CircunstanciaDesaparicionNetwork.GetTiposHipotesis();
        Sitios = await CircunstanciaDesaparicionNetwork.GetSitios();
    }
}