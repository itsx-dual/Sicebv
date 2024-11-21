using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.components.edit_telefono;
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
    private static IDashboardNavigationService _navigationService = App.Current.Services.GetService<IDashboardNavigationService>()!;
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;
    
    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Reportante _reportante = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;

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
    
    // --------------------------------------------
    
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

    [RelayCommand]
    private async Task OnEditarTelefonoReportante(Telefono telefono)
    {
        // Se crea una nueva instancia:
        var telefonoClone = new Telefono(telefono);
        
        // Se manda a llamar el ContentDialog el cual muestra una nueva instancia del UserControl.
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Editar Seña Particular",
                Content = new EditTelefonoUserControl
                {
                    DataContext = new EditTelefonoViewModel() { Telefono = telefonoClone }
                },
                PrimaryButtonText = "Guardar",
                CloseButtonText = "Descartar"
            },
            new CancellationToken()
        );

        // Si se dio en guardar
        if (result != ContentDialogResult.Primary) return;
        
        // Se busca el indice del elemento seleccionado en la lista.
        var index = Reportante.Persona.Telefonos.ToList().FindIndex(x => x.Equals(telefono));
        
        // Se sobreescribe la instancia anterior.
        Reportante.Persona.Telefonos[index] = telefonoClone;
    }
    
    [RelayCommand]
    private async Task OnEditarTelefonoDesaparecido(Telefono telefono)
    {
        // Se crea una nueva instancia:
        var telefonoClone = new Telefono(telefono);
        
        // Se manda a llamar el ContentDialog el cual muestra una nueva instancia del UserControl.
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Editar Seña Particular",
                Content = new EditTelefonoUserControl
                {
                    DataContext = new EditTelefonoViewModel() { Telefono = telefonoClone }
                },
                PrimaryButtonText = "Guardar",
                CloseButtonText = "Descartar"
            },
            new CancellationToken()
        );

        // Si se dio en guardar
        if (result != ContentDialogResult.Primary) return;
        
        // Se busca el índice del elemento seleccionado en la lista.
        var index = Desaparecido.Persona.Telefonos.ToList().FindIndex(x => x.Equals(telefono));
        
        // Se sobreescribe la instancia anterior.
        Desaparecido.Persona.Telefonos[index] = telefonoClone;
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
    private async Task OnGuardarReporte()
    {
        // Añadir registros pendientes
        AddTelefonoMovilReportanteCommand.Execute(null);
        AddTelefonoMovilDesaparecidoCommand.Execute(null);

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
            await ReporteServiceNetwork.SubirFotosDesaparecido(Desaparecido.Id ?? 0, Enumerable.ToList<BitmapImage>(ImagenesDesaparecido), ImagenBoletin);
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