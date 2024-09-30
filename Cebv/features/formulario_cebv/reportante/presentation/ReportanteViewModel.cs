using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.persona.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.data.OpcionesCebv;
using static Cebv.core.util.CollectionsHelper;
using static Cebv.core.util.enums.TipoContacto;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;
    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = new();
    [ObservableProperty] private PersonaViewModel _persona = new();
    [ObservableProperty] private Direccion? _direccion;

    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private Estado? _estadoSelected;
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

    public ReportanteViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();
        if (!Reporte.Reportantes.Any()) Reporte.Reportantes.Add(Reportante);
        Reportante = Reporte.Reportantes.FirstOrDefault()!;

        Direccion = Reportante.Persona.Direcciones.FirstOrDefault();
        EnsureObjectExists(ref _direccion, Reportante.Persona.Direcciones);

        EdadAproxmida = CalculateAge(Reportante.Persona.FechaNacimiento);

        TieneTelefonosMoviles = Reportante.Persona.Telefonos.Any(x => (bool)x.EsMovil!);
        TieneTelefonosFijos = Reportante.Persona.Telefonos.Any(x => (bool)!x.EsMovil!);
        TieneCorreos = Reportante.Persona.Contactos.Any(x => x.Tipo == CorreoElectronico);
        TienePertenenciasGrupales = Reportante.Persona.GruposVulnerables.Any();

        Reportante.Persona.Estudios ??= new();
        Reportante.Persona.ContextoFamiliar ??= new();
    }

    private async void InitAsync()
    {
        Colectivos = await CebvNetwork.GetRoute<Catalogo>("colectivos");
        GruposVulnerables = await CebvNetwork.GetRoute<Catalogo>("grupos-vulnerables");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");

        var reportante = _reporteService.GetReporte().Reportantes.FirstOrDefault();

        var est =
            reportante?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio?.Estado;

        var mpio =
            reportante?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio;

        if (est is not null)
        {
            EstadoSelected = est;
            Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", est.Id);
        }

        if (mpio is not null)
        {
            MunicipioSelected = mpio;
            Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", mpio.Id);
        }
    }

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
        if (Reporte.Reportantes.Count > 1) Reportante.EdadEstimadaAnhos = value;
    }

    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value is null) return;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value is null) return;
        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil.Length <= 0) return;

        var telefonos = Reporte.Reportantes[0].Persona.Telefonos;
        telefonos.Add(new Telefono
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

        var telefonos = Reporte.Reportantes[0].Persona.Telefonos;
        telefonos.Add(new Telefono
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

        var contactos = Reporte.Reportantes[0].Persona.Contactos;
        contactos.Add(new Contacto
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
        var gruposVulnerables = Reporte.Reportantes[0].Persona.GruposVulnerables;
        if (GrupoVulnerableSelected != null) gruposVulnerables.Add(GrupoVulnerableSelected);
        GrupoVulnerableSelected = null;
    }

    [RelayCommand]
    private void OnRemoveGrupoVulnerabilidad(Catalogo catalogo)
    {
        var gruposVulnerables = Reporte.Reportantes[0].Persona.GruposVulnerables;
        gruposVulnerables.Remove(catalogo);
    }

    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono)
    {
        var telefonos = Reporte.Reportantes[0].Persona.Telefonos;
        telefonos.Remove(telefono);
    }

    [RelayCommand]
    private void OnEliminarContacto(Contacto contacto)
    {
        var contactos = Reporte.Reportantes[0].Persona.Contactos;
        contactos.Remove(contacto);
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