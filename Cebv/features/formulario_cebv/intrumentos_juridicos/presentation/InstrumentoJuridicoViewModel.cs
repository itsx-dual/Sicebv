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
        if (Reporte.Desaparecidos.Count <= 0) Reporte.Desaparecidos.Add(Desaparecido);
        else Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;


        CarpetaInvestigacion = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.CarpetaInvestigacion
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.CarpetaInvestigacion,
            EsOficial = false
        };


        AmparoBuscador = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.AmparoBuscador
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.AmparoBuscador,
            EsOficial = false
        };

        RecomendacionDerechos = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.RecomendacionDerechos
        ) ?? new DocumentoLegal
        {
            TipoDocumento = TipoDocumentoLegal.RecomendacionDerechos,
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

    [ObservableProperty] private DocumentoLegal? _recomendacionDerechos;

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
        // Guardar documentos legales.
        // Si el documento legal no existe, se agrega a la lista de documentos legales.
        // Cuando el documento legal no existe su indice es -1.
        // Si el documento legal existe, se actualiza en la lista de documentos legales.
        
        var carpetaIndex = Reporte.Desaparecidos[0].DocumentosLegales.IndexOf(CarpetaInvestigacion!);
        var amparoIndex = Reporte.Desaparecidos[0].DocumentosLegales.IndexOf(AmparoBuscador!);
        var recomendacionIndex = Reporte.Desaparecidos[0].DocumentosLegales.IndexOf(RecomendacionDerechos!);

        if (carpetaIndex == -1) Reporte.Desaparecidos[0].DocumentosLegales.Add(CarpetaInvestigacion!);
        else Reporte.Desaparecidos[0].DocumentosLegales[carpetaIndex] = CarpetaInvestigacion!;

        if (amparoIndex == -1) Reporte.Desaparecidos[0].DocumentosLegales.Add(AmparoBuscador!);
        else Reporte.Desaparecidos[0].DocumentosLegales[amparoIndex] = AmparoBuscador!;

        if (recomendacionIndex == -1) Reporte.Desaparecidos[0].DocumentosLegales.Add(RecomendacionDerechos!);
        else Reporte.Desaparecidos[0].DocumentosLegales[recomendacionIndex] = RecomendacionDerechos!;
    }
}