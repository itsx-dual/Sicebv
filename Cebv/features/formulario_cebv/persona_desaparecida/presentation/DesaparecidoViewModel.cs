using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoViewModel : ObservableObject
{ 
    private static ISnackbarService _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido;
    [ObservableProperty] private Direccion _direccionDesaparecido;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposRedesSociales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposOcupaciones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _razonesCurp = new();
    [ObservableProperty] private ObservableCollection<Ocupacion> _ocupacionesPrincipales = new();
    [ObservableProperty] private ObservableCollection<Ocupacion> _ocupacionesSecundarias = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();
    
    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Municipio _municipioSelected;
    [ObservableProperty] private Catalogo _companiaTelefonicaSelected;
    [ObservableProperty] private Catalogo _tipoRedSocialSelected;
    [ObservableProperty] private Catalogo _tipoOcupacionPrincipalSelected;
    [ObservableProperty] private Catalogo _tipoOcupacionSecundariaSelected;
    
    [ObservableProperty] private bool _esMismoDomicilioReportante;
    [ObservableProperty] private bool _tieneTelefonosMoviles;
    [ObservableProperty] private bool _tieneTelefonosFijos;
    [ObservableProperty] private bool _tieneCorreos;
    [ObservableProperty] private bool _tieneRedesSociales;
    [ObservableProperty] private bool _tieneApodos;
    
    [ObservableProperty] private string? _apodoNombre = string.Empty;
    [ObservableProperty] private string? _apodoApellidoPaterno = string.Empty;
    [ObservableProperty] private string? _apodoApellidoMaterno = string.Empty;
    [ObservableProperty] private string? _noTelefonoMovil = string.Empty;
    [ObservableProperty] private string? _observacionesMovil = string.Empty;
    [ObservableProperty] private string? _noTelefonoFijo = string.Empty;
    [ObservableProperty] private string? _observacionesFijo = string.Empty;
    [ObservableProperty] private string? _usuarioCorreo = string.Empty;
    [ObservableProperty] private string? _observacionesCorreo = string.Empty;
    [ObservableProperty] private string? _usuarioRedSocial = string.Empty;
    [ObservableProperty] private string? _observacionesRedSocial = string.Empty;
    
    [ObservableProperty] private int? _edadAnos;
    [ObservableProperty] private int? _edadMeses;
    [ObservableProperty] private int? _edadDias;
    
    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "TERMINADA",
        "EN CURSO",
        "NO ESPECIFICA"
    };

    
    public DesaparecidoViewModel()
    {
        InitAsync();
    }

    private async Task CargarCatalogos()
    {
        Parentescos = await CebvNetwork.GetCatalogo("parentescos");
        Sexos = await CebvNetwork.GetCatalogo("sexos");
        Generos = await CebvNetwork.GetCatalogo("generos");
        Colectivos = await CebvNetwork.GetCatalogo("colectivos");
        Religiones = await CebvNetwork.GetCatalogo("religiones");
        Lenguas = await CebvNetwork.GetCatalogo("lenguas");
        RazonesCurp = await CebvNetwork.GetCatalogo("razones-curp");
        Nacionalidades = await CebvNetwork.GetCatalogo("nacionalidades");
        Escolaridades = await CebvNetwork.GetCatalogo("escolaridades");
        EstadosConyugales = await CebvNetwork.GetCatalogo("estados-conyugales");
        GruposVulnerables = await CebvNetwork.GetCatalogo("grupos-vulnerables");
        CompaniasTelefonicas = await CebvNetwork.GetCatalogo("companias-telefonicas");
        TiposRedesSociales = await CebvNetwork.GetCatalogo("tipos-redes-sociales");
        TiposOcupaciones = await CebvNetwork.GetCatalogo("tipos-ocupaciones");
        Estados = await InegiNetwork.GetEstados();
    }
    
    private async Task CargarDireccion()
    {
        if (!Desaparecido.Persona.Direcciones.Any())
        {
            Desaparecido.Persona.Direcciones.Add(new Direccion());
        }
        DireccionDesaparecido = Desaparecido.Persona.Direcciones?.FirstOrDefault();
        
        var estadoId = DireccionDesaparecido?.Asentamiento?.Municipio?.Estado.Id;
        var municipioId = DireccionDesaparecido?.Asentamiento?.Municipio.Id;

        EstadoSelected = Estados.FirstOrDefault(x => x.Id == estadoId);
        
        Municipios = await InegiNetwork.GetMunicipiosDeEstado(estadoId);
        MunicipioSelected = Municipios.FirstOrDefault(x => x.Id == municipioId);
        
        Asentamientos = await InegiNetwork.GetAsentamientosDeMunicipio(municipioId);
    }

    private async void InitAsync()
    {
        var reporte = _reporteService.GetReporte();

        if (!reporte.Desaparecidos.Any())
        {
            reporte.Desaparecidos.Add(new Desaparecido());
        }
        Desaparecido = reporte.Desaparecidos.FirstOrDefault()!;

        await CargarCatalogos();
        await CargarDireccion();
        
        if (Desaparecido.OcupacionPrincipal is not null)
        {
            TipoOcupacionPrincipalSelected = TiposOcupaciones.FirstOrDefault(x=>x.Id == Desaparecido.OcupacionPrincipal.TipoOcupacion.Id);
            OcupacionesPrincipales = await DesaparecidoNetwork.OcupacionesDadoTipo(TipoOcupacionPrincipalSelected.Id);
        }
        
        if (Desaparecido.OcupacionSecundaria is not null)
        {
            TipoOcupacionSecundariaSelected = TiposOcupaciones.FirstOrDefault(x=>x.Id == Desaparecido.OcupacionSecundaria.TipoOcupacion.Id);
            OcupacionesSecundarias = await DesaparecidoNetwork.OcupacionesDadoTipo(TipoOcupacionSecundariaSelected.Id);
        }
        
        Desaparecido.Persona.Nacionalidades ??= [];
        if (!Desaparecido.Persona.Nacionalidades.Any())
        {
            Desaparecido.Persona.Nacionalidades.Add(new Catalogo());
        }

        Reporte = reporte;
        TieneApodos = Desaparecido.Persona.Apodos?.Any() ?? false;
        TieneTelefonosMoviles = Desaparecido.Persona.Telefonos?.Any(x => (bool)x.EsMovil!) ?? false;
        TieneTelefonosFijos = Desaparecido.Persona.Telefonos?.Any(x => (bool)!x.EsMovil!) ?? false;
        TieneCorreos = Desaparecido.Persona.Contactos?.Any(x => x.Tipo == "Correo Electronico") ?? false;
        TieneRedesSociales = Desaparecido.Persona.Contactos?.Any(x => x.Tipo == "Red Social") ?? false;
        
        DiferenciaFechas(Desaparecido.Persona.FechaNacimiento, DateTime.Now);
    }

    async partial void OnTipoOcupacionPrincipalSelectedChanged(Catalogo value)
    {
        OcupacionesPrincipales = await DesaparecidoNetwork.OcupacionesDadoTipo(value.Id);
    }
    
    async partial void OnTipoOcupacionSecundariaSelectedChanged(Catalogo value)
    {
        OcupacionesSecundarias = await DesaparecidoNetwork.OcupacionesDadoTipo(value.Id);
    }

    async partial void OnEstadoSelectedChanged(Estado value)
    {
        MunicipioSelected = null;
        Municipios = await InegiNetwork.GetMunicipiosDeEstado(value.Id ?? string.Empty);
    }
    
    async partial void OnMunicipioSelectedChanged(Municipio value)
    {
        if (value != null) Asentamientos = await InegiNetwork.GetAsentamientosDeMunicipio(value.Id);
    }

    async partial void OnEsMismoDomicilioReportanteChanged(bool value)
    {
        var reportante = Reporte.Reportantes.FirstOrDefault();
        
        if (value)
        {
            var direccionReportante = reportante.Persona.Direcciones.FirstOrDefault();
            
            if (reportante is null) return;
            if (direccionReportante is null)
            {
                _snackbarService.Show(
                    "El reportante no tiene una direccion",
                    $"No se realizó la acción",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5)
                );
                return;
            }

            if (!value) return;
            Desaparecido.Persona.Direcciones[0] = direccionReportante;
        }
        else
        {
            Desaparecido.Persona.Direcciones.Remove(reportante?.Persona.Direcciones?.FirstOrDefault());
        }
        await CargarDireccion();
    }

    [RelayCommand]
    private void OnAddApodo()
    {
        if (ApodoNombre.Length > 0 || ApodoApellidoPaterno.Length > 0 || ApodoApellidoMaterno.Length > 0)
        {
            Desaparecido.Persona?.Apodos?.Add(new Apodo
            {
                Nombre = ApodoNombre,
                ApellidoPaterno = ApodoApellidoPaterno, 
                ApellidoMaterno = ApodoApellidoMaterno
            });

            ApodoNombre = string.Empty;
            ApodoApellidoPaterno = string.Empty;
            ApodoApellidoMaterno = string.Empty;
        };
    }

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil.Length <= 0) return;
        
        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoMovil,
            Observaciones = ObservacionesMovil,
            EsMovil = true,
            Compania = CompaniaTelefonicaSelected
        });
        
        NoTelefonoMovil = string.Empty; 
        ObservacionesMovil = string.Empty;
        CompaniaTelefonicaSelected = null;
    }
    
    [RelayCommand]
    private void OnAddTelefonoFijo()
    {
        if (NoTelefonoFijo.Length <= 0) return;
        
        var telefonos = Desaparecido.Persona?.Telefonos;
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
    private void OnRemoveApodo(Apodo apodo)
    {
        Desaparecido.Persona?.Apodos?.Remove(apodo);
    }
    
    [RelayCommand]
    private void OnRemoveGrupoVulnerabilidad(Catalogo catalogo)
    {
        var gruposVulnerables = Desaparecido.Persona?.GruposVulnerables;
        gruposVulnerables?.Remove(catalogo);
    }

    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono)
    {
        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Remove(telefono);
    }
    
    [RelayCommand]
    private void OnEliminarContacto(Contacto contacto)
    {
        var contactos = Desaparecido.Persona?.Contactos;
        contactos?.Remove(contacto);
    }
    
    [RelayCommand]
    private void OnAddCorreo()
    {
        if (UsuarioCorreo.Length <= 0) return;
        
        var contactos = Desaparecido.Persona?.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = UsuarioCorreo,
            Observaciones = ObservacionesCorreo,
            Tipo = "Correo Electronico"
        });
        
        UsuarioCorreo = string.Empty; 
        ObservacionesCorreo = string.Empty;
    }

    [RelayCommand]
    private void OnAddRedSocial()
    {
        if (UsuarioRedSocial.Length <= 0) return;
        
        var contactos = Desaparecido.Persona.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = UsuarioRedSocial,
            Observaciones = ObservacionesRedSocial,
            TipoRedSocial = TipoRedSocialSelected,
            Tipo = "Red Social"
        });
        
        UsuarioRedSocial = string.Empty; 
        ObservacionesRedSocial = string.Empty;
        TipoRedSocialSelected = null;
    }

    // Información de nacimiento
    [ObservableProperty] private bool _fechaAproximada;

    /**
     * Datos sociemográficos de la persona desaparecida.
     */
    [ObservableProperty] private string _hablaEspanolOpcion = OpcionesCebv.No;

    [ObservableProperty] private bool? _hablaEspanol;

    [ObservableProperty] private string _sabeLeerOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeLeer;

    [ObservableProperty] private string _sabeEscribirOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeEscribir;

    private void DiferenciaFechas(DateTime? a, DateTime b)
    {
        if (a == null) return;
        
        EdadAnos = b.Year - a?.Year;
        EdadMeses = b.Month - a?.Month;
        EdadDias = b.Day - a?.Day;
        
        if (EdadDias < 0)
        {
            EdadMeses--;
            EdadDias += DateTime.DaysInMonth(b.Year, b.Month);
        }

        if (EdadMeses < 0)
        {
            EdadAnos--;
            EdadMeses += 12;
        }
    }

    [RelayCommand]
    private void OnGuardarYContinuar(Type pageType)
    {
        AddApodoCommand.Execute(null);
        AddTelefonoMovilCommand.Execute(null);
        AddTelefonoFijoCommand.Execute(null);
        AddCorreoCommand.Execute(null);
        AddRedSocialCommand.Execute(null);
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }

    partial void OnHablaEspanolOpcionChanged(string value) =>
        HablaEspanol = OpcionesCebv.MappingToBool(value);

    partial void OnSabeLeerOpcionChanged(string value) =>
        SabeLeer = OpcionesCebv.MappingToBool(value);

    partial void OnSabeEscribirOpcionChanged(string value) =>
        SabeEscribir = OpcionesCebv.MappingToBool(value);
}