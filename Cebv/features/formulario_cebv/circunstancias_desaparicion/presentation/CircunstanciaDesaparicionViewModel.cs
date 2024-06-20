using System.Collections.ObjectModel;
using System.Diagnostics;
using Cebv.core.data;
using Cebv.core.modules.persona.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciaDesaparicionViewModel : ObservableObject
{
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    
    
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
    
    
    /**
     * Logica de busqueda
     */
    [ObservableProperty] private string? _nombreDirecto;
    [ObservableProperty] private string? _nombreIndirecto;
    [ObservableProperty] private string? _primerApellidoDirecto;
    [ObservableProperty] private string? _primerApellidoIndirecto;
    [ObservableProperty] private string? _segundoApellidoDirecto;
    [ObservableProperty] private string? _segundoApellidoIndirecto;
    
    [ObservableProperty] private ObservableCollection<Persona> _personas = new();
    
    [RelayCommand]
    private async Task BuscarPersona()
    {
        Personas =  await CircunstanciaDesaparicionNetwork.BuscarPersona(NombreDirecto, PrimerApellidoDirecto, SegundoApellidoDirecto);
        Console.WriteLine(Personas.Count);
    }
    
    
    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.UbicacionHechos = Ubicacion;
        _navigationService.Navigate(pageType);
    }
}