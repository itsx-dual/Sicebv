using Cebv.core.data;
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
    
    // Catalogos
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;
    
    // Valores seleccionados
    [ObservableProperty] private bool? _opcionCarpeta;
    [ObservableProperty] private DocumentoLegal _carpetaInvestigacion = new();
    
    [ObservableProperty] private bool? _opcionAmparo;
    [ObservableProperty] private DocumentoLegal _amparoBuscador = new();
    
    [ObservableProperty] private bool? _opcionRecomendacion = false;
    [ObservableProperty] private DocumentoLegal _recomedacionDerechosHumanos;
    
    public InstrumentoJuridicoViewModel()
    {
        Reporte = _reporteService.GetReporte();
        
        if (!Reporte.Desaparecidos.Any())
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos?.Add(Desaparecido);
        }
        Desaparecido = Reporte.Desaparecidos?.FirstOrDefault()!;
        
        CarpetaInvestigacion = Desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "CI")!;
        if (CarpetaInvestigacion == null)
        {
            CarpetaInvestigacion = new DocumentoLegal { TipoDocumento = "CI" };
            OpcionCarpeta = false;
        }
        else
        {
            OpcionCarpeta = true;
        }

        AmparoBuscador = Desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "AB")!;
        if (AmparoBuscador == null)
        {
            AmparoBuscador = new DocumentoLegal { TipoDocumento = "AB" };
            OpcionAmparo = false;
        }
        else
        {
            OpcionAmparo = true;
        }

        RecomedacionDerechosHumanos = Desaparecido.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "DH")!;
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
    
    [ObservableProperty] private string _opcionCarpetaKey = "No";

    partial void OnOpcionCarpetaChanged(bool? value)
    {
        var documentos = Desaparecido.DocumentosLegales;
        if (value ?? false)
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
    

    partial void OnOpcionAmparoChanged(bool? value)
    {
        var documentos = Desaparecido.DocumentosLegales;
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

    partial void OnOpcionRecomendacionChanged(bool? value)
    {
        var documentos = Desaparecido.DocumentosLegales;
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

    [RelayCommand]
    public void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}