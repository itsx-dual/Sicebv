using Cebv.core.data;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoViewModel : ObservableObject
{
    public InstrumentoJuridicoViewModel()
    {
        Reporte = _reporteService.GetReporteActual();
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
    [ObservableProperty] private bool? _opcionCarpeta = false;
    [ObservableProperty] private string _numeroCarpeta = String.Empty;
    [ObservableProperty] private string _dondeRadicaCarpeta = String.Empty;
    [ObservableProperty] private string _nombreServidorPublicoCarpeta = String.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionCarpeta;

    /**
     * Amparo buscador.
     */
    [ObservableProperty] private string _opcionAmparoKey = "No";
    [ObservableProperty] private bool? _opcionAmparo = false;
    [ObservableProperty] private string _numeroAmparo = String.Empty;
    [ObservableProperty] private string _dondeRadicaAmparo = String.Empty;
    [ObservableProperty] private string _nombreServidorPublicoAmparo = String.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionAmparo;

    /**
     * Recomendación de derechos humanos.
     */
    [ObservableProperty] private string _opcionRecomendacionKey = "No";
    [ObservableProperty] private bool? _opcionRecomendacion = false;
    [ObservableProperty] private string _numeroRecomendacion = String.Empty;
    [ObservableProperty] private string _dondeRadicaRecomendacion = String.Empty;
    [ObservableProperty] private string _nombreServidorPublicoRecomendacion = String.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionRecomendacion;

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
        var informacion = new InstrumentoJuridicoPostObject
        {
            NumeroCarpeta = NumeroCarpeta,
            DondeRadicaCarpeta = DondeRadicaCarpeta,
            ServidorPublicoCarpeta = NombreServidorPublicoCarpeta,
            FechaRecepcionCarpeta = FechaRecepcionCarpeta,
            
            NumeroAmparo = NumeroAmparo,
            DondeRadicaAmparo = DondeRadicaAmparo,
            ServidorPublicoAmparo =  NombreServidorPublicoAmparo,
            FechaRecepcionAmparo = FechaRecepcionAmparo,
            
            NumeroRecomendacion = NumeroRecomendacion,
            DondeRadicaRecomendacion = DondeRadicaRecomendacion,
            ServidorPublicoRecomendacion = NombreServidorPublicoRecomendacion,
            FechaRecepcionRecomendacion = FechaRecepcionRecomendacion,
            
            DeclaracionAusencia = (bool) Reporte.Desaparecidos[0].DeclaracionEspecialAusencia,
            AccionUrgente = (bool) Reporte.Desaparecidos[0].AccionUrgente,
            Dictamen = (bool) Reporte.Desaparecidos[0].Dictamen,
            CarpetaFederal = (bool) Reporte.Desaparecidos[0].CiNivelFederal,
            OtroDerecho = Reporte.Desaparecidos[0].OtroDerechoHumano
        };

        if (_reporteService.SendInformacionInstrumentoJuridico(informacion)) _navigationService.Navigate(pageType);
    }
}