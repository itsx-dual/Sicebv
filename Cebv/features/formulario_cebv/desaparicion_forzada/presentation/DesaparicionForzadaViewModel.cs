using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.desaparicion_forzada.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

public partial class DesaparicionForzadaViewModel : ObservableObject
{
    private IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    /**
     * Constructor de la clase
     */
    public DesaparicionForzadaViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    // Autoridad
    [ObservableProperty] private string _sufrioDesaparicionForzadaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sufrioDesaparicionForzada = false;

    partial void OnSufrioDesaparicionForzadaOpcionChanged(string value) =>
        SufrioDesaparicionForzada = OpcionesCebv.MappingToBool(value);

    [ObservableProperty] private ObservableCollection<Catalogo> _autoridades = new();
    [ObservableProperty] private Catalogo _autoridad = new();

    [ObservableProperty] private string _autoriddadDescripcion = string.Empty;

    // Particular
    [ObservableProperty] private string _sufrioDesaparicionParticularOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sufrioDesaparicionParticular = false;

    partial void OnSufrioDesaparicionParticularOpcionChanged(string value) =>
        SufrioDesaparicionParticular = OpcionesCebv.MappingToBool(value);

    [ObservableProperty] private ObservableCollection<Catalogo> _particulares = new();
    [ObservableProperty] private Catalogo _particular = new();

    [ObservableProperty] private string _particularDescripcion = string.Empty;

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _metodosCaptura = new();
    [ObservableProperty] private Catalogo _metodoCaptura = new();
    [ObservableProperty] private string _metodoCapturaDescripcion = string.Empty;

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _mediosCaptura = new();
    [ObservableProperty] private Catalogo _medioCaptura = new();
    [ObservableProperty] private string _medioCapturaDescripcion = string.Empty;

    // Detencion legal previa
    [ObservableProperty] private string _detencionLegalExtorsionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _detencionLegalExtorsion = false;

    partial void OnDetencionLegalExtorsionOpcionChanged(string value) =>
        DetencionLegalExtorsion = OpcionesCebv.MappingToBool(value);

    [ObservableProperty] private string _detencionLegalExtorsionDescripcion = String.Empty;

    // Ha sido avistado previamente
    [ObservableProperty] private string _haSidoAvistadoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _haSidoAvistado = false;

    partial void OnHaSidoAvistadoOpcionChanged(string value) =>
        HaSidoAvistado = OpcionesCebv.MappingToBool(value);

    [ObservableProperty] private string _haSidoAvistadoDescripcion = String.Empty;

    // Información sobre el perpetrador
    [ObservableProperty] private PerpetradorViewModel _perpetrador = new();
    
    [ObservableProperty] private string _grupoPerpetradorDescripcion = String.Empty;

    [ObservableProperty] private string _delitoDesaparicionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _delitoDesaparicion = false;

    [ObservableProperty] private string _delitoDesaparicionDescripcion = String.Empty;


    /**
     * Permite cargar los catalogos necesarios para el formulario
     */
    private async void CargarCatalogos()
    {
        Autoridades = await DesaparicionForzadaNetwork.GetAutoridades();
        Particulares = await DesaparicionForzadaNetwork.GetParticulares();
        MetodosCaptura = await DesaparicionForzadaNetwork.GetMetodosCaptura();
        MediosCaptura = await DesaparicionForzadaNetwork.GetMediosCaptura();
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}