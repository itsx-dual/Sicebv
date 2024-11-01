using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.comunicacion_difusion.presentation;

public partial class GeneracionBoletinViewModel : ObservableObject
{
    private readonly IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private readonly IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    public GeneracionBoletinViewModel()
    {
        InitAsync();
        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
        EsMayorEdad = CalcularEdad();
    }

    private async void InitAsync()
    {
        TiposBoletines = await CebvNetwork.GetRoute<Catalogo>("tipos-boletines");
        EstatusPersonas = await CebvNetwork.GetRoute<BasicResource>("estatus-personas");
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposBoletines = new();
    [ObservableProperty] private Catalogo _tipoBoletin = new();
    [ObservableProperty] private ObservableCollection<BasicResource> _estatusPersonas = new();
    [ObservableProperty] private BasicResource _estatusPersona = new();
    
    [ObservableProperty] private bool _esMayorEdad;
    
    private bool CalcularEdad()
    {
        if (Desaparecido.Persona.FechaNacimiento is null) return false;

        return Desaparecido.Persona.FechaNacimiento.Value.AddYears(18) <= DateTime.Now;
    }

    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty] private ObservableCollection<BitmapImage> _imagenesDesaparecido = new();
    [ObservableProperty] private BitmapImage _imagenBoletin;

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
    private void OnDeleteDesaparecidoImagen(BitmapImage image)
    { 
        ImagenesDesaparecido.Remove(image);   
    }

    [RelayCommand]
    private void OnGenerarBoletin()
    {
        var url = string.Empty;
        if (TipoBoletin.Nombre == "Búsqueda Inmediata") url = $"reportes/boletines/busqueda-inmediata/{Desaparecido.Id}";
        if (url == string.Empty) return;
        
        var webview = new WebView2Window(url, "Boletin de búsqueda inmediata");
        webview.Show();
    }

    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        if (ImagenesDesaparecido.Count > 0) await ReporteServiceNetwork.SubirFotosDesaparecido(Desaparecido.Id ?? 0, ImagenesDesaparecido.ToList(), ImagenBoletin);
        await _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}