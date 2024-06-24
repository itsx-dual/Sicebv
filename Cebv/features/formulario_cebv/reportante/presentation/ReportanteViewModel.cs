using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;
    [ObservableProperty] private Reporte _reporte;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

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

    // Datos de identificación de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos;

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

    /**
     * Peticiones a la Api
     */
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetParentescos();
        //TODO: Corregir colectivos
        //Colectivos = await ReportanteNetwork.GetColectivos();
        
        Reporte = _reporteService.GetReporteActual();
    }

    /**
     * Guardar Borrador
     */
    private void ValidarBorrador()
    {
        if (Reporte.Reportantes[0]?.Persona?.Nombre == String.Empty ||
            Reporte.Reportantes[0]?.Persona?.ApellidoPaterno == String.Empty ||
            Reporte.Reportantes[0]?.Persona?.ApellidoMaterno == String.Empty)
            PuedeGuardar = false;
        else PuedeGuardar = true;

        WeakReferenceMessenger.Default.Send(new GuardarBorradorMessage(PuedeGuardar));
    }

    [RelayCommand]
    public void GuardarBorrador()
    {
        ValidarBorrador();
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
        var reportante = new ReportantePostObject
        {
            //Persona = persona,
            //Parentesco = Parentesco.Id,
            DenunciaAnonima = EsAnonimo,
            PertenenciaColectivo = PertenenciaColectivo,
            //NombreColectivo = Colectivo.Nombre,
            InformacionRelevante = InformacionRelevante
        };
        
        _reporteService.SendReportante(reportante);
        _navigationService.Navigate(pageType);
    }
};