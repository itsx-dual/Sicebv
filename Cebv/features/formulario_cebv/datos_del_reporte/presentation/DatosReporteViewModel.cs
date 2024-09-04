using System.Collections.ObjectModel;
using Cebv.core.data;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.domain;
using Cebv.core.modules.ubicacion.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reporte;

    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    /**
     * Constructor de la clase.
     */
    public DatosReporteViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        var tipoMedioId = _reporteService.GetReporte().MedioConocimiento?.TipoMedio.Id;

        TiposMedios = await CebvNetwork.GetCatalogo("tipos-medios");
        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>
            ("medios", "tipo_medio_id", tipoMedioId.ToString() ?? "7");
        Estados = await UbicacionNetwork.GetEstados();
        Instituciones = await CebvNetwork.GetCatalogo("instituciones");

        Reporte = _reporteService.GetReporte();
        TipoMedio = Reporte.MedioConocimiento?.TipoMedio!;

        // Esta seccion del formulario lidia con dos atributos de reportante.
        if (Reporte.Reportantes.Count == 0)
        {
            Reporte.Reportantes.Add(new Reportante()
            {
                InformacionExclusivaBusqueda = false,
                PublicacionRegistroNacional = false
            });
        }

        // Información exclusiva de búsqueda.
        InformacionExclusivaBusquedaOpcion =
            MappingToString(Reporte.Reportantes[0].InformacionExclusivaBusqueda);

        // Publicación de información.
        PublicacionInformacionOpcion =
            MappingToString(Reporte.Reportantes[0].PublicacionRegistroNacional);
    }

    /**
     * Fuente de información.
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();

    [ObservableProperty] private Catalogo? _tipoMedio;

    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();

    [ObservableProperty] private ObservableCollection<Estado> _estados;

    [ObservableProperty] private ObservableCollection<Catalogo> _instituciones = new();

    /**
     * Información de consentimiento.
     */
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;


    /**
     * Información exclusiva de búsqueda.
     */
    [ObservableProperty] private string _informacionExclusivaBusquedaOpcion = No;

    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;

    partial void OnInformacionExclusivaBusquedaOpcionChanged(string value)
    {
        InformacionExclusivaBusqueda = MappingToBool(value);
        Reporte.Reportantes[0].InformacionExclusivaBusqueda = InformacionExclusivaBusqueda;
    }

    /**
     * Publicación de información en el registro nacional.
     */
    [ObservableProperty] private string _publicacionInformacionOpcion = No;

    [ObservableProperty] private bool? _publicacionInformacion = false;

    partial void OnPublicacionInformacionOpcionChanged(string value)
    {
        PublicacionInformacion = MappingToBool(value);
        Reporte.Reportantes[0].PublicacionRegistroNacional = PublicacionInformacion;
    }

    async partial void OnTipoMedioChanged(Catalogo? value)
    {
        if (value?.Id is null) return;

        Medios = await CebvNetwork.GetByFilter<MedioConocimiento>
            ("medios", "tipo_medio_id", value.Id.ToString()!);
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}