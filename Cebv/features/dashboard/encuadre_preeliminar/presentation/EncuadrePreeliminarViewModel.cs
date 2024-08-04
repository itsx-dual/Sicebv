using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.presentation;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.formulario_cebv.prendas.domain;
using Cebv.features.formulario_cebv.senas_particulares.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableObject
{
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private static IDashboardNavigationService _navigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;

    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    [ObservableProperty] private Desaparecido _desaparecido;

    // Catalogos y valores predefinidos
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _razonesCurp = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _zonasEstados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos;
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello;
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello;

    [ObservableProperty] private ObservableCollection<Catalogo> _vistas;
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo;
    [ObservableProperty] private ObservableCollection<Catalogo> _colores;
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia;
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias;

    // Valores seleccionados
    [ObservableProperty] private Catalogo _tipoMedioSelected;
    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Municipio _municipioSelected;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaReportanteSelected;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    [ObservableProperty] private Catalogo _vistaSelected;
    [ObservableProperty] private Catalogo _tipoSelected;
    [ObservableProperty] private CatalogoColor _regionCuerpoSelected;
    [ObservableProperty] private CatalogoColor _ladoSelected;
    [ObservableProperty] private string _colorRegionCuerpo;
    [ObservableProperty] private string _colorLado;
    [ObservableProperty] private int _cantidad = 1;
    [ObservableProperty] private string _descripcion;

    [ObservableProperty] private Catalogo _grupoPerteneciaSelected;
    [ObservableProperty] private Pertenencia _perteneciaSelected;
    [ObservableProperty] private Catalogo _colorSelected;
    [ObservableProperty] private string _currentMarca;
    [ObservableProperty] private string _currentPrendaDescripcion;

    [ObservableProperty] private DateTime _fechaNacimientoDesaparecido = DateTime.Today;
    [ObservableProperty] private DateTime _fechaDesaparicion = DateTime.Today;
    [ObservableProperty] private TimeSpan _horaDesaparicion;
    [ObservableProperty] private int _anosDesaparecido;
    [ObservableProperty] private int _mesesDesaparecido;
    [ObservableProperty] private int _diasDesaparecido;
    [ObservableProperty] private ObservableCollection<string> _files = new();
    [ObservableProperty] private string _curp;

    // Valores para insercion a listas
    [ObservableProperty] private string _noTelefonoReportante = string.Empty;
    [ObservableProperty] private string _observacionesTelefonoReportante = string.Empty;
    [ObservableProperty] private string _noTelefonoDesaparecido = string.Empty;
    [ObservableProperty] private string _observacionesTelefonoDesaparecido = string.Empty;

    // Visibilidades
    [ObservableProperty] private bool _seDesconoceFechaNacimientoDesaparecido;
    [ObservableProperty] private bool _seDesconoceFechaExactaHechos;
    [ObservableProperty] private bool _reportanteTieneTelefonos;
    [ObservableProperty] private bool _desaparecidoTieneTelefonos;
    [ObservableProperty] private bool _hayPrendas;
    [ObservableProperty] private ObservableCollection<BitmapImage> _imagenesDesaparecido = [];
    [ObservableProperty] private BitmapImage? _imagenBoletin;
    [ObservableProperty] private bool _noHayCurp;

    public EncuadrePreeliminarViewModel() =>
        InitAsync();

    private async Task CargarCatalogos()
    {
        Sexos = await ReportanteNetwork.GetCatalogo("sexos");
        RazonesCurp = await DesaparecidoNetwork.GetCatalogo("razones-curp");
        Generos = await ReportanteNetwork.GetCatalogo("generos");
        Parentescos = await ReportanteNetwork.GetCatalogo("parentescos");
        Nacionalidades = await ReportanteNetwork.GetCatalogo("nacionalidades");
        CompaniasTelefonicas = await DesaparecidoNetwork.GetCatalogo("companias-telefonicas");
        Complexiones = await DesaparecidoNetwork.GetCatalogo("complexiones");
        ColoresPiel = await DesaparecidoNetwork.GetCatalogo("colores-pieles");
        ColoresOjos = await DesaparecidoNetwork.GetCatalogo("colores-ojos");
        ColoresCabello = await DesaparecidoNetwork.GetCatalogo("colores-cabellos");
        TamanosCabello = await DesaparecidoNetwork.GetCatalogo("tamanos-cabellos");
        TiposCabello = await DesaparecidoNetwork.GetCatalogo("tipos-cabellos");
        Vistas = await SenasParticularesNetwork.GetCatalogo("vistas");
        Tipos = await SenasParticularesNetwork.GetCatalogo("tipos");
        Colores = await SenasParticularesNetwork.GetCatalogo("colores");
        //Pertenencias = await PrendasNetwork.GetPertenencias();
        GruposPertenencia = await SenasParticularesNetwork.GetCatalogo("grupos-pertenencias");
        RegionesCuerpo = await SenasParticularesNetwork.GetCatalogoColor("regiones-cuerpo");
        Lados = await SenasParticularesNetwork.GetCatalogoColor("lados");
        Estados = await ReportanteNetwork.GetEstados();
        ZonasEstados = await SenasParticularesNetwork.GetCatalogo("zonas-estados");
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
    }

    private void DefaultValues()
    {
        ColorRegionCuerpo = "3F48CC";
        ColorLado = "6C7156";
        Descripcion = "";
        Cantidad = 1;
    }

    private void GetReporteFromService()
    {
        Reporte = _reporteService.GetReporte();

        if (Reporte.Reportantes.Any())
        {
            Reportante = Reporte.Reportantes.First();
        }
        else
        {
            Reportante = new Reportante();
            Reporte.Reportantes.Add(Reportante);
        }

        if (Reporte.Desaparecidos.Any())
        {
            Desaparecido = Reporte.Desaparecidos.First();
        }
        else
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        DefaultValues();
        GetReporteFromService();
        Curp = "";
    }

    private void DiferenciaFechas(DateTime? a, DateTime? b)
    {
        if (a == null || b == null) return;

        AnosDesaparecido = (int)(b?.Year - a?.Year)!;
        MesesDesaparecido = (int)(b?.Month - a?.Month)!;
        DiasDesaparecido = (int)(b?.Day - a?.Day)!;

        if (DiasDesaparecido < 0)
        {
            MesesDesaparecido--;
            DiasDesaparecido += DateTime.DaysInMonth((int)b?.Year!, (int)b?.Month!);
        }

        if (MesesDesaparecido < 0)
        {
            AnosDesaparecido--;
            MesesDesaparecido += 12;
        }
    }

    partial void OnCurpChanged(string value)
    {
        NoHayCurp = value.Length == 0;
        Desaparecido.Persona.Curp = value;
    }

    async partial void OnTipoMedioSelectedChanged(Catalogo value) =>
        Medios = await DatosReporteNetwork.GetMedios(value.Id);

    async partial void OnEstadoSelectedChanged(Estado value)
    {
        if (value == null) return;
        Municipios = await ReportanteNetwork.GetMunicipiosDeEstado(value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio value)
    {
        if (value == null) return;
        Asentamientos = await ReportanteNetwork.GetAsentamientosDeMunicipio(value.Id);
    }

    async partial void OnGrupoPerteneciaSelectedChanged(Catalogo value)
    {
        if (value == null) return;
        Pertenencias = await PrendasNetworkEncuadrePreeliminar.GetPertenencias(value.Id ?? 0);
    }

    partial void OnSeDesconoceFechaNacimientoDesaparecidoChanged(bool value)
    {
        if (value)
        {
            Desaparecido.Persona.FechaNacimiento = null;
            Desaparecido.FechaNacimientoAproximada = FechaNacimientoDesaparecido;
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }
        else
        {
            Desaparecido.Persona.FechaNacimiento = FechaNacimientoDesaparecido;
            Desaparecido.FechaNacimientoAproximada = null;
            Desaparecido.EdadMomentoDesaparicionAnos = null;
            Desaparecido.EdadMomentoDesaparicionMeses = null;
            Desaparecido.EdadMomentoDesaparicionDias = null;
        }
    }

    partial void OnSeDesconoceFechaExactaHechosChanged(bool value)
    {
        if (value)
        {
            Reporte.HechosDesaparicion.FechaDesaparicion = null;
            Reporte.HechosDesaparicion.FechaDesaparicionAproximada = FechaDesaparicion;
        }
        else
        {
            Reporte.HechosDesaparicion.FechaDesaparicion = FechaDesaparicion;
            Reporte.HechosDesaparicion.FechaDesaparicionAproximada = null;
        }
    }
    
    partial void OnFechaNacimientoDesaparecidoChanged(DateTime value)
    {
        DiferenciaFechas(FechaNacimientoDesaparecido, FechaDesaparicion);
        if (SeDesconoceFechaNacimientoDesaparecido)
        {
            Desaparecido.FechaNacimientoAproximada = value;
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }
        else
        {
            Desaparecido.Persona.FechaNacimiento = value;
        }
    }

    partial void OnFechaDesaparicionChanged(DateTime value)
    {
        DiferenciaFechas(FechaNacimientoDesaparecido, FechaDesaparicion);
        if (SeDesconoceFechaNacimientoDesaparecido)
        {
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }

        if (SeDesconoceFechaExactaHechos)
        {
            Reporte.HechosDesaparicion.FechaDesaparicionAproximada = value;
        }
        else
        {
            Reporte.HechosDesaparicion.FechaDesaparicion = value;
        }
        
    }

    [RelayCommand]
    private void OnAddTelefonoMovilReportante()
    {
        if (NoTelefonoReportante.Length <= 0) return;

        var telefonos = Reportante.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoReportante,
            Observaciones = ObservacionesTelefonoReportante,
            EsMovil = true,
            Compania = CompañiaTelefonicaReportanteSelected
        });

        NoTelefonoReportante = string.Empty;
        ObservacionesTelefonoReportante = string.Empty;
        CompañiaTelefonicaReportanteSelected = null;
        ReportanteTieneTelefonos = Reportante.Persona?.Telefonos?.Any() ?? false;
    }

    [RelayCommand]
    private void OnAddTelefonoMovilDesaparecido()
    {
        if (NoTelefonoDesaparecido.Length <= 0) return;

        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoDesaparecido,
            Observaciones = ObservacionesTelefonoDesaparecido,
            EsMovil = true,
            Compania = CompañiaTelefonicaDesaparecidoSelected
        });

        NoTelefonoDesaparecido = string.Empty;
        ObservacionesTelefonoDesaparecido = string.Empty;
        CompañiaTelefonicaDesaparecidoSelected = null;
        DesaparecidoTieneTelefonos = Desaparecido.Persona?.Telefonos?.Any() ?? false;
    }

    [RelayCommand]
    private void OnAddPrendaDeVestir()
    {
        if (PerteneciaSelected == null) return;

        var prendasDeVestir = Desaparecido.PrendasDeVestir;
        prendasDeVestir?.Add(new PrendaDeVestir
        {
            Marca = CurrentMarca,
            Descripcion = CurrentPrendaDescripcion,
            Pertenencia = PerteneciaSelected,
            Color = ColorSelected
        });

        CurrentMarca = String.Empty;
        CurrentPrendaDescripcion = string.Empty;
        GrupoPerteneciaSelected = null;
        PerteneciaSelected = null;
        ColorSelected = null;
        HayPrendas = Desaparecido.PrendasDeVestir?.Any() ?? false;
    }

    [RelayCommand]
    private void OnRemovePrendaDeVestir(PrendaDeVestir prenda)
    {
        Desaparecido.PrendasDeVestir?.Remove(prenda);
    }

    [RelayCommand]
    private void OnRemoveTelefonoReportante(Telefono telefono)
    {
        Reportante.Persona.Telefonos?.Remove(telefono);
    }

    [RelayCommand]
    private void OnRemoveTelefonoDesaparecido(Telefono telefono)
    {
        Desaparecido.Persona.Telefonos?.Remove(telefono);
    }

    partial void OnColorRegionCuerpoChanged(string value)
    {
        var region = RegionesCuerpo.FirstOrDefault(e => e.Color == value);
        RegionCuerpoSelected = region ?? RegionesCuerpo.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorLadoChanged(string value)
    {
        var lado = Lados.FirstOrDefault(e => e.Color == value);
        LadoSelected = lado ?? Lados.First(e => e.Nombre == "NO ESPECIFICA");
    }

    [RelayCommand]
    private void OnAddSenaParticular()
    {
        Desaparecido.Persona.SenasParticulares.Add(new SenaParticular
        {
            Cantidad = Cantidad,
            Descripcion = Descripcion,
            Foto = null,
            RegionCuerpo = RegionCuerpoSelected,
            Vista = VistaSelected,
            Lado = LadoSelected,
            Tipo = TipoSelected
        });
    }

    [RelayCommand]
    private void OnDeleteSenaParticular(dynamic sena)
    {
        var senaParticular = sena as SenaParticular;
        if (senaParticular != null) Desaparecido.Persona.SenasParticulares.Remove(senaParticular);
    }
    
    [RelayCommand]
    private void OnOpenFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        //Files = new ObservableCollection<string>(openFileDialog.FileNames);
        foreach (var file in openFileDialog.FileNames)
        {
            ImagenesDesaparecido.Add(new BitmapImage(new Uri(file)));
        }
    }

    [RelayCommand]
    private async void OnGuardarReporte()
    {
        // Añadir registros pendientes
        AddTelefonoMovilReportanteCommand.Execute(null);
        AddTelefonoMovilDesaparecidoCommand.Execute(null);
        AddSenaParticularCommand.Execute(null);
        AddPrendaDeVestirCommand.Execute(null);
        
        if (await _reporteService.Sync() == null)
        {
            _snackBarService.Show(
                "Error fatal",
                "No se pudo actualizar o ingresar la informacion del reporte",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning32),
                new TimeSpan(0, 0, 5));
            return;
        }

        GetReporteFromService();
        if (ImagenesDesaparecido.Count > 0)
        {
            await ReporteServiceNetwork.SubirFotosDesaparecido(Desaparecido.Id, ImagenesDesaparecido.ToList(), ImagenBoletin);
        }

        var modal = new PostEncuadreModalWindow();
        if (modal.ShowDialog() ?? false)
        {
            _navigationService.Navigate(typeof(ReportesDesaparicion));
            _snackBarService.Show(
                "El reporte ha sido creado exitosamente",
                "Se ha creado el reporte de manera exitosa, ha sido redireccionado a la pantalla de consultas.",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.Checkmark32),
                new TimeSpan(0, 0, 5));
        }
    }
}