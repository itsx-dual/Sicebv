using Cebv.core.data;
using Cebv.core.modules.desaparecido.data;
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
    public InstrumentoJuridicoViewModel()
    {
        Reporte = _reporteService.GetReporte();

        // Esta seccion del formulario lidia con cuatro atributos de desaparecido.
        if (Reporte.Desaparecidos?.Count == 0)
        {
            Reporte.Desaparecidos?.Add(new Desaparecido());
        }

        var desaparecido = Reporte.Desaparecidos[0];
        
        CarpetaInvestigacion = desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "CI")!;
        if (CarpetaInvestigacion == null)
        {
            CarpetaInvestigacion = new DocumentoLegal { TipoDocumento = "CI" };
            OpcionCarpeta = false;
        }
        else
        {
            OpcionCarpeta = true;
        }

        AmparoBuscador = desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "AB")!;
        if (AmparoBuscador == null)
        {
            AmparoBuscador = new DocumentoLegal { TipoDocumento = "AB" };
            OpcionAmparo = false;
        }
        else
        {
            OpcionAmparo = true;
        }

        RecomedacionDerechosHumanos = desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "DH")!;
        if (RecomedacionDerechosHumanos == null)
        {
            RecomedacionDerechosHumanos = new DocumentoLegal { TipoDocumento = "DH" };
            OpcionRecomendacion = false;
        }
        else
        {
            OpcionRecomendacion = true;
        }
    }

    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    // Opciones cebv.
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;
    [ObservableProperty] private Reporte _reporte;

    /**
     * Carpeta de investigación.
     */
    [ObservableProperty] private string _opcionCarpetaKey = "No";
    [ObservableProperty] private bool? _opcionCarpeta;
    [ObservableProperty] private DocumentoLegal _carpetaInvestigacion = new();

    partial void OnOpcionCarpetaChanged(bool? value)
    {
        var documentos = Reporte.Desaparecidos[0].DocumentosLegales;
        if (value != null && (bool) value)
        {
            var tiene_ci = documentos.Any(x => x.Equals(CarpetaInvestigacion));

            if (!tiene_ci)
            {
                documentos.Add(CarpetaInvestigacion);
            }
            return;
        }

        var ci = documentos.FirstOrDefault(x => x.Equals(CarpetaInvestigacion));
        documentos.Remove(ci);
    }

    /**
     * Amparo buscador.
     */
    [ObservableProperty] private string _opcionAmparoKey = "No";
    [ObservableProperty] private bool? _opcionAmparo;
    [ObservableProperty] private DocumentoLegal _amparoBuscador = new();

    partial void OnOpcionAmparoChanged(bool? value)
    {
        var documentos = Reporte.Desaparecidos[0].DocumentosLegales;
        if (value != null && (bool) value)
        {
            var tiene_amparo = documentos.Any(x => x.Equals(AmparoBuscador));

            if (!tiene_amparo)
            {
                documentos.Add(AmparoBuscador);
            }
            return;
        }

        var amparo = documentos.FirstOrDefault(x => x.Equals(AmparoBuscador));
        documentos.Remove(amparo);
    }

    /**
     * Recomendación de derechos humanos.
     */
    [ObservableProperty] private string _opcionRecomendacionKey = "No";
    [ObservableProperty] private bool? _opcionRecomendacion = false;
    [ObservableProperty] private DocumentoLegal _recomedacionDerechosHumanos;

    partial void OnOpcionRecomendacionChanged(bool? value)
    {
        var documentos = Reporte.Desaparecidos[0].DocumentosLegales;
        if (value != null && (bool) value)
        {
            var tiene_recomendacion = documentos.Any(x => x.Equals(RecomedacionDerechosHumanos));

            if (!tiene_recomendacion)
            {
                documentos.Add(RecomedacionDerechosHumanos);
            }
            return;
        }

        var recomendacion = documentos.FirstOrDefault(x => x.Equals(RecomedacionDerechosHumanos));
        documentos.Remove(recomendacion);
    }

    /**
     * Otros
     */
    [ObservableProperty] private bool _declaracionAusencia;
    [ObservableProperty] private bool _accionUrgente;
    [ObservableProperty] private bool _dictamen;
    [ObservableProperty] private bool _carpeteFederal;
    [ObservableProperty] private string _otroDerecho = String.Empty;

    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}