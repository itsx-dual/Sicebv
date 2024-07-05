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
    public IFormularioCebvNavigationService FormularioNavigationService { get; set; }
    public IReporteService ReporteService { get; set; }
    
    public CircunstanciaDesaparicionViewModel(IFormularioCebvNavigationService formularioNavigationService, IReporteService reporteService)
    {
        FormularioNavigationService = formularioNavigationService;
        ReporteService = reporteService;
        
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

    [ObservableProperty] private ObservableCollection<Persona> _personasDirectas = new();
    [ObservableProperty] private ObservableCollection<Persona> _personasIndirectas = new();
    [ObservableProperty] private ObservableCollection<Expediente> _expedientesDirectos = new();
    [ObservableProperty] private ObservableCollection<Expediente> _expedientesIndirectos = new();

    [RelayCommand]
    private async Task BuscarPersonaDirecta()
    {
        PersonasDirectas = await CircunstanciaDesaparicionNetwork.BuscarPersona(
            NombreDirecto,
            PrimerApellidoDirecto,
            SegundoApellidoDirecto
        );
        Console.WriteLine(PersonasDirectas.Count);
    }

    [RelayCommand]
    private async Task BuscarPersonaIndirecta()
    {
        PersonasIndirectas = await CircunstanciaDesaparicionNetwork.BuscarPersona2(
            NombreIndirecto,
            PrimerApellidoIndirecto,
            SegundoApellidoIndirecto
        );
        Console.WriteLine(PersonasIndirectas.Count);
    }

    [RelayCommand]
    private void AddExpedienteDirecto(Persona persona)
    {
        if (ExpedientesDirectos.Any(p => p.Persona.Id == persona.Id)) return;

        var viewModel = new AgregarExpedienteViewModel(persona);

        // Suscribirse al evento de guardado
        viewModel.GuardarExpediente += OnExpedienteDirectoGuardado;

        // Abrir la ventana de edición de la prenda
        var dialog = new AgregarExpediente { DataContext = viewModel };

        // Configurar la acción de cierre para la ventana de edición
        if (dialog.DataContext is AgregarExpedienteViewModel vm)
        {
            vm.CloseAction = () => dialog.Close();
        }

        dialog.ShowDialog();
    }

    private void OnExpedienteDirectoGuardado(object? sender, Expediente expediente)
    {
        if (sender is not AgregarExpedienteViewModel vm) return;

        ExpedientesDirectos.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpedienteDirecto(Expediente expediente)
    {
        ExpedientesDirectos.Remove(expediente);
    }

    [RelayCommand]
    private void AddExpedienteIndirecto(Persona persona)
    {
        if (ExpedientesIndirectos.Any(p => p.Persona.Id == persona.Id)) return;

        var viewModel = new AgregarExpedienteViewModel(persona);

        // Suscribirse al evento de guardado
        viewModel.GuardarExpediente += OnExpedienteIndirectoGuardado;

        // Abrir la ventana de edición de la prenda
        var dialog = new AgregarExpediente { DataContext = viewModel };

        // Configurar la acción de cierre para la ventana de edición
        if (dialog.DataContext is AgregarExpedienteViewModel vm)
        {
            vm.CloseAction = () => dialog.Close();
        }

        dialog.ShowDialog();
    }

    private void OnExpedienteIndirectoGuardado(object? sender, Expediente expediente)
    {
        if (sender is not AgregarExpedienteViewModel vm) return;

        ExpedientesIndirectos.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpedienteIndirecto(Expediente expediente)
    {
        ExpedientesIndirectos.Remove(expediente);
    }


    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        ReporteService.UbicacionHechos = Ubicacion;
        
        ModoTiempoLugarPost informacion = new()
        {
            ReporteId = 2,
            FechaDesaparicion = FechaDesaparicion,
            FechaPercato = FechaPercato,
            AclaracionHechos = AclaracionHechos,
            Ubicacion = Ubicacion,
            AmenazaCambioComportamiento = AmenazaCambioComportamiento,
            AmenazaDescripcion = AmenazaDescripcion,
            ContadorDesaparicion = ContadorDesaparicion,
            SituacionPreviaDescripcion = SituacionPreviaDescripcion,
            DatosPersonasRealacionadas = DatosPersonasRealacionadas,
            DescripcionHechosDesaparicion = DescripcionHechosDesaparicion,
            SintesisHechosDesaparicion = SintesisHechosDesaparicion,
            TipoHipotesisUno = TipoHipotesisUno.Id,
            TipoHipotesisDos = TipoHipotesisDos.Id,
            Sitio = Sitio.Id,
            AreaCodifica = AreaCodifica,
            DesaparecioAcompanado = DesaparecioAcompanado,
            NumeroPersonasMismoEvento = NumeroPersonasMismoEvento,
        };
        
        if (ReporteService.SendModoTiempoLugar(informacion)) FormularioNavigationService.Navigate(pageType);
    }
    
}