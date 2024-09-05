using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionViewModel : ObservableObject
{
    // Servicios
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private static IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido;
    [ObservableProperty] private MediaFiliacion _mediaFiliacion;
    
    // Catalogos
    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPieles = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _formasCaras = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _formasOjos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOjos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _calvicies = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabellos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabellos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabellos = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposNarices = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosBocas = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosLabios = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOrejas = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _formasOrejas = [];
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCejas = [];
    [ObservableProperty] private ObservableCollection<string> _opciones = ["Si", "No", "No Especifica"]; 

    public MediaFiliacionViewModel()
    {
        InitAsync();
    }

    private async Task InitAsync()
    {
        await CargarCatalogos();
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any())
        {
            Reporte.Desaparecidos.Add(new Desaparecido());
        }
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Desaparecido.Persona.MediaFiliacion ??= new MediaFiliacion();
        MediaFiliacion = Desaparecido.Persona.MediaFiliacion;
    }

    private async Task CargarCatalogos()
    {
        Complexiones = await CebvNetwork.GetCatalogo("complexiones");
        ColoresPieles = await CebvNetwork.GetCatalogo("colores-pieles");
        FormasCaras = await CebvNetwork.GetCatalogo("formas-caras");
        ColoresOjos = await CebvNetwork.GetCatalogo("colores-ojos");
        FormasOjos = await CebvNetwork.GetCatalogo("formas-ojos");
        TamanosOjos = await CebvNetwork.GetCatalogo("tamanos-ojos");
        Calvicies = await CebvNetwork.GetCatalogo("tipos-calvicies");
        ColoresCabellos = await CebvNetwork.GetCatalogo("colores-cabellos");
        TamanosCabellos = await CebvNetwork.GetCatalogo("tamanos-cabellos");
        TiposCabellos = await CebvNetwork.GetCatalogo("tipos-cabellos");
        TiposCejas = await CebvNetwork.GetCatalogo("tipos-cejas");
        TiposNarices = await CebvNetwork.GetCatalogo("tipos-narices");
        TamanosBocas = await CebvNetwork.GetCatalogo("tamanos-bocas");
        TamanosOrejas = await CebvNetwork.GetCatalogo("tamanos-orejas");
        FormasOrejas = await CebvNetwork.GetCatalogo("formas-orejas");
        TamanosLabios = await CebvNetwork.GetCatalogo("tamanos-labios");
    }

    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        await _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}