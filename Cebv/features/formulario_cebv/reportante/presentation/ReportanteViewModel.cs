using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos;
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos;
    [ObservableProperty] private ObservableCollection<Catalogo> _generos;
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones;
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas;
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades;
    [ObservableProperty] private ObservableCollection<Estado> _estados;

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
        Parentescos = await ReportanteNetwork.GetCatalogo("parentescos");
        Sexos = await ReportanteNetwork.GetCatalogo("sexos");
        Generos = await ReportanteNetwork.GetCatalogo("generos");
        Colectivos = await ReportanteNetwork.GetCatalogo("colectivos");
        Religiones = await ReportanteNetwork.GetCatalogo("religiones");
        Lenguas = await ReportanteNetwork.GetCatalogo("lenguas");
        Nacionalidades = await ReportanteNetwork.GetCatalogo("nacionalidades");
        Estados = await ReportanteNetwork.GetEstados();
        
        Reporte = _reporteService.GetReporte();
        if (Reporte.Reportantes?.Count == 0)
        {
            Reporte.Reportantes?.Add(new Reportante());
        }

        if (Reporte.Reportantes[0].Persona.Nacionalidades.Count == 0)
        {
            Reporte.Reportantes[0].Persona.Nacionalidades.Add(new Catalogo());
        }
        
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
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
};