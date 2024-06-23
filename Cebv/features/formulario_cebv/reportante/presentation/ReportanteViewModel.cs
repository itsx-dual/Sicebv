using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>();

    /**
    * Constructor
    */
    public ReportanteViewModel()
    {
        CargarCatalogos();
    }
    
    /**
     * Datos de control
     */
    [ObservableProperty] private bool _esAnonimo;
    [ObservableProperty] private bool _puedeGuardar;
    [ObservableProperty] private PersonaViewModel _reportante;


    // Datos de identificación de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();

    // Datos de contacto
    [ObservableProperty] private ContactoViewModel _contacto = new();

    // Domicilio
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    // Información relevante
    [ObservableProperty] private string _informacionRelevante = String.Empty;

    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "Terminada", "En curso", "No especifica"
    };

    [ObservableProperty] private string _estatusEscolaridad = "No especifica";

    [ObservableProperty] private string _pertenenciaColectivoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaColectivo = false;

    // Amezanas
    [ObservableProperty] private string _victimaExtorsionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _victimaExtorsion;

    [ObservableProperty] private string _recibioAmenazaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _recibioAmenaza;

    // Busquedas pasadas
    [ObservableProperty] private string _participoBusquedaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _participoBusqueda;

    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private Catalogo _colectivo = new();

    /**
     * Peticiones a la Api
     */
    private async void CargarCatalogos()
    {
        var reportante = _reporteService.GetReporteActual().Reportantes?.FirstOrDefault();
        var persona = reportante?.Persona;
        
        Parentescos = await ReportanteNetwork.GetParentescos();
        Colectivos = await ReportanteNetwork.GetColectivos();
        Reportante = await PersonaViewModel.CreateAsync(persona);
        
        if (_reporteService.HayReporte())
        {
            if (reportante != null)
            {
                Parentesco = Parentescos.FirstOrDefault(x => x.Id == reportante.ParentescoId)!;
                EsAnonimo = reportante.DenunciaAnonima ?? false;
                PertenenciaColectivo = reportante.PertenenciaColectivo;
                Colectivo.Nombre = reportante.NombreColectivo;
                InformacionRelevante = reportante.InformacionRelevante;
            }
        }
    }

    /**
     * Guardar Borrador
     */
    private void ValidarBorrador()
    {
        if (Reportante.Nombre == String.Empty ||
            Reportante.ApellidoPaterno == String.Empty ||
            Reportante.ApellidoMaterno == String.Empty)
            PuedeGuardar = false;
        else PuedeGuardar = true;

        WeakReferenceMessenger.Default.Send(new GuardarBorradorMessage(PuedeGuardar));
    }

    [RelayCommand]
    public void GuardarBorrador()
    {
        ValidarBorrador();
        Console.WriteLine(Reportante.Nombre);
    }

    partial void OnPertenenciaColectivoOpcionChanged(string value) =>
        PertenenciaColectivo = OpcionesCebv.MappingToBool(value);

    partial void OnVictimaExtorsionOpcionChanged(string value) =>
        VictimaExtorsion = OpcionesCebv.MappingToBool(value);

    partial void OnRecibioAmenazaOpcionChanged(string value) =>
        RecibioAmenaza = OpcionesCebv.MappingToBool(value);

    partial void OnParticipoBusquedaOpcionChanged(string value) =>
        ParticipoBusqueda = OpcionesCebv.MappingToBool(value);

    [RelayCommand]
    public void OnGuardarYSiguiente(Type pageType)
    {

        var persona = new PersonaPostObject
        {
            LugarNacimientoId = Ubicacion.Estado.Id,
            Nombre = Reportante.Nombre,
            ApellidoPaterno = Reportante.ApellidoPaterno,
            ApellidoMaterno = Reportante.ApellidoMaterno,
            PseudonimoNombre = Reportante.PseudonimoNombre,
            PseudonimoApellidoPaterno = Reportante.PseudonimoApellidoPaterno,
            PseudonimoApellidoMaterno = Reportante.PseudonimoApellidoMaterno,
            FechaNacimiento = Reportante.FechaNacimiento,
            Curp = Reportante.Curp,
            ObservacionesCurp = Reportante.ObservacionesCurp,
            Rfc = Reportante.Rfc,
            Ocupacion = Reportante.OcupacionUno.Nombre,
            Sexo = Reportante.Sexo.Id,
            Genero = Reportante.Genero.Id,
        };

        var reportante = new ReportantePostObject
        {
            Persona = persona,
            Parentesco = Parentesco.Id,
            DenunciaAnonima = EsAnonimo,
            PertenenciaColectivo = PertenenciaColectivo,
            NombreColectivo = Colectivo.Nombre,
            InformacionRelevante = InformacionRelevante
        };
        
        _reporteService.SendReportante(reportante);
        _navigationService.Navigate(pageType);
    }
};