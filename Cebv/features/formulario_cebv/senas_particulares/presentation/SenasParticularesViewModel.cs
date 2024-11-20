using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.senas_particulares.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.senas_particulares.presentation;

public partial class SenasParticularesViewModel : ObservableObject
{
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = new();
    [ObservableProperty] private Desaparecido _desaparecido = new();

    // Catalogos
    [ObservableProperty] private ObservableCollection<CatalogoColor> _vistas = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos = [];
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados = [];
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo = [];

    [ObservableProperty] private string _colorRegionCuerpo;
    [ObservableProperty] private string _colorLado;
    [ObservableProperty] private string _colorVista;

    [ObservableProperty] private SenaParticular _senaParticular = new();
    public BitmapImage? FallbackImage { get; set; }

    public SenasParticularesViewModel()
    {
        InitAsync();
        //FallbackImage = new BitmapImage(new Uri());
    }

    private async Task CargarCatalogos()
    {
        Vistas = await CebvNetwork.GetRoute<CatalogoColor>("vistas");
        Tipos = await CebvNetwork.GetRoute<Catalogo>("tipos");
        RegionesCuerpo = await CebvNetwork.GetRoute<CatalogoColor>("regiones-cuerpo");
        Lados = await CebvNetwork.GetRoute<CatalogoColor>("lados");
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any())
        {
            Reporte.Desaparecidos.Add(new Desaparecido());
        }

        Desaparecido = Reporte.Desaparecidos.First();
    }

    partial void OnColorRegionCuerpoChanged(string value)
    {
        if (value.Length < 1) return;

        // TODO: Aqui hay un error
        var region = RegionesCuerpo.FirstOrDefault(e => e.Color == value);
        SenaParticular.RegionCuerpo = region ?? RegionesCuerpo.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorLadoChanged(string value)
    {
        if (value.Length < 1) return;

        var lado = Lados.FirstOrDefault(e => e.Color == value);
        SenaParticular.Lado = lado ?? Lados.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorVistaChanged(string value)
    {
        if (value.Length < 1) return;

        var vista = Vistas.FirstOrDefault(e => e.Color == value);
        SenaParticular.Vista = vista ?? Vistas.First(e => e.Nombre == "NO ESPECIFICA");
    }

    [RelayCommand]
    private void OnAddSenaParticular()
    {
        Desaparecido.Persona.SenasParticulares.Add(SenaParticular);
        SenaParticular = new SenaParticular();
    }

    [RelayCommand]
    private void OnDeleteSenaParticular(dynamic sena)
    {
        var senaParticular = sena as SenaParticular;
        if (senaParticular != null) Desaparecido.Persona.SenasParticulares.Remove(senaParticular);
    }

    [RelayCommand]
    private async void OnEditSenaParticular(SenaParticular sena)
    {
        // Se crea una nueva instancia:
        var senaClone = new SenaParticular(sena);
        
        // Se manda a llamar el ContentDialog el cual muestra una nueva instancia del UserControl.
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Editar Seña Particular",
                Content = new EditSenasParticularesUserControl
                {
                    DataContext = new SenasParticularesViewModel { SenaParticular = senaClone }
                },
                PrimaryButtonText = "Guardar",
                CloseButtonText = "Descartar"
            },
            new CancellationToken()
        );

        // Si se dio en guardar
        if (result != ContentDialogResult.Primary) return;
        
        // Se busca el indice del elemento seleccionado en la lista.
        var index = Desaparecido.Persona.SenasParticulares.ToList().FindIndex(x => x.Equals(sena));
        
        // Se sobreescribe la instancia anterior.
        Desaparecido.Persona.SenasParticulares[index] = senaClone;
    }

    private bool _cancelar = true;

    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = SenasParticularesDictionary.GetSenaParticular(this);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);

        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);

            if (dialogo.Confirmacion == "Guardar") confirmacion = true;
            else if (dialogo.Confirmacion == "No guardar") return _cancelar = false;
        }
        else confirmacion = true;

        return confirmacion;
    }

    [RelayCommand]
    private void OnSubirImagenSenaParticular()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            Multiselect = false,
            Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.webp"
        };

        if (openFileDialog.ShowDialog() == false) return;
        if (!File.Exists(openFileDialog.FileName)) return;

        SenaParticular.Imagen = new BitmapImage(new Uri(openFileDialog.FileName));
    }

    [RelayCommand]
    private async Task OnGuardarYContinuar(Type pageType)
    {
        if (!await EnlistarCampos())
        {
            if (!_cancelar) _navigationService.Navigate(pageType);

            return;
        }

        await _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}