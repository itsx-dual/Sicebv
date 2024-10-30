using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.dashboard.encuadre_preeliminar.Data;
using Cebv.features.dashboard.encuadre_preeliminar.presentation.ListasEditables;
using Cebv.features.dashboard.reportes_desaparicion.presentation;
using Cebv.features.formulario_cebv.prendas.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Wpf.Ui.Controls;
using Color = System.Windows.Media.Color;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableValidator
{
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private static IDashboardNavigationService _navigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;

    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;

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
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPiel = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabello = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabello = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabello = new();

    [ObservableProperty] private ObservableCollection<CatalogoColor> _vistas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos = new();
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados = new();
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia = new();
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();

    // Valores seleccionados
    [ObservableProperty] 
    [Required(ErrorMessage = "El campo Tipo de medio es obligatorio")]
    private Catalogo? _tipoMedioSelected;
    
    [ObservableProperty] private Estado? _estadoSelected;
    
    [ObservableProperty] 
    [Required(ErrorMessage = "El campo Municipio es obligatorio")]
    private Municipio? _municipioSelected;
    
    [ObservableProperty] private Catalogo? _compañiaTelefonicaReportanteSelected;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    [ObservableProperty] private CatalogoColor? _vistaSelected;
    [ObservableProperty] private Catalogo? _tipoSelected;
    [ObservableProperty] private BitmapImage? _imagenSenaParticularSelected;
    [ObservableProperty] private CatalogoColor? _regionCuerpoSelected;
    [ObservableProperty] private CatalogoColor? _ladoSelected;
    [ObservableProperty] private string? _colorRegionCuerpo;
    [ObservableProperty] private string? _colorLado;
    [ObservableProperty] private int _cantidad = 1;
    [ObservableProperty] private string? _descripcion;

    [ObservableProperty] private Catalogo? _grupoPerteneciaSelected;
    [ObservableProperty] private Pertenencia? _perteneciaSelected;
    [ObservableProperty] private Catalogo? _colorSelected;
    [ObservableProperty] private string? _currentMarca;
    [ObservableProperty] private string? _currentPrendaDescripcion;

    [ObservableProperty] private DateTime _fechaNacimientoDesaparecido = DateTime.Today;
    [ObservableProperty] private DateTime _fechaDesaparicion = DateTime.Today;
    [ObservableProperty] private TimeSpan _horaDesaparicion;
    [ObservableProperty] private int _anosDesaparecido;
    [ObservableProperty] private int _mesesDesaparecido;
    [ObservableProperty] private int _diasDesaparecido;
    [ObservableProperty] private ObservableCollection<string> _files = new();
    [ObservableProperty] private string? _curp;

    // Valores para insercion a listas
    [ObservableProperty] 
    [Required(ErrorMessage = "El campo No. Telefono reportante es obligatorio")]
    [MinLength(8, ErrorMessage = "El campo No. Telefono reportante debe tener al menos 8 numeroa")]
    private string _noTelefonoReportante = string.Empty;
    
    [ObservableProperty] private string _observacionesTelefonoReportante = string.Empty;

    [ObservableProperty] 
    [Required(ErrorMessage = "El campo No. Telefono reportante es obligatorio")]
    [MinLength(8, ErrorMessage = "El campo No. Telefono reportante debe tener al menos 8 numeroa")]
    private string _noTelefonoDesaparecido = string.Empty;
    
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
    
    public EncuadrePreeliminarViewModel()
    {
        InitAsync();
        
    }

    private async Task CargarCatalogos()
    {
        Stopwatch sw = new();
        sw.Start();
        Sexos = await CebvNetwork.GetRoute<Catalogo>("sexos");
        RazonesCurp = await CebvNetwork.GetRoute<Catalogo>("razones-curp");
        Generos = await CebvNetwork.GetRoute<Catalogo>("generos");
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
        Nacionalidades = await CebvNetwork.GetRoute<Catalogo>("nacionalidades");
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
        Complexiones = await CebvNetwork.GetRoute<Catalogo>("complexiones");
        ColoresPiel = await CebvNetwork.GetRoute<Catalogo>("colores-piel");
        ColoresOjos = await CebvNetwork.GetRoute<Catalogo>("colores-ojos");
        ColoresCabello = await CebvNetwork.GetRoute<Catalogo>("colores-cabello");
        TamanosCabello = await CebvNetwork.GetRoute<Catalogo>("tamanos-cabello");
        TiposCabello = await CebvNetwork.GetRoute<Catalogo>("tipos-cabello");
        Vistas = await CebvNetwork.GetRoute<CatalogoColor>("vistas");
        Tipos = await CebvNetwork.GetRoute<Catalogo>("tipos");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
        GruposPertenencia = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        RegionesCuerpo = await CebvNetwork.GetRoute<CatalogoColor>("regiones-cuerpo");
        Lados = await CebvNetwork.GetRoute<CatalogoColor>("lados");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        TiposMedios = await CebvNetwork.GetRoute<Catalogo>("tipos-medios");
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    private void DefaultValues()
    {
        ColorRegionCuerpo = "3F48CC";
        ColorLado = "6C7156";
        Descripcion = "";
        Cantidad = 1;
        ImagenSenaParticularSelected = null;
    }

    private void GetReporteFromService()
    {
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Reportantes.Any())
        {
            Reporte.Reportantes.Add(new Reportante());
        }

        Reportante = Reporte.Reportantes.First();

        if (!Reporte.Desaparecidos.Any())
        {
            Reporte.Desaparecidos.Add(new Desaparecido());
        }

        Desaparecido = Reporte.Desaparecidos.First();
        Reporte.HechosDesaparicion ??= new();
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        DefaultValues();
        GetReporteFromService();
        //Desaparecido.Persona = new();
        //Reportante.Persona = new();
        //Reporte.HechosDesaparicion = new();
        Curp = "";
        FechaDesaparicion = DateTime.Now;
        Desaparecido.Persona.Salud ??= new();
        Desaparecido.Persona.Ojos ??= new();
        Desaparecido.Persona.Cabello ??= new();
        Desaparecido.Persona.VelloFacial ??= new();
        Desaparecido.Persona.Nariz ??= new();
        Desaparecido.Persona.Boca ??= new();
        Desaparecido.Persona.Orejas ??= new();
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

        if (MesesDesaparecido >= 0) return;
        AnosDesaparecido--;
        MesesDesaparecido += 12;
    }

    partial void OnCurpChanged(string? value)
    {
        NoHayCurp = value?.Length == 0;
        if (Desaparecido.Persona.Curp is null) return;
        Desaparecido.Persona.Curp = value;
    }

    async partial void OnTipoMedioSelectedChanged(Catalogo? value)
    {
        if (value is null) return;
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>("medios", "tipo_medio_id", value.Id.ToString()!);
    }

    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value == null) return;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value == null) return;
        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }

    async partial void OnGrupoPerteneciaSelectedChanged(Catalogo? value)
    {
        if (value == null ) return;
        Pertenencias = await CebvNetwork.GetByFilter<Pertenencia>("pertenencias", "grupo_pertenencia_id", value.Id.ToString()!);
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
            Reporte.HechosDesaparicion!.FechaDesaparicion = null;
            Reporte.HechosDesaparicion.FechaDesaparicion = FechaDesaparicion;
        }
        else
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = FechaDesaparicion;
            Reporte.HechosDesaparicion.FechaDesaparicion = null;
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
            Reporte.HechosDesaparicion!.FechaDesaparicion = value;
        }
        else
        {
            Reporte.HechosDesaparicion!.FechaDesaparicion = value;
        }
    }

    [RelayCommand]
    private void OnAddTelefonoMovilReportante()
    {
        if (NoTelefonoReportante.Length <= 0) return;

        var telefonos = Reportante.Persona.Telefonos;
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
        ReportanteTieneTelefonos = Reportante.Persona?.Telefonos.Any() ?? false;
    }

    [RelayCommand]
    private void OnAddTelefonoMovilDesaparecido()
    {
        if (NoTelefonoDesaparecido.Length <= 0) return;

        var telefonos = Desaparecido.Persona.Telefonos;
        telefonos.Add(new Telefono
        {
            Numero = NoTelefonoDesaparecido,
            Observaciones = ObservacionesTelefonoDesaparecido,
            EsMovil = true,
            Compania = CompañiaTelefonicaDesaparecidoSelected
        });

        NoTelefonoDesaparecido = string.Empty;
        ObservacionesTelefonoDesaparecido = string.Empty;
        CompañiaTelefonicaDesaparecidoSelected = null;
        DesaparecidoTieneTelefonos = Desaparecido.Persona.Telefonos.Any();
    }

    [RelayCommand]
    private void OnAddPrendaDeVestir()
    {
        if (PerteneciaSelected == null) return;

        var prendasDeVestir = Desaparecido.PrendasVestir;
        prendasDeVestir.Add(new PrendaVestir()
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
        HayPrendas = Desaparecido.PrendasVestir.Any();
    }

    [RelayCommand]
    private void OnRemovePrendaDeVestir(PrendaVestir prenda)
    {
        Desaparecido.PrendasVestir.Remove(prenda);
    }

    [RelayCommand]
    private void OnRemoveTelefonoReportante(Telefono telefono)
    {
        Reportante.Persona.Telefonos.Remove(telefono);
    }

    [RelayCommand]
    private void OnRemoveTelefonoDesaparecido(Telefono telefono)
    {
        Desaparecido.Persona.Telefonos.Remove(telefono);
    }
    
    string _visibiliity = string.Empty;
    
    [RelayCommand]
    private async Task OnEditarTelefonoReportante(Telefono telefono)
    {
        var showEditList = new ShowDialogEditList();

        // Crea una instancia de EditarTelefonoDialogContent y asigna el DataContext
        var dialogContent = new EditTelefono(_visibiliity = "Reportante")
        {
            DataContext = this
        };

        await showEditList.ShowContentDialogCommand.ExecuteAsync(dialogContent);

        if (showEditList.Confirmacion)
        {
            Reportante.Persona.Telefonos.Remove(telefono);

            var telefonos = Reportante.Persona.Telefonos;
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
            ReportanteTieneTelefonos = Reportante.Persona?.Telefonos.Any() ?? false;
        }
    }

    [RelayCommand]
    private async Task OnEditarTelefonoDesaparecido(Telefono telefono)
    {
        var showEditList = new ShowDialogEditList();

        // Crea una instancia de EditarTelefonoDialogContent y asigna el DataContext
        var dialogContent = new EditTelefono(_visibiliity = "Desaparecido")
        {
            DataContext = this
        };

        await showEditList.ShowContentDialogCommand.ExecuteAsync(dialogContent);

        if (showEditList.Confirmacion)
        {
            Desaparecido.Persona.Telefonos.Remove(telefono);

            var telefonos = Desaparecido.Persona.Telefonos;
            telefonos.Add(new Telefono
            {
                Numero = NoTelefonoDesaparecido,
                Observaciones = ObservacionesTelefonoDesaparecido,
                EsMovil = true,
                Compania = CompañiaTelefonicaDesaparecidoSelected
            });

            NoTelefonoDesaparecido = string.Empty;
            ObservacionesTelefonoDesaparecido = string.Empty;
            CompañiaTelefonicaDesaparecidoSelected = null;
            DesaparecidoTieneTelefonos = Desaparecido.Persona.Telefonos.Any();
        }
    }

    /*partial void OnColorRegionCuerpoChanged(string? value)
    {
        // TODO: hay un error aca tambien
        var region = RegionesCuerpo.FirstOrDefault(e => e.Color == value);
        RegionCuerpoSelected = region ?? RegionesCuerpo.First(e => e.Nombre == "NO ESPECIFICA");
    }*/

    /*partial void OnColorLadoChanged(string? value)
    {
        var lado = Lados.FirstOrDefault(e => e.Color == value);
        LadoSelected = lado ?? Lados.First(e => e.Nombre == "NO ESPECIFICA");
    }*/

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
            Tipo = TipoSelected,
            Imagen = ImagenSenaParticularSelected
        });
    }

    [RelayCommand]
    private void OnDeleteSenaParticular(dynamic sena)
    {
        var senaParticular = sena as SenaParticular;
        if (senaParticular != null) Desaparecido.Persona.SenasParticulares.Remove(senaParticular);
    }

    [RelayCommand]
    private void OnOpenDesaparecidoImages()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            Multiselect = true,
            Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.webp"
        };

        if (openFileDialog.ShowDialog() == false) return;
        if (!File.Exists(openFileDialog.FileName)) return;

        foreach (var file in openFileDialog.FileNames)
        {
            ImagenesDesaparecido.Add(new BitmapImage(new Uri(file)));
        }
    }

    [RelayCommand]
    private void OnDeleteDesaparecidoImagen(BitmapImage image) => ImagenesDesaparecido.Remove(image);

    [RelayCommand]
    private void OnOpenSenaParticularImage()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            Multiselect = false,
            Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.webp"
        };

        if (openFileDialog.ShowDialog() == false) return;
        if (!File.Exists(openFileDialog.FileName)) return;
        ImagenSenaParticularSelected = new(new Uri(openFileDialog.FileName));
    }

    public void Validate() => ValidateAllProperties();

    private bool _cancelar = true;
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = EncuadrePreeliminarDictionary.GetEncuadrePreliminarDictionary(this, Reporte, Reportante, Desaparecido);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            if (dialogo.Confirmacion == "Guardar") confirmacion = true;
            else if (dialogo.Confirmacion == "No guardar") return _cancelar;
        }
        else confirmacion = true;

        return confirmacion;
    }
    
    [RelayCommand]
    private async Task OnGuardarReporte()
    {
        if (!EncuadrePreeliminarDictionary.ValidateEncuadre(this, Reporte, Reportante, Desaparecido))
        {
            string errores = ListEmptyElements.GetAllValidationMessages(new List<ObservableValidator>
            { this, Reportante.Persona, Desaparecido.Persona, Reporte.HechosDesaparicion, Reporte });
            
            _snackBarService.Show(
                "Error en los campos",
                "Por favor, revise los campos obligatorios y corrija los siguientes errores:\n" + errores,
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning48),
                new TimeSpan(0, 0, 10));
            return;
        }
        
        if (!await EnlistarCampos())
            return;
        
        // Añadir registros pendientes
        AddTelefonoMovilReportanteCommand.Execute(null);
        AddTelefonoMovilDesaparecidoCommand.Execute(null);
        AddPrendaDeVestirCommand.Execute(null);

        var senasParticulares = Desaparecido.Persona.SenasParticulares.ToList();

        if (await _reporteService.Sync() is null)
        {
            _snackBarService.Show(
                "Error fatal",
                "No se pudo actualizar o ingresar la informacion del reporte",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning48),
                new TimeSpan(0, 0, 5));
            return;
        }

        GetReporteFromService();
        if (ImagenesDesaparecido.Count > 0)
        {
            await ReporteServiceNetwork.SubirFotosDesaparecido(Desaparecido.Id ?? 0, ImagenesDesaparecido.ToList(), ImagenBoletin);
        }

        var modal = new PostEncuadreModalWindow();
        if (!(modal.ShowDialog() ?? false)) return;
        _navigationService.Navigate(typeof(ReportesDesaparicionPage));
        _snackBarService.Show(
            "El reporte ha sido creado exitosamente",
            "Se ha creado el reporte de manera exitosa, ha sido redireccionado a la pantalla de consultas.",
            ControlAppearance.Success,
            new SymbolIcon(SymbolRegular.Checkmark32),
            new TimeSpan(0, 0, 5));
    }
}