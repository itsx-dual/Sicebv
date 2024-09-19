using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.persona.presentation;
using static Cebv.core.util.enums.PrioridadOcupacion;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using static Cebv.core.util.enums.TipoContacto;
using Cebv.core.util.snackbar;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoViewModel : ObservableObject
{
    private static ISnackbarService _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;
    [ObservableProperty] private PersonaViewModel _persona = new();

    public DesaparecidoViewModel()
    {
        CargarCatalogos();
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposRedesSociales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _razonesCurp = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private Estado? _estadoSelected;
    [ObservableProperty] private Municipio? _municipioSelected;
    [ObservableProperty] private Catalogo? _companiaTelefonicaSelected;
    [ObservableProperty] private Catalogo? _tipoRedSocialSelected;

    [ObservableProperty] private bool _esMismoDomicilioReportante;
    [ObservableProperty] private bool _tieneTelefonosMoviles;
    [ObservableProperty] private bool _tieneTelefonosFijos;
    [ObservableProperty] private bool _tieneCorreos;
    [ObservableProperty] private bool _tieneRedesSociales;
    [ObservableProperty] private bool _tieneApodos;

    [ObservableProperty] private string? _apodoNombre;
    [ObservableProperty] private string? _apodoApellidoPaterno;
    [ObservableProperty] private string? _apodoApellidoMaterno;
    [ObservableProperty] private string? _noTelefonoMovil;
    [ObservableProperty] private string? _observacionesMovil;
    [ObservableProperty] private string? _noTelefonoFijo;
    [ObservableProperty] private string? _observacionesFijo;
    [ObservableProperty] private string? _usuarioCorreo;
    [ObservableProperty] private string? _observacionesCorreo;
    [ObservableProperty] private string? _usuarioRedSocial;
    [ObservableProperty] private string? _observacionesRedSocial;

    [ObservableProperty] private int? _edadAnos;
    [ObservableProperty] private int? _edadMeses;
    [ObservableProperty] private int? _edadDias;

    /**
     * Ocupaciones
     */
    [ObservableProperty] private ObservableCollection<Catalogo>? _tiposOcupaciones;

    [ObservableProperty] private Catalogo? _tipoOcupacionPrincipal;
    [ObservableProperty] private Catalogo? _tipoOcupacionSecundaria;

    [ObservableProperty] private ObservableCollection<Ocupacion>? _ocupacionesPrincipales;
    [ObservableProperty] private ObservableCollection<Ocupacion>? _ocupacionesSecundarias;

    [ObservableProperty] private OcupacionPersona? _ocupacionPrincipal;
    [ObservableProperty] private OcupacionPersona? _ocupacionSecundaria;

    private async void CargarCatalogos()
    {
        var desaparecido = _reporteService.GetReporte().Desaparecidos.FirstOrDefault();
        var estadoId = desaparecido?.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado?.Id;
        var municipioId = desaparecido?.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Id;

        var tipoOcupacionPrincipal = desaparecido?.Persona?.Ocupaciones.FirstOrDefault(x => x.Prioridad == Principal)
            ?.Ocupacion?.TipoOcupacion;
        var tipoOcupacionSecundaria = desaparecido?.Persona?.Ocupaciones.FirstOrDefault(x => x.Prioridad == Secundaria)
            ?.Ocupacion?.TipoOcupacion;

        TiposOcupaciones = await CebvNetwork.GetRoute<Catalogo>("tipos-ocupaciones");
        Colectivos = await CebvNetwork.GetRoute<Catalogo>("colectivos");
        RazonesCurp = await CebvNetwork.GetRoute<Catalogo>("razones-curp");
        GruposVulnerables = await CebvNetwork.GetRoute<Catalogo>("grupos-vulnerables");
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
        TiposRedesSociales = await CebvNetwork.GetRoute<Catalogo>("tipos-redes-sociales");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        if (estadoId != null) Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", estadoId);
        if (municipioId != null) Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", municipioId);
        if (tipoOcupacionPrincipal != null)
        {
            TipoOcupacionPrincipal = tipoOcupacionPrincipal;
            OcupacionesPrincipales = await CebvNetwork.GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id",
                tipoOcupacionPrincipal.Id.ToString()!);
        }

        if (tipoOcupacionSecundaria != null)
        {
            TipoOcupacionSecundaria = tipoOcupacionSecundaria;
            OcupacionesSecundarias = await CebvNetwork.GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id",
                tipoOcupacionSecundaria.Id.ToString()!);
        }

        Reporte = _reporteService.GetReporte();

        OcupacionPrincipal = Reporte.Desaparecidos[0].Persona!.Ocupaciones.FirstOrDefault(
            x => x.Prioridad == Principal
        ) ?? new OcupacionPersona
        {
            Prioridad = Principal
        };

        OcupacionSecundaria = Reporte.Desaparecidos[0].Persona!.Ocupaciones.FirstOrDefault(
            x => x.Prioridad == Secundaria
        ) ?? new OcupacionPersona
        {
            Prioridad = Secundaria
        };

        if (Reporte.Desaparecidos.Count == 0)
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }
        else
        {
            Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
        }

        if (Desaparecido.Persona?.Nacionalidades?.Count == 0)
        {
            Desaparecido.Persona.Nacionalidades.Add(new Catalogo());
        }

        if (Desaparecido.Persona?.Direcciones != null && (bool)Desaparecido.Persona?.Direcciones?.Any())
        {
            Desaparecido.Persona?.Direcciones?.Add(new Direccion());
        }
        else
        {
            EsMismoDomicilioReportante = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?
                .Equals(Reporte.Reportantes.FirstOrDefault()?.Persona?.Direcciones?.FirstOrDefault()) ?? false;

            EstadoSelected = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio?.Estado!;
            MunicipioSelected = Desaparecido.Persona?.Direcciones?.FirstOrDefault()?.Asentamiento?.Municipio!;
        }

        TieneApodos = Desaparecido.Persona?.Pseudonimos?.Any() ?? false;
        TieneTelefonosMoviles = Desaparecido.Persona?.Telefonos?.Any(x => (bool)x.EsMovil!) ?? false;
        TieneTelefonosFijos = Desaparecido.Persona?.Telefonos?.Any(x => (bool)!x.EsMovil!) ?? false;
        TieneCorreos = Desaparecido.Persona?.Contactos?.Any(x => x.Tipo == CorreoElectronico) ?? false;
        TieneRedesSociales = Desaparecido.Persona?.Contactos?.Any(x => x.Tipo == RedSoial) ?? false;

        DiferenciaFechas(Reporte.Desaparecidos[0].Persona?.FechaNacimiento, DateTime.Now);

        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.ContextoFamiliar ??= new();
        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.Estudios ??= new();
    }

    async partial void OnTipoOcupacionPrincipalChanged(Catalogo? value)
    {
        if (value?.Id is null or <= 0) return;
        OcupacionesPrincipales =
            await CebvNetwork.GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", value.Id?.ToString()!);
    }

    async partial void OnTipoOcupacionSecundariaChanged(Catalogo? value)
    {
        if (value?.Id is null or <= 0) return;
        OcupacionesSecundarias =
            await CebvNetwork.GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", value.Id?.ToString()!);
    }


    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value is null) return;
        MunicipioSelected = null;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value is null) return;
        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }

    async partial void OnEsMismoDomicilioReportanteChanged(bool value)
    {
        try
        {
            if (value)
            {
                var reporteReportante = Reporte.Reportantes.FirstOrDefault();
                var direccionReportante = reporteReportante?.Persona?.Direcciones?.FirstOrDefault();

                if (direccionReportante != null)
                {
                    if (Desaparecido.Persona == null) return;
                    if (Desaparecido.Persona.Direcciones == null) return;

                    Desaparecido.Persona.Direcciones[0] = direccionReportante;
                    var estadoId = direccionReportante.Asentamiento?.Municipio?.Estado?.Id;
                    var municipioId = direccionReportante.Asentamiento?.Municipio?.Id;

                    if (estadoId != null)
                    {
                        Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", estadoId);
                    }

                    if (municipioId != null)
                    {
                        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("municpios", "municipio_id", municipioId);
                    }

                    Desaparecido.Persona.Direcciones[0] = direccionReportante;
                }
                else
                {
                    _snackbarService.Show(
                        "No se encontró una dirección para el reportante",
                        "No se realizó la acción",
                        ControlAppearance.Caution,
                        new SymbolIcon(SymbolRegular.Warning20),
                        new TimeSpan(0, 0, 5));
                }
            }
            else
            {
                if (Desaparecido.Persona is null) return;
                if (Desaparecido.Persona.Direcciones != null) Desaparecido.Persona.Direcciones[0] = new Direccion();
            }
        }
        catch (Exception ex)
        {
            _snackbarService.Show(
                "No se encontró una dirección para el reportante",
                $"No se realizó la acción",
                ControlAppearance.Caution,
                new SymbolIcon(SymbolRegular.Warning20),
                new TimeSpan(0, 0, 5));
        }
    }

    [RelayCommand]
    private void OnAddApodo()
    {
        if (ApodoNombre?.Length <= 0 &&
            ApodoApellidoPaterno?.Length <= 0 &&
            ApodoApellidoMaterno?.Length <= 0) return;

        Desaparecido.Persona?.Pseudonimos?.Add(new Pseudonimo
        {
            Nombre = ApodoNombre,
            ApellidoPaterno = ApodoApellidoPaterno,
            ApellidoMaterno = ApodoApellidoMaterno
        });

        ApodoNombre = null;
        ApodoApellidoPaterno = null;
        ApodoApellidoMaterno = null;
    }

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil?.Length <= 0) return;

        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoMovil,
            Observaciones = ObservacionesMovil,
            EsMovil = true,
            Compania = CompaniaTelefonicaSelected
        });

        NoTelefonoMovil = null;
        ObservacionesMovil = null;
        CompaniaTelefonicaSelected = null;
    }

    [RelayCommand]
    private void OnAddTelefonoFijo()
    {
        if (NoTelefonoFijo?.Length <= 0) return;

        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoFijo,
            Observaciones = ObservacionesFijo,
            EsMovil = false,
            Compania = null
        });

        NoTelefonoFijo = null;
        ObservacionesFijo = null;
    }

    [RelayCommand]
    private void OnRemoveApodo(Pseudonimo pseudonimo)
    {
        Desaparecido.Persona?.Pseudonimos?.Remove(pseudonimo);
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
        if (UsuarioCorreo?.Length <= 0) return;

        var contactos = Desaparecido.Persona?.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = UsuarioCorreo,
            Observaciones = ObservacionesCorreo,
            Tipo = CorreoElectronico
        });

        UsuarioCorreo = null;
        ObservacionesCorreo = null;
    }

    [RelayCommand]
    private void OnAddRedSocial()
    {
        if (TipoRedSocialSelected is null || UsuarioRedSocial?.Length <= 0) return;

        var contactos = Desaparecido.Persona?.Contactos;
        contactos?.Add(new Contacto
        {
            Nombre = UsuarioRedSocial,
            Observaciones = ObservacionesRedSocial,
            TipoRedSocial = TipoRedSocialSelected,
            Tipo = RedSoial
        });

        UsuarioRedSocial = null;
        ObservacionesRedSocial = null;
        TipoRedSocialSelected = null;
    }

    // Información de nacimiento
    [ObservableProperty] private bool _fechaAproximada;

    /**
     * Datos sociemográficos de la persona desaparecida.
     */
    [ObservableProperty] private string _hablaEspanol = No;

    [ObservableProperty] private string _sabeLeerEscribir = No;

    private void DiferenciaFechas(DateTime? a, DateTime b)
    {
        if (a == null) return;

        EdadAnos = b.Year - a.Value.Year;
        EdadMeses = b.Month - a.Value.Month;
        EdadDias = b.Day - a.Value.Day;

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

    private void GuardarOcupaciones()
    {
        var ocupacionPrincipalIndex =
            Reporte.Desaparecidos[0].Persona!.Ocupaciones.IndexOf(OcupacionPrincipal!);
        
        var ocupacionSecundariaIndex =
            Reporte.Desaparecidos[0].Persona!.Ocupaciones.IndexOf(OcupacionSecundaria!);
        
        if (ocupacionPrincipalIndex == -1) Reporte.Desaparecidos[0].Persona!.Ocupaciones.Add(OcupacionPrincipal!);
        else Reporte.Desaparecidos[0].Persona!.Ocupaciones[ocupacionPrincipalIndex] = OcupacionPrincipal!;
        
        if (ocupacionSecundariaIndex == -1) Reporte.Desaparecidos[0].Persona!.Ocupaciones.Add(OcupacionSecundaria!);
        else Reporte.Desaparecidos[0].Persona!.Ocupaciones[ocupacionSecundariaIndex] = OcupacionSecundaria!;
    }

    [RelayCommand]
    private void OnGuardarYContinuar(Type pageType)
    {
        GuardarOcupaciones();
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}