using Cebv.core.data;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoViewModel : ObservableObject
{
    public IFormularioCebvNavigationService FormularioNavigationService { get; set; }
    public IReporteService ReporteService { get; set; }

    // Opciones cebv.
    [ObservableProperty] private Dictionary<string, bool?> _opciones = OpcionesCebv.Ops;

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

    public InstrumentoJuridicoViewModel(IFormularioCebvNavigationService formularioNavigationService, IReporteService reporteService)
    {
        ReporteService = reporteService;
        FormularioNavigationService = formularioNavigationService;
        
        if (!ReporteService.HayReporte()) return;
        
        var reporte = ReporteService.GetReporteActual();
        var desaparecido = reporte.Desaparecidos?.FirstOrDefault();

        if (desaparecido == null) return;
        DeclaracionAusencia = (bool) desaparecido.DeclaracionAusencia;
        AccionUrgente = (bool) desaparecido.AccionUrgente;
        Dictamen = (bool) desaparecido.Dictamen;
        CarpeteFederal = (bool) desaparecido.CarpetaFederal;
        OtroDerecho = desaparecido.OtroDerecho;
        
        if (desaparecido.DocumentosLegales == null || desaparecido.DocumentosLegales.Count == 0) return;
        var carpetaInvestigacion = desaparecido.DocumentosLegales.FirstOrDefault(x => x.TipoDocumento == "CI");
        var amparo = desaparecido.DocumentosLegales.FirstOrDefault(x => x.TipoDocumento == "AB");
        var recomendacion = desaparecido.DocumentosLegales.FirstOrDefault(x => x.TipoDocumento == "DH");
            

        if (null != carpetaInvestigacion)
        {
            OpcionCarpeta = true;
            NumeroCarpeta = carpetaInvestigacion.NumeroDocumento;
            DondeRadicaCarpeta = carpetaInvestigacion.DondeRadica;
            NombreServidorPublicoCarpeta = carpetaInvestigacion.NombreServidorPublico;
            FechaRecepcionCarpeta = carpetaInvestigacion.FechaRecepcion;
        }

        if (null != amparo)
        {
            OpcionAmparo = true;
            NumeroAmparo = amparo.NumeroDocumento;
            DondeRadicaAmparo = amparo.DondeRadica;
            NombreServidorPublicoAmparo = amparo.NombreServidorPublico;
            FechaRecepcionAmparo = amparo.FechaRecepcion;
        }

        if (null != recomendacion)
        {
            OpcionRecomendacion = true;
            NumeroRecomendacion = recomendacion.NumeroDocumento;
            DondeRadicaRecomendacion = recomendacion.DondeRadica;
            NombreServidorPublicoRecomendacion = recomendacion.NombreServidorPublico;
            FechaRecepcionRecomendacion = recomendacion.FechaRecepcion;
        }
    }

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
            
            DeclaracionAusencia = DeclaracionAusencia,
            AccionUrgente = AccionUrgente,
            Dictamen = Dictamen,
            CarpetaFederal = CarpeteFederal,
            OtroDerecho = OtroDerecho
        };

        if (ReporteService.SendInformacionInstrumentoJuridico(informacion)) FormularioNavigationService.Navigate(pageType);
    }
}