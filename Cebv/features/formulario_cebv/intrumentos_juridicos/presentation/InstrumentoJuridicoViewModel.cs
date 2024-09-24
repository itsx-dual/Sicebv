using Cebv.core.util.enums;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using DocumentoLegal = Cebv.core.util.reporte.viewmodels.DocumentoLegal;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reporte = null!;

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    /**
     * Constructor de la clase.
     */
    public InstrumentoJuridicoViewModel()
    {
        LoadAsync();
    }

    private void LoadAsync()
    {
        Reporte = _reporteService.GetReporte();

        // Esta seccion del formulario lidia con cuatro atributos de desaparecido.
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        CarpetaInvestigacion = Desaparecido.DocumentosLegales.FirstOrDefault(x => x.TipoDocumento == TipoDocumentoLegal.CarpetaInvestigacion);

        if (CarpetaInvestigacion is null)
        {
            var carpeta = new DocumentoLegal
            {
                TipoDocumento = TipoDocumentoLegal.CarpetaInvestigacion,
                EsOficial = false
            };
            CarpetaInvestigacion = carpeta;
            Desaparecido.DocumentosLegales.Add(CarpetaInvestigacion);
        }

        AmparoBuscador = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.AmparoBuscador
        );

        if (AmparoBuscador is null)
        {
            var carpeta = new DocumentoLegal
            {
                TipoDocumento = TipoDocumentoLegal.AmparoBuscador,
                EsOficial = false
            };
            AmparoBuscador = carpeta;
            Desaparecido.DocumentosLegales.Add(AmparoBuscador);
        }

        RecomendacionDerechos = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.RecomendacionDerechos
        );

        if (RecomendacionDerechos is null)
        {
            var carpeta = new DocumentoLegal
            {
                TipoDocumento = TipoDocumentoLegal.RecomendacionDerechos,
                EsOficial = false
            };
            RecomendacionDerechos = carpeta;
            Desaparecido.DocumentosLegales.Add(RecomendacionDerechos);
        }
    }

    // Opciones cebv.
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    // Desaparecido.
    [ObservableProperty] private Desaparecido _desaparecido = new();

    /**
     * Carpeta de investigación.
     */
    [ObservableProperty] private string _carpeta = No;

    [ObservableProperty] private DocumentoLegal? _carpetaInvestigacion;

    /**
     * Amparo buscador.
     */
    [ObservableProperty] private string _amparo = No;

    [ObservableProperty] private DocumentoLegal? _amparoBuscador;

    /**
     * Recomendación de derechos humanos.
     */
    [ObservableProperty] private string _recomendacion = No;

    [ObservableProperty] private DocumentoLegal? _recomendacionDerechos;

    /**
     * Comando para guardar y seguir.
     */
    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}