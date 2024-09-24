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
    // Referente a servicios
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido;

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    // Valores seleccionados
    [ObservableProperty] private DocumentoLegal? _carpetaInvestigacion;

    [ObservableProperty] private DocumentoLegal? _amparoBuscador;

    [ObservableProperty] private DocumentoLegal? _recomedacionDerechosHumanos;

    public InstrumentoJuridicoViewModel()
    {
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any())
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        CarpetaInvestigacion = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.CarpetaInvestigacion);

        AmparoBuscador = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.AmparoBuscador);

        RecomedacionDerechosHumanos = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.RecomendacionDerechos);

        EnsureDocumentExists(ref _carpetaInvestigacion, TipoDocumentoLegal.CarpetaInvestigacion);
        EnsureDocumentExists(ref _amparoBuscador, TipoDocumentoLegal.AmparoBuscador);
        EnsureDocumentExists(ref _recomedacionDerechosHumanos, TipoDocumentoLegal.RecomendacionDerechos);
    }

    private void EnsureDocumentExists(ref DocumentoLegal? document, string documentType, bool? officialness = false)
    {
        if (document is not null) return;

        var newDocument = new DocumentoLegal
            { TipoDocumento = documentType, EsOficial = officialness };

        document = newDocument;

        Desaparecido.DocumentosLegales.Add(document);
    }

    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}