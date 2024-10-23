using Cebv.core.util;
using static Cebv.core.util.CollectionsHelper;
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
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    /// <summary>
    /// Clase instanciada para acceder a los parametros por defecto.
    /// </summary>
    [ObservableProperty] private DocumentoLegal _p = new();

    [ObservableProperty] private DocumentoLegal? _carpetaInvestigacion;

    [ObservableProperty] private DocumentoLegal? _amparoBuscador;

    [ObservableProperty] private DocumentoLegal? _recomedacionDerechos;

    public InstrumentoJuridicoViewModel()
    {
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        CarpetaInvestigacion = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.CarpetaInvestigacion);

        AmparoBuscador = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.AmparoBuscador);

        RecomedacionDerechos = Desaparecido.DocumentosLegales.FirstOrDefault(
            x => x.TipoDocumento == TipoDocumentoLegal.RecomendacionDerechos);

        EnsureObjectExists(ref _carpetaInvestigacion, Desaparecido.DocumentosLegales, P.ParametrosCarpeta);
        EnsureObjectExists(ref _amparoBuscador, Desaparecido.DocumentosLegales, P.ParametrosAmparo);
        EnsureObjectExists(ref _recomedacionDerechos, Desaparecido.DocumentosLegales, P.ParametrosRecomendacion);
    }
    
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion;

        var properties = ListEmptyElements.GetInstrumentoJuridico(CarpetaInvestigacion, AmparoBuscador, RecomedacionDerechos, Desaparecido);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            confirmacion = dialogo.Confirmacion;
        }
        else confirmacion = true;

        return confirmacion;
    }

    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        if (!await EnlistarCampos())
            return;
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}