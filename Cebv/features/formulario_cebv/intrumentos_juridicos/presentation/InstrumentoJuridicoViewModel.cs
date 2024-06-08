using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoViewModel : ObservableObject
{
    // Opciones cebv.
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    /**
     * Carpeta de investigación.
     */
    [ObservableProperty] private string _opcionCarpetaCebv = OpcionesCebv.No;

    [ObservableProperty] private bool? _opcionCarpeta = false;

    [ObservableProperty] private string _numeroCarpeta = String.Empty;
    [ObservableProperty] private string _dondeRadicaCarpeta = String.Empty;
    [ObservableProperty] private string _nombreServidorPublicoCarpeta = String.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionCarpeta;

    /**
     * Amparo buscador.
     */
    [ObservableProperty] private string _opcionAmparoCebv = OpcionesCebv.No;

    [ObservableProperty] private bool? _opcionAmparo = false;
    [ObservableProperty] private string _numeroAmparo = String.Empty;
    [ObservableProperty] private string _dondeRadicaAmparo = String.Empty;
    [ObservableProperty] private string _nombreServidorPublicoAmparo = String.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionAmparo;

    /**
     * Recomendación de derechos humanos.
     */
    [ObservableProperty] private string _opcionRecomendacionCebv = OpcionesCebv.No;

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


    /**
     * Mapeo de valores string a boolean.
     */
    partial void OnOpcionCarpetaCebvChanged(string value) =>
        OpcionCarpeta = OpcionesCebv.MappingToBool(value);

    partial void OnOpcionAmparoCebvChanged(string value) =>
        OpcionAmparo = OpcionesCebv.MappingToBool(value);

    partial void OnOpcionRecomendacionCebvChanged(string value) =>
        OpcionRecomendacion = OpcionesCebv.MappingToBool(value);
}