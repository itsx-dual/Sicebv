using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.persona.presentation;
using Cebv.core.util;
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
using static Cebv.core.util.CollectionsHelper;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoViewModel : ObservableValidator
{
    private static readonly ISnackbarService SnackbarService = App.Current.Services.GetService<ISnackbarService>()!;

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();
    [ObservableProperty] private Reportante _reportante = new();
    [ObservableProperty] private Direccion? _direccion;

    public DesaparecidoViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        if (!Reporte.Reportantes.Any()) Reporte.Reportantes.Add(Reportante);

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
        Reportante = Reporte.Reportantes.FirstOrDefault()!;

        Direccion = Desaparecido.Persona.Direcciones.FirstOrDefault(); // Dirección del desaparecido
        OcupacionPrincipal = Desaparecido.Persona.Ocupaciones.FirstOrDefault(x => x.Prioridad == Principal);
        OcupacionSecundaria = Desaparecido.Persona.Ocupaciones.FirstOrDefault(x => x.Prioridad == Secundaria);

        EnsureObjectExists(ref _direccion, Desaparecido.Persona.Direcciones);

        EnsureObjectExists(
            ref _ocupacionPrincipal,
            Desaparecido.Persona.Ocupaciones,
            P.ParametrosPrincipal);

        EnsureObjectExists(
            ref _ocupacionSecundaria,
            Desaparecido.Persona.Ocupaciones,
            P.ParametrosSecundaria);

        Desaparecido.Persona.ContextoFamiliar ??= new();
        Desaparecido.Persona.Estudios ??= new();
        Desaparecido.Persona.ContextoEconomico ??= new();

        EsMismoDomicilioReportante =
            Direccion?.Equals(Reportante.Persona.Direcciones.FirstOrDefault())
            ?? false;

        TieneApodos = Desaparecido.Persona.Pseudonimos.Any();
        TieneTelefonosMoviles = Desaparecido.Persona.Telefonos.Any(x => (bool)x.EsMovil!);
        TieneTelefonosFijos = Desaparecido.Persona.Telefonos.Any(x => (bool)!x.EsMovil!);
        TieneCorreos = Desaparecido.Persona.Contactos.Any(x => x.Tipo == CorreoElectronico);
        TieneRedesSociales = Desaparecido.Persona.Contactos.Any(x => x.Tipo == RedSocial);

        DiferenciaFechas(Desaparecido.Persona.FechaNacimiento, DateTime.Now);
    }

    private async void InitAsync()
    {
        TiposOcupaciones = await CebvNetwork.GetRoute<Catalogo>("tipos-ocupaciones");
        Colectivos = await CebvNetwork.GetRoute<Catalogo>("colectivos");
        RazonesCurp = await CebvNetwork.GetRoute<Catalogo>("razones-curp");
        GruposVulnerables = await CebvNetwork.GetRoute<Catalogo>("grupos-vulnerables");
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
        TiposRedesSociales = await CebvNetwork.GetRoute<Catalogo>("tipos-redes-sociales");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");

        var desaparecido = _reporteService.GetReporte().Desaparecidos.FirstOrDefault();

        // Dirección
        var est =
            desaparecido?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio?.Estado;

        var mpio =
            desaparecido?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio;

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

        // Ocupaciones
        var tipoOcupPrincipal =
            desaparecido?.Persona.Ocupaciones.FirstOrDefault(x => x.Prioridad == Principal)?.Ocupacion?.TipoOcupacion;

        var tipoOcupSecundaria =
            desaparecido?.Persona.Ocupaciones.FirstOrDefault(x => x.Prioridad == Secundaria)?.Ocupacion?.TipoOcupacion;

        if (tipoOcupPrincipal is not null)
        {
            TipoOcupacionPrincipal = tipoOcupPrincipal;
            OcupacionesPrincipales = await CebvNetwork
                .GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", tipoOcupPrincipal.Id.ToString()!);
        }

        if (tipoOcupSecundaria != null)
        {
            TipoOcupacionSecundaria = tipoOcupSecundaria;
            OcupacionesSecundarias = await CebvNetwork
                .GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", tipoOcupSecundaria.Id.ToString()!);
        }
    }

    [ObservableProperty] private PersonaViewModel _persona = new();

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

    [ObservableProperty] private Pseudonimo? _pseudonimo;
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

    [ObservableProperty] private OcupacionPersona _p = new();
    [ObservableProperty] private OcupacionPersona? _ocupacionPrincipal;
    [ObservableProperty] private OcupacionPersona? _ocupacionSecundaria;

    // Información de nacimiento
    [ObservableProperty] private bool _fechaAproximada;

    async partial void OnTipoOcupacionPrincipalChanged(Catalogo? value)
    {
        if (value?.Id is null) return;
        OcupacionesPrincipales = await CebvNetwork
            .GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", value.Id.ToString()!);
    }

    async partial void OnTipoOcupacionSecundariaChanged(Catalogo? value)
    {
        if (value?.Id is null) return;
        OcupacionesSecundarias = await CebvNetwork
            .GetByFilter<Ocupacion>("ocupaciones", "tipo_ocupacion_id", value.Id.ToString()!);
    }


    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value is null) return;
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
            if (!value) return;

            var direccionReportante = Reporte.Reportantes.FirstOrDefault()?.Persona.Direcciones.FirstOrDefault();

            if (direccionReportante is not null)
            {
                var est =
                    direccionReportante.Asentamiento?.Municipio?.Estado;

                var mpio =
                    direccionReportante.Asentamiento?.Municipio;

                if (est is not null)
                {
                    EstadoSelected = est;
                    Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", est.Id);
                }

                if (mpio is not null)
                {
                    MunicipioSelected = mpio;
                    Asentamientos =
                        await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", mpio.Id);
                }

                Direccion = direccionReportante;
            }
            else
            {
                SnackbarService.Show(
                    "No se encontró una dirección para el reportante",
                    "No se realizó la acción",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));
            }
        }
        catch (Exception)
        {
            SnackbarService.Show(
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
        if (Pseudonimo?.Nombre is null) return;

        Desaparecido.Persona.Pseudonimos.Add(Pseudonimo);

        Pseudonimo = null;
    }

    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil is null) return;

        Desaparecido.Persona.Telefonos.Add(new Telefono
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
        if (NoTelefonoFijo is null) return;

        Desaparecido.Persona.Telefonos.Add(new Telefono
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
    private void OnAddCorreo()
    {
        if (UsuarioCorreo is null) return;

        Desaparecido.Persona.Contactos.Add(new Contacto
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
        if (TipoRedSocialSelected is null || UsuarioRedSocial is null) return;

        Desaparecido.Persona.Contactos.Add(new Contacto
        {
            Nombre = UsuarioRedSocial,
            Observaciones = ObservacionesRedSocial,
            TipoRedSocial = TipoRedSocialSelected,
            Tipo = RedSocial
        });

        UsuarioRedSocial = null;
        ObservacionesRedSocial = null;
        TipoRedSocialSelected = null;
    }

    [RelayCommand]
    private void OnRemoveApodo(Pseudonimo pseudonimo) => Desaparecido.Persona.Pseudonimos.Remove(pseudonimo);

    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono) => Desaparecido.Persona.Telefonos.Remove(telefono);

    [RelayCommand]
    private void OnEliminarContacto(Contacto contacto) => Desaparecido.Persona.Contactos.Remove(contacto);
    
    [RelayCommand]
    private async Task OnEditarTelefono(Telefono telefono)
    {
        var showEditList = new ShowDialogEditList();
        
        await showEditList.ShowContentDialogCommand.ExecuteAsync(telefono);
    }


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

    [RelayCommand]
    private void OnGuardarYContinuar(Type pageType)
    {
        Desaparecido.Persona.ValidateAll();

        if (Desaparecido.Persona.HasErrors)
        {
            ShowErrors();
            return;
        }

        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }

    // TODO: Mejorar logica creando un componente por default
    [RelayCommand]
    private void ShowErrors()
    {
        string message = string.Join(Environment.NewLine, Desaparecido.Persona.GetErrors().Select(e => e.ErrorMessage));

        SnackbarService.Show("Validation errors", message, ControlAppearance.Danger, null, TimeSpan.FromSeconds(10));
    }
}