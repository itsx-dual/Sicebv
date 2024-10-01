using System.Collections.ObjectModel;
using static Cebv.core.data.OpcionesCebv;
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
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    /**
     * Constructor de la clase
     */
    public MediaFiliacionViewModel()
    {
        IniAsync();

        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.First();


        Desaparecido.Persona.Salud ??= new();
        Desaparecido.Persona.Ojos ??= new();
        Desaparecido.Persona.Cabello ??= new();
        Desaparecido.Persona.VelloFacial ??= new();
        Desaparecido.Persona.Nariz ??= new();
        Desaparecido.Persona.Boca ??= new();
        Desaparecido.Persona.Orejas ??= new();
    }

    // Perfil corporal
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
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

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
    private async void IniAsync()
    {
        Complexiones = await CebvNetwork.GetRoute<Catalogo>("complexiones");
        ColoresPieles = await CebvNetwork.GetRoute<Catalogo>("colores-piel");
        FormasCaras = await CebvNetwork.GetRoute<Catalogo>("formas-cara");
        ColoresOjos = await CebvNetwork.GetRoute<Catalogo>("colores-ojos");
        FormasOjos = await CebvNetwork.GetRoute<Catalogo>("formas-ojos");
        TamanosOjos = await CebvNetwork.GetRoute<Catalogo>("tamanos-ojos");
        Calvicies = await CebvNetwork.GetRoute<Catalogo>("tipos-calvicie");
        ColoresCabellos = await CebvNetwork.GetRoute<Catalogo>("colores-cabello");
        TamanosCabellos = await CebvNetwork.GetRoute<Catalogo>("tamanos-cabello");
        TiposCabellos = await CebvNetwork.GetRoute<Catalogo>("tipos-cabello");
        TiposCejas = await CebvNetwork.GetRoute<Catalogo>("tipos-cejas");
        TiposNarices = await CebvNetwork.GetRoute<Catalogo>("tipos-nariz");
        TamanosBocas = await CebvNetwork.GetRoute<Catalogo>("tamanos-boca");
        TamanosLabios = await CebvNetwork.GetRoute<Catalogo>("tamanos-labios");
        TamanosOrejas = await CebvNetwork.GetRoute<Catalogo>("tamanos-orejas");
        FormasOrejas = await CebvNetwork.GetRoute<Catalogo>("formas-orejas");
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}