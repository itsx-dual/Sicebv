using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionViewModel : ObservableObject
{
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;

    /**
     * Constructor de la clase
     */
    public MediaFiliacionViewModel()
    {
        LoadAsync();
    }

    /**
     * Variables de la clase
     */
    // Perfil corporal
    [ObservableProperty] private float _estatura;

    [ObservableProperty] private float _peso;

    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _coloresPieles = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasCaras = new();

    // Ojos
    [ObservableProperty] private ObservableCollection<Catalogo> _coloresOjos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasOjos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOjos = new();


    // Cabello
    [ObservableProperty] private ObservableCollection<Catalogo> _calvicies = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _coloresCabellos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosCabellos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCabellos = new();

    // Vello facial
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposCejas = new();

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _bigoteOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _bigote = false;

    partial void OnBigoteOpcionChanged(string value)
    {
        if (Reporte.Desaparecidos.Count > 0)
            Reporte.Desaparecidos[0].Persona!.VelloFacial!.TieneBigote = OpcionesCebv.MappingToBool(value);
    }

    [ObservableProperty] private string _barbaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _barba = false;

    partial void OnBarbaOpcionChanged(string value)
    {
        if (Reporte.Desaparecidos.Count > 0)
            Reporte.Desaparecidos[0].Persona!.VelloFacial!.TieneBarba = OpcionesCebv.MappingToBool(value);
    }

    // Nariz
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposNarices = new();
    [ObservableProperty] private Catalogo _tipoNariz = new();

    // Boca
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosBocas = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosLabios = new();

    // Orejas
    [ObservableProperty] private ObservableCollection<Catalogo> _tamanosOrejas = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _formasOrejas = new();

    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void LoadAsync()
    {
        Complexiones = await CebvNetwork.GetCatalogo("complexiones");
        ColoresPieles = await CebvNetwork.GetCatalogo("colores-piel");
        FormasCaras = await CebvNetwork.GetCatalogo("formas-cara");
        ColoresOjos = await CebvNetwork.GetCatalogo("colores-ojos");
        FormasOjos = await CebvNetwork.GetCatalogo("formas-ojos");
        TamanosOjos = await CebvNetwork.GetCatalogo("tamanos-ojos");
        Calvicies = await CebvNetwork.GetCatalogo("tipos-calvicie");
        ColoresCabellos = await CebvNetwork.GetCatalogo("colores-cabello");
        TamanosCabellos = await CebvNetwork.GetCatalogo("tamanos-cabello");
        TiposCabellos = await CebvNetwork.GetCatalogo("tipos-cabello");
        TiposCejas = await CebvNetwork.GetCatalogo("tipos-cejas");
        TiposNarices = await CebvNetwork.GetCatalogo("tipos-nariz");
        TamanosBocas = await CebvNetwork.GetCatalogo("tamanos-boca");
        TamanosLabios = await CebvNetwork.GetCatalogo("tamanos-labios");
        TamanosOrejas = await CebvNetwork.GetCatalogo("tamanos-orejas");
        FormasOrejas = await CebvNetwork.GetCatalogo("formas-orejas");

        Reporte = _reporteService.GetReporte();

        var @default = Reporte.Desaparecidos.FirstOrDefault();

        @default!.Persona!.Salud ??= new();
        @default.Persona!.Ojos ??= new();
        @default.Persona!.Cabello ??= new();
        @default.Persona!.VelloFacial ??= new();
        @default.Persona!.Nariz ??= new();
        @default.Persona!.Boca ??= new();
        @default.Persona!.Orejas ??= new();

        if (@default.Persona.VelloFacial is null) return;
        
        BigoteOpcion = OpcionesCebv.MappingToString(Reporte.Desaparecidos[0].Persona!.VelloFacial!.TieneBigote);
        BarbaOpcion = OpcionesCebv.MappingToString(Reporte.Desaparecidos[0].Persona!.VelloFacial!.TieneBigote);
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}