using Cebv.core.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.intrumentos_juridicos.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.data.OpcionesCebv;
using DocumentoLegal = Cebv.core.util.reporte.viewmodels.DocumentoLegal;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reporte;

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    /**
     * Constructor de la clase.
     */
    public InstrumentoJuridicoViewModel()
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
        // Carpeta de investigación.
        if (CarpetaInvestigacion is not null)
            CarpetaOpcion = MappingToString(CarpetaInvestigacion.EsOficial);

        // Amparo buscador.
        if (AmparoBuscador is not null)
            AmparoOpcion = MappingToString(AmparoBuscador.EsOficial);

        // Recomendación de derechos humanos.
        if (RecomedacionDerechos is not null)
            RecomendacionOpcion = MappingToString(RecomedacionDerechos.EsOficial);
    }

    // Opciones cebv.
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    // Desaparecido.
    [ObservableProperty] private Desaparecido _desaparecido = new();

    /**
     * Carpeta de investigación.
     */
    [ObservableProperty] private string _carpetaOpcion = No;

    [ObservableProperty] private DocumentoLegal? _carpetaInvestigacion;

    partial void OnCarpetaOpcionChanged(string value)
    {
        if (CarpetaInvestigacion is not null) CarpetaInvestigacion.EsOficial = MappingToBool(value);
    }

    /**
     * Amparo buscador.
     */
    [ObservableProperty] private string _amparoOpcion = No;

    [ObservableProperty] private DocumentoLegal? _amparoBuscador;

    partial void OnAmparoOpcionChanged(string value)
    {
        if (AmparoBuscador is not null) AmparoBuscador.EsOficial = MappingToBool(value);
    }

    /**
     * Recomendación de derechos humanos.
     */
    [ObservableProperty] private string _recomendacionOpcion = No;

    [ObservableProperty] private DocumentoLegal? _recomedacionDerechos;

    partial void OnRecomendacionOpcionChanged(string value)
    {
        if (RecomedacionDerechos is not null) RecomedacionDerechos.EsOficial = MappingToBool(value);
    }


    /**
     * Comando para guardar y seguir.
     */
    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
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