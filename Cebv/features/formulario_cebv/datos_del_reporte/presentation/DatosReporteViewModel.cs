using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.desaparecido.data;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReporteViewModel : ObservableObject
{
    // Información del reporte.
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes = new();
    [ObservableProperty] private Catalogo _tipoReporte = new();

    // Fuente de información.
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private Catalogo _tipoMedio = new();
    [ObservableProperty] private ObservableCollection<Medio> _medios = new();
    [ObservableProperty] private Medio _medio = new();
    [ObservableProperty] private string _dependenciaOrigen = string.Empty;
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private Estado _estado = new();

    // Información del RNPDNO.
    [ObservableProperty] private string _folioFub = string.Empty;
    [ObservableProperty] private string _autoridadIngresafolioFub = string.Empty;

    // Información de consentimiento.
    [ObservableProperty] private List<string> _informacionConsentimientoList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _informacionConsentimientoSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionConsentimiento;

    [ObservableProperty] private List<string> _informacionExclusivaBusquedaList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _informacionExclusivaBusquedaSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;

    [ObservableProperty] private List<string> _publicacionInformacionList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _publicacionInformacionSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _publicacionInformacion = false;

    // Carpeta de investigación.
    [ObservableProperty] private List<string> _carpetaInvestigacionList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _carpetaInvestigacionSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _carpetaInvestigacion = false;

    [ObservableProperty] private string _numeroCi = string.Empty;
    [ObservableProperty] private string _dondeRadicaCi = string.Empty;
    [ObservableProperty] private string _nombreServidorPublicoCi = string.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionCi;

    // Amparo de buscador.
    [ObservableProperty] private List<string> _amparoBuscadorList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _amparoBuscadorSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _amparoBuscador = false;

    [ObservableProperty] private string _numeroAmparo = string.Empty;
    [ObservableProperty] private string _dondeRadicaAmparo = string.Empty;
    [ObservableProperty] private string _nombreJuez = string.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionAb;

    // Recomendación de derechos humanos.
    [ObservableProperty] private List<string> _derechosHumanosList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _derechosHumanosSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _derechosHumanos = false;

    [ObservableProperty] private string _numeroDerechosHumanos = string.Empty;
    [ObservableProperty] private string _dondeRadicaDh = string.Empty;
    [ObservableProperty] private string _nombreServidorPublicoDh = string.Empty;
    [ObservableProperty] private DateTime? _fechaRecepcionDh;

    [ObservableProperty] private bool _declaracionEspecialAusencia;
    [ObservableProperty] private bool _accionUrgente;
    [ObservableProperty] private bool _dictamen;
    [ObservableProperty] private bool _carpetaInvestigacionNivelFederal;
    [ObservableProperty] private bool? _otroDerechoHumano = false;
    [ObservableProperty] private string _otroDerechoHumanoEspecificado = string.Empty;

    /**
     * Constructor de la clase.
     */
    public DatosReporteViewModel()
    {
        CargarCatalogos();
    }

    /**
     *  Mapeo de los valores string a boolean.
     */
    partial void OnInformacionConsentimientoSelectedChanged(string value) =>
        InformacionConsentimiento = OpcionesCebv.MappingToBool(value);

    partial void OnInformacionExclusivaBusquedaSelectedChanged(string value) =>
        InformacionExclusivaBusqueda = OpcionesCebv.MappingToBool(value);

    partial void OnPublicacionInformacionSelectedChanged(string value) =>
        PublicacionInformacion = OpcionesCebv.MappingToBool(value);

    partial void OnCarpetaInvestigacionSelectedChanged(string value) =>
        CarpetaInvestigacion = OpcionesCebv.MappingToBool(value);

    partial void OnAmparoBuscadorSelectedChanged(string value) =>
        AmparoBuscador = OpcionesCebv.MappingToBool(value);

    partial void OnDerechosHumanosSelectedChanged(string value) =>
        DerechosHumanos = OpcionesCebv.MappingToBool(value);

    /**
     * Comandos de la vista.
     */
    [RelayCommand]
    public void GuardarReporte()
    {
        ReporteRequest reporte = new()
        {
            TipoReporte = TipoReporte,
            MedioConocimiento = Medio,
            Estado = Estado,
        };

        WeakReferenceMessenger.Default.Send(new AddReporteMessage(reporte));
        GuardarDocumentos();
    }

    private void GuardarDocumentos()
    {
        ObservableCollection<DocumentoLegal> documentos = new();

        if (CarpetaInvestigacion is true)
            documentos.Add(new DocumentoLegal()
            {
                TipoDocumento = "CI",
                NumeroDocumento = NumeroCi,
                DondeRadica = DondeRadicaCi,
                NombreServidorPublico = NombreServidorPublicoCi,
                FechaRecepcion = FechaRecepcionCi
            });

        if (AmparoBuscador is true)
            documentos.Add(new DocumentoLegal()
            {
                TipoDocumento = "AB",
                NumeroDocumento = NumeroAmparo,
                DondeRadica = DondeRadicaAmparo,
                NombreServidorPublico = NombreJuez,
                FechaRecepcion = FechaRecepcionAb
            });

        if (DerechosHumanos is true)
            documentos.Add(new DocumentoLegal()
            {
                TipoDocumento = "DH",
                NumeroDocumento = NumeroDerechosHumanos,
                DondeRadica = DondeRadicaDh,
                NombreServidorPublico = NombreServidorPublicoDh,
                FechaRecepcion = FechaRecepcionDh
            });

        WeakReferenceMessenger.Default.Send(new AddDocumentoMessage(documentos));
    }

    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos()
    {
        TiposReportes = await ReporteNetwork.GetTiposReportes();
        TiposMedios = await ReporteNetwork.GetTiposMedios();
        Estados = await UbicacionNetwork.GetEstados();
    }

    async partial void OnTipoMedioChanged(Catalogo value)
    {
        Medios = await ReporteNetwork.GetMedios(value.Id);
    }
}