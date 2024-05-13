using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.util;
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

    // Información del RNPDNO.

    // Información de consentimiento.
    [ObservableProperty] private bool? _informacionConsentimiento = false;
    [ObservableProperty] private string _informacionConsentimientoLabel = OpcionesCebv.No;
    [ObservableProperty] private bool? _informacionExclusivaBusqueda = false;
    [ObservableProperty] private string _informacionExclusivaBusquedaLabel = OpcionesCebv.No;
    [ObservableProperty] private bool? _publicacionInformacion = false;
    [ObservableProperty] private string _publicacionInformacionLabel = OpcionesCebv.No;

    // Información de carpeta de investigación.
    [ObservableProperty] private bool? _carpetaInvestigacion = false;
    [ObservableProperty] private string _carpetaInvestigacionLabel = OpcionesCebv.No;

    // Información de amparo de buscador.
    [ObservableProperty] private bool? _amparoBuscador = false;
    [ObservableProperty] private string _amparoBuscadorLabel = OpcionesCebv.No;

    // Información de recomendación de derechos humanos.
    [ObservableProperty] private bool? _derechosHumanos = false;
    [ObservableProperty] private string _derechosHumanosLabel = OpcionesCebv.No;
    [ObservableProperty] private bool? _otroDerechoHumano = false;

    /// <summary>
    /// Manejador de etiquetas según los valores tertiarios del checkbox (True, False, Null).
    /// Esto es un elemento de la UI que hace explícita la selección del usuario.
    /// </summary>
    partial void OnInformacionConsentimientoChanged(bool? value) =>
        InformacionConsentimientoLabel = OpcionesCebv.GetLabel(value);

    partial void OnInformacionExclusivaBusquedaChanged(bool? value) =>
        InformacionExclusivaBusquedaLabel = OpcionesCebv.GetLabel(value);

    partial void OnPublicacionInformacionChanged(bool? value) =>
        PublicacionInformacionLabel = OpcionesCebv.GetLabel(value);

    partial void OnCarpetaInvestigacionChanged(bool? value) =>
        CarpetaInvestigacionLabel = OpcionesCebv.GetLabel(value);

    partial void OnAmparoBuscadorChanged(bool? value) =>
        AmparoBuscadorLabel = OpcionesCebv.GetLabel(value);

    partial void OnDerechosHumanosChanged(bool? value) =>
        DerechosHumanosLabel = OpcionesCebv.GetLabel(value);

    ///
    /// Constructor
    ///
    public DatosReporteViewModel()
    {
        CargarCatalogos();
    }

    ///
    /// Comandos
    ///
    [RelayCommand]
    public void GuardarReporte()
    {
        ReporteRequest reporte = new()
        {
            TipoReporte = TipoReporte,
        };

        WeakReferenceMessenger.Default.Send(new AddReporteMessage(reporte));
    }

    ///
    /// Peticiones a la Api
    ///
    private async void CargarCatalogos()
    {
        TiposReportes = await ReporteNetwork.GetTiposReportes();
    }

    partial void OnTipoReporteChanged(Catalogo value)
    {
        Console.WriteLine(value.Nombre);
    }
}