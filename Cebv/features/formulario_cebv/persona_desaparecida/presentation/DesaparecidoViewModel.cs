using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoViewModel : ObservableObject
{ 
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido;
    
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
        CargarCatalogos();
    }

    private async void CargarCatalogos()
    {
        var desaparecido = _reporteService.GetReporte().Desaparecidos?.FirstOrDefault();
        var estadoId = desaparecido?.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado?.Id;
        var municipioId = desaparecido?.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Id;
        
        TiposOcupaciones = await DesaparecidoNetwork.GetCatalogo("tipos-ocupaciones");
        if (desaparecido?.OcupacionPrincipal != null)
        {
            TipoOcupacionPrincipalSelected = TiposOcupaciones.FirstOrDefault(x=>x.Id == desaparecido.OcupacionPrincipal.TipoOcupacion.Id);
            OcupacionesPrincipales = await DesaparecidoNetwork.OcupacionesDadoTipo(TipoOcupacionPrincipalSelected.Id);
        }
        
        if (desaparecido?.OcupacionSecundaria != null)
        {
            TipoOcupacionSecundariaSelected = TiposOcupaciones.FirstOrDefault(x=>x.Id == desaparecido.OcupacionSecundaria.TipoOcupacion.Id);
            OcupacionesSecundarias = await DesaparecidoNetwork.OcupacionesDadoTipo(TipoOcupacionSecundariaSelected.Id);
        }
        
        Parentescos = await DesaparecidoNetwork.GetCatalogo("parentescos");
        Sexos = await DesaparecidoNetwork.GetCatalogo("sexos");
        Generos = await DesaparecidoNetwork.GetCatalogo("generos");
        Colectivos = await DesaparecidoNetwork.GetCatalogo("colectivos");
        Religiones = await DesaparecidoNetwork.GetCatalogo("religiones");
        Lenguas = await DesaparecidoNetwork.GetCatalogo("lenguas");
        Nacionalidades = await DesaparecidoNetwork.GetCatalogo("nacionalidades");
        Escolaridades = await DesaparecidoNetwork.GetCatalogo("escolaridades");
        EstadosConyugales = await DesaparecidoNetwork.GetCatalogo("estados-conyugales");
        GruposVulnerables = await DesaparecidoNetwork.GetCatalogo("grupos-vulnerables");
        CompaniasTelefonicas = await DesaparecidoNetwork.GetCatalogo("companias-telefonicas");
        TiposRedesSociales = await DesaparecidoNetwork.GetCatalogo("tipos-redes-sociales");
        Estados = await DesaparecidoNetwork.GetEstados();
        if (estadoId != null) Municipios = await DesaparecidoNetwork.GetMunicipiosDeEstado(estadoId);
        if (municipioId != null) Asentamientos = await DesaparecidoNetwork.GetAsentamientosDeMunicipio(municipioId);
        
        Reporte = _reporteService.GetReporte();
        
        if (Reporte.Desaparecidos?.Count == 0)
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos?.Add(Desaparecido);
        }
        else
        {
            Desaparecido = Reporte.Desaparecidos?.FirstOrDefault()!;
        }
        
        if (Desaparecido.Persona?.Nacionalidades?.Count == 0)
        {
            Desaparecido.Persona.Nacionalidades.Add(new Catalogo());
        }
        
        if ((bool) !Desaparecido.Persona?.Direcciones?.Any()!)
        {
            Desaparecido.Persona?.Direcciones?.Add(new Direccion());
        }
        else
        {
            EsMismoDomicilioReportante = Desaparecido.Persona?.Direcciones?[0]
                .Equals(Reporte.Reportantes?[0].Persona.Direcciones?[0]) ?? false;
            
            EstadoSelected = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado!;
            MunicipioSelected = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio!;
        }

        TieneApodos = Desaparecido.Persona?.Apodos?.Any() ?? false;
        TieneTelefonosMoviles = Desaparecido.Persona?.Telefonos?.Any(x => (bool)x.EsMovil!) ?? false;
        TieneTelefonosFijos = Desaparecido.Persona?.Telefonos?.Any(x => (bool)!x.EsMovil!) ?? false;
        TieneCorreos = Desaparecido.Persona?.Contactos?.Any(x => x.Tipo == "Correo Electronico") ?? false;
        TieneRedesSociales = Desaparecido.Persona?.Contactos?.Any(x => x.Tipo == "Red Social") ?? false;
        
        DiferenciaFechas(Reporte.Desaparecidos?[0].Persona?.FechaNacimiento, DateTime.Now);
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
        Municipios = await DesaparecidoNetwork.GetMunicipiosDeEstado(value.Id ?? string.Empty);
    }
    
    async partial void OnMunicipioSelectedChanged(Municipio value)
    {
        if (value != null) Asentamientos = await DesaparecidoNetwork.GetAsentamientosDeMunicipio(value.Id);
    }

    async partial void OnEsMismoDomicilioReportanteChanged(bool value)
    {
        if (value)
        {
            Desaparecido.Persona.Direcciones[0] = Reporte.Reportantes[0].Persona.Direcciones[0];
            var estadoId = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado?.Id;
            var municipioId = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Id;
            
            if (estadoId != null) Municipios = await DesaparecidoNetwork.GetMunicipiosDeEstado(estadoId);
            if (municipioId != null) Asentamientos = await DesaparecidoNetwork.GetAsentamientosDeMunicipio(municipioId);
            Desaparecido.Persona.Direcciones[0] = Reporte.Reportantes[0].Persona.Direcciones[0];
        }
        else
        {
            Desaparecido.Persona.Direcciones[0] = new Direccion();
        }
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
        
        var contactos = Desaparecido.Persona?.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = UsuarioRedSocial,
            Observaciones = ObservacionesRedSocial,
            TipoRedSocial = TipoRedSocialSelected,
            Tipo = "Red Social"
        });
        
        UsuarioCorreo = string.Empty; 
        ObservacionesCorreo = string.Empty;
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