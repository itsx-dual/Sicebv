using static Cebv.core.data.OpcionesCebv;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.intrumentos_juridicos.data;
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
        if (Reporte.Desaparecidos.Count == 0) Reporte.Desaparecidos.Add(Desaparecido);
        else Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;


        CarpetaInvestigacion = Reporte.Desaparecidos[0].DocumentosLegales?.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.CarpetaInvestigacion.ToString()
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.CarpetaInvestigacion.ToString(),
            EsOficial = false
        };


        AmparoBuscador = Desaparecido.DocumentosLegales?.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.AmparoBuscador.ToString()
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.AmparoBuscador.ToString(),
            EsOficial = false
        };

        RecomedacionDerechos = Desaparecido.DocumentosLegales?.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.RecomendacionDerechos.ToString()
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.RecomendacionDerechos.ToString(),
            EsOficial = false
        };
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

    [ObservableProperty] private DocumentoLegal? _recomedacionDerechos;

    /**
     * Comando para guardar y seguir.
     */
    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        GuardarDocumentosLegales();
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }

    private void GuardarDocumentosLegales()
    {
        if (CarpetaInvestigacion is not null)
            Reporte.Desaparecidos[0].DocumentosLegales?.Add(CarpetaInvestigacion);

        if (AmparoBuscador is not null)
            Reporte.Desaparecidos[0].DocumentosLegales?.Add(AmparoBuscador);

        if (RecomedacionDerechos is not null)
            Reporte.Desaparecidos[0].DocumentosLegales?.Add(RecomedacionDerechos);
    }
}