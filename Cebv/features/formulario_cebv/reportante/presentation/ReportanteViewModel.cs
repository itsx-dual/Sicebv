using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.reportante.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    [ObservableProperty] private Direccion _direccionReportante;
    
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerablesFiltrados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();
    
    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Municipio _municipioSelected;
    [ObservableProperty] private Catalogo _grupoVulnerableSelected;

    [ObservableProperty] private string _noTelefonoMovil = string.Empty;
    [ObservableProperty] private string _observacionesMovil = string.Empty;
    
    [ObservableProperty] private string _noTelefonoFijo = string.Empty;
    [ObservableProperty] private string _observacionesFijo = string.Empty;
    
    [ObservableProperty] private string _nombreContacto = string.Empty;
    [ObservableProperty] private string _observacionesContacto = string.Empty;
    
    [ObservableProperty] private int? _edadAproxmida;
    
    [ObservableProperty] private bool _tieneTelefonosMoviles;
    [ObservableProperty] private bool _tieneTelefonosFijos;
    [ObservableProperty] private bool _tieneCorreos;
    [ObservableProperty] private bool _tienePertenenciasGrupales;
    [ObservableProperty] private bool? _participoBusqueda;
    [ObservableProperty] private bool? _victimaExtorsion;
    [ObservableProperty] private bool? _recibioAmenaza;

    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "TERMINADA",
        "EN CURSO",
        "NO ESPECIFICA"
    };

    public ReportanteViewModel()
    {
        InitAsync();
    }

    private async void InitAsync()
    {
        var reporte = _reporteService.GetReporte();
        if (!reporte.Reportantes.Any())
        {
            Reportante = new Reportante();
        }
        Reportante = reporte.Reportantes.FirstOrDefault();
        
        await CargarCatalogos();
        await CargarDireccion();
        
        Reporte = reporte;

        if (!Reportante.Persona.Nacionalidades.Any())
        {
            Reportante.Persona.Nacionalidades.Add(new Catalogo());
        }

        ParticipoBusqueda = Reportante.ParticipacionBusquedas is not null
            ? Reportante.ParticipacionBusquedas != string.Empty
            : null; 
        
        VictimaExtorsion = Reportante.DescripcionExtorsion is not null
            ? Reportante.DescripcionExtorsion != string.Empty
            : null; 
        
        RecibioAmenaza = Reportante.DescripcionDondeProviene is not null
            ? Reportante.DescripcionDondeProviene != string.Empty
            : null; 
        
        EdadAproxmida = CalculateAge(reporte?.Reportantes?.FirstOrDefault()?.Persona.FechaNacimiento);
        TieneTelefonosMoviles = Reportante.Persona.Telefonos.Any(x => (bool)x.EsMovil!);
        TieneTelefonosFijos = Reportante.Persona.Telefonos.Any(x => (bool)!x.EsMovil!);
        TieneCorreos = Reportante.Persona.Contactos.Any(x => x.Tipo == "Correo Electronico");
        TienePertenenciasGrupales = Reportante.Persona.GruposVulnerables.Any();
        GruposVulnerablesFiltrados = new ObservableCollection<Catalogo>(GruposVulnerables.Except(Reporte.Reportantes.FirstOrDefault()?.Persona.GruposVulnerables));
    }

    private async Task CargarDireccion()
    {
        if (!Reportante.Persona.Direcciones.Any())
        {
            Reportante.Persona.Direcciones.Add(new Direccion());
        }
        DireccionReportante = Reportante.Persona.Direcciones?.FirstOrDefault();
        
        var estadoId = DireccionReportante?.Asentamiento?.Municipio?.Estado.Id;
        var municipioId = DireccionReportante?.Asentamiento?.Municipio.Id;

        EstadoSelected = Estados.FirstOrDefault(x => x.Id == estadoId);
        
        Municipios = await ReportanteNetwork.GetMunicipiosDeEstado(estadoId) ?? [];
        MunicipioSelected = Municipios.FirstOrDefault(x => x.Id == municipioId);
        
        Asentamientos = await ReportanteNetwork.GetAsentamientosDeMunicipio(municipioId) ?? [];
    }
    
    private async Task CargarCatalogos()
    {
        Parentescos = await CebvNetwork.GetCatalogo("parentescos");
        Sexos = await CebvNetwork.GetCatalogo("sexos");
        Generos = await CebvNetwork.GetCatalogo("generos");
        Colectivos = await CebvNetwork.GetCatalogo("colectivos");
        Religiones = await CebvNetwork.GetCatalogo("religiones");
        Lenguas = await CebvNetwork.GetCatalogo("lenguas");
        Nacionalidades = await CebvNetwork.GetCatalogo("nacionalidades");
        Escolaridades = await CebvNetwork.GetCatalogo("escolaridades");
        EstadosConyugales = await CebvNetwork.GetCatalogo("estados-conyugales");
        GruposVulnerables = await CebvNetwork.GetCatalogo("grupos-vulnerables");
        Estados = await ReportanteNetwork.GetEstados();
    }
    
    public static int? CalculateAge(DateTime? birthDate)
    {
        if (!birthDate.HasValue) // Check if birthDate is null
            return null; 

        int years = DateTime.Now.Year - birthDate.Value.Year;
        if (birthDate.Value.AddYears(years) > DateTime.Now)
            years--;
        return years;
    }

    partial void OnEdadAproxmidaChanged(int? value)
    {
        if (Reporte.Reportantes != null) Reportante.EdadEstimada = value;
    }

    partial void OnParticipoBusquedaChanged(bool? value)
    {
        switch (value)
        {
            case false:
                Reportante.ParticipacionBusquedas = "";
                break;
            case null:
                Reportante.ParticipacionBusquedas = null;
                break;
        };
    }
    
    partial void OnVictimaExtorsionChanged(bool? value)
    {
        switch (value)
        {
            case false:
                Reportante.DescripcionExtorsion = "";
                break;
            case null:
                Reportante.DescripcionExtorsion = null;
                break;
        };
    }
    
    partial void OnRecibioAmenazaChanged(bool? value)
    {
        switch (value)
        {
            case false:
                Reportante.DescripcionDondeProviene = "";
                break;
            case null:
                Reportante.DescripcionDondeProviene = null;
                break;
        };
    }

    async partial void OnEstadoSelectedChanged(Estado value)
    {
        Municipios = null;
        if (value is not null) Municipios = await ReportanteNetwork.GetMunicipiosDeEstado(value.Id);
    }
    
    async partial void OnMunicipioSelectedChanged(Municipio value)
    {
        Asentamientos = null;
        if (value != null) Asentamientos = await ReportanteNetwork.GetAsentamientosDeMunicipio(value.Id);
    }
    
    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil.Length <= 0) return;
        
        var telefonos = Reporte.Reportantes?[0].Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoMovil,
            Observaciones = ObservacionesMovil,
            EsMovil = true,
            Compania = null
        });

        TieneTelefonosMoviles = Reportante.Persona.Telefonos?.Any(x => x.EsMovil ?? false) ?? false;
        NoTelefonoMovil = string.Empty; 
        ObservacionesMovil = string.Empty;
    }
    
    [RelayCommand]
    private void OnAddTelefonoFijo()
    {
        if (NoTelefonoFijo.Length <= 0) return;
        
        var telefonos = Reportante.Persona.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoFijo,
            Observaciones = ObservacionesFijo,
            EsMovil = false,
            Compania = null
        });
        
        TieneTelefonosFijos = Reportante.Persona.Telefonos?.Any(x => !x.EsMovil ?? false) ?? false;
        NoTelefonoFijo = string.Empty; 
        ObservacionesFijo = string.Empty;
    }
    
    [RelayCommand]
    private void OnAddContacto()
    {
        if (NombreContacto.Length <= 0) return;
        
        var contactos = Reportante.Persona.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = NombreContacto,
            Observaciones = ObservacionesContacto,
            Tipo = "Correo Electronico"
        });

        TieneCorreos = Reportante.Persona.Contactos?.Any() ?? false;
        NombreContacto = string.Empty; 
        ObservacionesContacto = string.Empty;
    }
    
    [RelayCommand]
    private void OnAddGrupoVulnerabilidad()
    {
        var gruposVulnerables = Reporte.Reportantes.FirstOrDefault()?.Persona.GruposVulnerables;
        if (GrupoVulnerableSelected != null) gruposVulnerables?.Add(GrupoVulnerableSelected);
        GruposVulnerablesFiltrados = new ObservableCollection<Catalogo>(GruposVulnerables.Except(gruposVulnerables));
        GrupoVulnerableSelected = null;
    }
    
    [RelayCommand]
    private void OnRemoveGrupoVulnerabilidad(Catalogo catalogo)
    {
        var gruposVulnerables = Reporte.Reportantes?[0].Persona?.GruposVulnerables;
        gruposVulnerables?.Remove(catalogo);
        GruposVulnerablesFiltrados = new ObservableCollection<Catalogo>(GruposVulnerables.Except(gruposVulnerables));
    }

    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono)
    {
        var telefonos = Reporte.Reportantes?[0].Persona?.Telefonos;
        telefonos?.Remove(telefono);
    }
    
    [RelayCommand]
    private void OnEliminarContacto(Contacto contacto)
    {
        var contactos = Reporte.Reportantes?[0].Persona?.Contactos;
        contactos?.Remove(contacto);
    }

    [RelayCommand]
    public void OnGuardarYSiguiente(Type pageType)
    {
        // Si hay algo en los campos de telefonos, se agregan antes de sincronizar,
        // este es el comportamiento que la CEBV espera.
        AddTelefonoMovilCommand.Execute(null);
        AddTelefonoFijoCommand.Execute(null);
        AddContactoCommand.Execute(null);
        AddGrupoVulnerabilidadCommand.Execute(null);
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
};