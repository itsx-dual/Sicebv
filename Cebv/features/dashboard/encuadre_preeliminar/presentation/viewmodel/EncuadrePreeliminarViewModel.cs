using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.components.edit_telefono;
using Cebv.features.components.mensaje_basico;
using Cebv.features.dashboard.reportes_desaparicion.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Wpf.Ui;
using Wpf.Ui.Controls;
using ISnackbarService = Cebv.core.util.snackbar.ISnackbarService;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel;

public partial class EncuadrePreeliminarViewModel : ObservableValidator
{
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private static IDashboardNavigationService _navigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;

    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;


    [Required(ErrorMessage = "El campo No. Telefono reportante es obligatorio")]
    [MinLength(8, ErrorMessage = "El campo No. Telefono reportante debe tener al menos 8 numeroa")]
    
        
        
    [ObservableProperty] private ObservableCollection<BitmapImage> _imagenesDesaparecido = [];
    [ObservableProperty] private BitmapImage _imagenBoletin;

    // Listas para almacenar catalogos
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

    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencia = new();
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();


    [ObservableProperty]
    [Range(0,23)]
    private int? _horas ;
    [ObservableProperty] 
    [Range(0,59)]private int? _minutos;

    
    
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
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
        GruposPertenencia = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        TiposMedios = await CebvNetwork.GetRoute<Catalogo>("tipos-medios");
        sw.Stop();
        Console.WriteLine($"Los catalogos tardaron: {sw.Elapsed} en cargar.");
    }

    private void GetReporteFromService()
    {
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Reportantes.Any()) Reporte.Reportantes.Add(new Reportante());
        Reportante = Reporte.Reportantes.First();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(new Desaparecido());
        Desaparecido = Reporte.Desaparecidos.First();

        Reporte.HechosDesaparicion ??= new HechosDesaparicion();
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        GetReporteFromService();
        Curp = "";
        FechaDesaparicion = DateTime.Now;
        Desaparecido.Persona.Salud ??= new Salud();
        Desaparecido.Persona.Ojos ??= new Ojos();
        Desaparecido.Persona.Cabello ??= new Cabello();
        Desaparecido.Persona.VelloFacial ??= new VelloFacial();
        Desaparecido.Persona.Nariz ??= new Nariz();
        Desaparecido.Persona.Boca ??= new Boca();
        Desaparecido.Persona.Orejas ??= new Orejas();
    }

    private void DiferenciaFechas(DateTime? a, DateTime? b)
    {
        if (a is null || b is null) return;

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

    private async Task EditarTelefono(Telefono telefono, ObservableCollection<Telefono> lista,
        Visibility verCatalogos = Visibility.Visible)
    {
        var telefonoEditado = new Telefono(telefono);
        var content = new EditTelefonoUserControl
        {
            DataContext = new EditTelefonoViewModel
            {
                Telefono = telefonoEditado,
                VerCompanias = verCatalogos
            }
        };

        var result = await DialogHelper.ShowDialog(content, "Editar telefono");

        if (result != ContentDialogResult.Primary) return;
        lista.Update(telefono, telefonoEditado);
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

    private List<ValidationResult> ValidarEncuadre()
    {
        ValidateProperty(TipoMedioSelected, nameof(TipoMedioSelected)); // Inicio
        
        // Hechos:
        ValidateProperty(FechaDesaparicion, nameof(FechaDesaparicion)); 
        ValidateProperty(EstadoSelected, nameof(EstadoSelected));
        ValidateProperty(MunicipioSelected, nameof(MunicipioSelected));
        
        Reporte.ValidarReporteEncuadre();
        if (!Reportante.DenunciaAnonima) Reportante.Persona.ValidarReportante();
        Desaparecido.Persona.ValidarDesaparecido();
        Reporte.HechosDesaparicion?.Direccion.Validar();

        List<ValidationResult> errores = [];

        if (Reporte.HasErrors) errores.AddRange(Reporte.GetErrors());
        if (Reportante.Persona.HasErrors && !Reportante.DenunciaAnonima) errores.AddRange(Reportante.Persona.GetErrors());
        if (Desaparecido.Persona.HasErrors) errores.AddRange(Desaparecido.Persona.GetErrors());
        if (Reporte.HechosDesaparicion?.Direccion.HasErrors ?? false) errores.AddRange(Reporte.HechosDesaparicion.GetErrors());
        if (this.HasErrors) errores.AddRange(this.GetErrors());

        return errores;
    }

    [RelayCommand]
    private async Task OnGuardarReporte()
    {
        var errores = ValidarEncuadre();
        
        if (errores.Any())
        {
            var content = new MensajeBasicoUserControl
            {
                DataContext = new MensajeBasicoViewModel
                {
                    Mensaje = errores.Aggregate(string.Empty,
                        (current, error) => current + (error.ErrorMessage + Environment.NewLine))
                }
            };
            await DialogHelper.ShowDialog(content, "El encuadre no puede ser registrado.");
            return;
        }
        

        if (await _reporteService.Sync() is null)
        {
            _snackBarService.Show(
                "Error fatal",
                "No se pudo actualizar o ingresar la informacion del reporte.",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning48),
                new TimeSpan(0, 0, 5));
            return;
        }

        GetReporteFromService();
        
        if (ImagenesDesaparecido.Count > 0)
        {
            await ReporteServiceNetwork.SubirFotosDesaparecido(Desaparecido.Id ?? 0, Enumerable.ToList(ImagenesDesaparecido), ImagenBoletin);
        }

        var result = await DialogHelper.ShowDialog(new PostEncuadreUserControl(), "Generación de folio y reportes en PDF.");
        if (result != ContentDialogResult.Primary) return;
        _navigationService.Navigate(typeof(ReportesDesaparicionPage));
        _snackBarService.Show(
            "El reporte ha sido creado exitosamente",
            "Se ha creado el reporte de manera exitosa, ha sido redireccionado a la pantalla de consultas.",
            ControlAppearance.Success,
            new SymbolIcon(SymbolRegular.Checkmark32),
            new TimeSpan(0, 0, 5));
    }
    //Validaciones

   
    partial void OnMinutosChanged(int? value)
    {
        if(value is null)return;
        ValidateProperty(value,nameof(Minutos));
    }
    partial void OnHorasChanged(int? value)
    {
        if(value is null)return;
        ValidateProperty(value,nameof(Horas));
    }
}