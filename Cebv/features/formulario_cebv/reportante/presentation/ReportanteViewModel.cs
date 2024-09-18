using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;
using Persona = Cebv.core.util.reporte.viewmodels.Persona;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Opciones;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    [ObservableProperty] private PersonaViewModel _persona = new();

    private IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Municipio? _municipioSelected;
    [ObservableProperty] private Catalogo? _grupoVulnerableSelected;

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

    public ReportanteViewModel()
    {
        CargarCatalogos();
    }

    private async void CargarCatalogos()
    {
        var reporte = _reporteService.GetReporte();

        // Verifica si reporte y Reportantes están inicializados
        if (reporte.Id < 1 || reporte.Reportantes.Count < 1)
        {
            Reportante = new Reportante();
            reporte.Reportantes.Add(Reportante);
        }
        else
        {
            Reportante = reporte.Reportantes[0];
        }

        Reportante.Persona ??= new Persona();

        var estadoId =
            reporte.Reportantes[0].Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado?.Id;

        string? municipioId =
            reporte.Reportantes[0].Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Id;

        // Cargar los catálogos de forma asincrónica usando el método LoadCatalog
        Colectivos = await CebvNetwork.GetRoute<Catalogo>("colectivos");
        GruposVulnerables = await CebvNetwork.GetRoute<Catalogo>("grupos-vulnerables");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        if (estadoId != null) Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", estadoId);
        if (municipioId != null) Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", municipioId);

        Reporte = reporte;
        if (Reporte.Reportantes.Count < 1)
        {
            Reportante = new Reportante();
            Reporte.Reportantes.Add(Reportante);
        }
        else
        {
            Reportante = Reporte.Reportantes[0];
        }

        Reporte.Reportantes[0].Persona ??= new Persona();

        if (Reportante.Persona?.Nacionalidades != null && Reportante.Persona.Nacionalidades.Count == 0)
        {
            Reportante.Persona.Nacionalidades.Add(new Catalogo());
        }

        if (Reportante.Persona?.Direcciones != null && Reportante.Persona.Direcciones.Any())
        {
            Reportante.Persona.Direcciones.Add(new Direccion());
        }
        else
        {
            EstadoSelected = Reportante.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado!;
            MunicipioSelected = Reportante.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio!;
        }

        if (reporte.Reportantes.FirstOrDefault()?.ParticipacionBusquedas == null)
        {
            ParticipoBusqueda = null;
        }
        else ParticipoBusqueda = reporte.Reportantes.FirstOrDefault()?.ParticipacionBusquedas != String.Empty;

        if (reporte.Reportantes.FirstOrDefault()?.DescripcionExtorsion == null)
        {
            VictimaExtorsion = null;
        }
        else VictimaExtorsion = reporte.Reportantes.FirstOrDefault()?.DescripcionExtorsion != String.Empty;

        if (reporte.Reportantes.FirstOrDefault()?.DescripcionDondeProviene == null)
        {
            RecibioAmenaza = null;
        }
        else RecibioAmenaza = reporte.Reportantes.FirstOrDefault()?.DescripcionExtorsion != String.Empty;

        EdadAproxmida = CalculateAge(reporte.Reportantes.FirstOrDefault()?.Persona?.FechaNacimiento);

        if (Reportante.Persona?.Telefonos is { Count: > 0 })
        {
            TieneTelefonosMoviles = Reportante.Persona.Telefonos.Any(x => (bool)x.EsMovil!);
            TieneTelefonosFijos = Reportante.Persona.Telefonos.Any(x => (bool)!x.EsMovil!);
        }

        if (Reportante.Persona?.Contactos is { Count: > 0 })
            TieneCorreos = Reportante.Persona.Contactos.Any(x => x.Tipo == "Correo Electronico");

        if (Reportante.Persona?.GruposVulnerables is { Count: > 0 })
            TienePertenenciasGrupales = (bool)Reportante.Persona.GruposVulnerables?.Any();

        Reportante.Persona!.Estudios ??= new();
        Reportante.Persona!.ContextoFamiliar ??= new();
    }

    //private async Task<ObservableCollection<Catalogo>> LoadCatalog(string catalogName)
    //{
    //    var catalog = await ReportanteNetwork.GetCatalogo(catalogName);
    //    return catalog;
    //}

    private static int? CalculateAge(DateTime? birthDate)
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
        if (Reporte.Reportantes.Count > 1) Reportante.EdadEstimada = value;
    }

    partial void OnParticipoBusquedaChanged(bool? value)
    {
        Reportante.ParticipacionBusquedas = value switch
        {
            false => "",
            null => null,
            _ => Reportante.ParticipacionBusquedas
        };
    }

    partial void OnVictimaExtorsionChanged(bool? value)
    {
        Reportante.DescripcionExtorsion = value switch
        {
            false => "",
            null => null,
            _ => Reportante.DescripcionExtorsion
        };
    }

    partial void OnRecibioAmenazaChanged(bool? value)
    {
        Reportante.DescripcionDondeProviene = value switch
        {
            false => "",
            null => null,
            _ => Reportante.DescripcionDondeProviene
        };
    }

    async partial void OnEstadoSelectedChanged(Estado value)
    {
        MunicipioSelected = null;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value != null) Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil.Length <= 0) return;

        var telefonos = Reporte.Reportantes[0].Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoMovil,
            Observaciones = ObservacionesMovil,
            EsMovil = true,
            Compania = null
        });

        NoTelefonoMovil = string.Empty;
        ObservacionesMovil = string.Empty;
    }

    [RelayCommand]
    private void OnAddTelefonoFijo()
    {
        if (NoTelefonoFijo.Length <= 0) return;

        var telefonos = Reporte.Reportantes[0].Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoFijo,
            Observaciones = ObservacionesFijo,
            EsMovil = false,
            Compania = null
        });

        NoTelefonoFijo = string.Empty;
        ObservacionesFijo = string.Empty;
    }

    [RelayCommand]
    private void OnAddContacto()
    {
        if (NombreContacto.Length <= 0) return;

        var contactos = Reporte.Reportantes[0].Persona?.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = NombreContacto,
            Observaciones = ObservacionesContacto,
            Tipo = "Correo Electronico"
        });

        NombreContacto = string.Empty;
        ObservacionesContacto = string.Empty;
    }

    [RelayCommand]
    private void OnAddGrupoVulnerabilidad()
    {
        var gruposVulnerables = Reporte.Reportantes[0].Persona?.GruposVulnerables;
        if (GrupoVulnerableSelected != null) gruposVulnerables?.Add(GrupoVulnerableSelected);
        GrupoVulnerableSelected = null;
    }

    [RelayCommand]
    private void OnRemoveGrupoVulnerabilidad(Catalogo catalogo)
    {
        var gruposVulnerables = Reporte.Reportantes[0].Persona?.GruposVulnerables;
        gruposVulnerables?.Remove(catalogo);
    }

    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono)
    {
        var telefonos = Reporte.Reportantes[0].Persona?.Telefonos;
        telefonos?.Remove(telefono);
    }

    [RelayCommand]
    private void OnEliminarContacto(Contacto contacto)
    {
        var contactos = Reporte.Reportantes[0].Persona?.Contactos;
        contactos?.Remove(contacto);
    }

    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
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