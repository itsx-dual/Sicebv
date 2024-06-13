using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.control_ogpi.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.control_ogpi.presentation;

public partial class ControlOgpiViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public ControlOgpiViewModel()
    {
        CargarCatalogos();
    }

    [ObservableProperty] private DateTime? _fechaCodificacion;
    [ObservableProperty] private string _nombreCodificador = String.Empty;
    [ObservableProperty] private string _observaciones = String.Empty;
    [ObservableProperty] private string _numeroTarjeta = String.Empty;
    [ObservableProperty] private string _folioFub = String.Empty;
    [ObservableProperty] private string _origenFolioFub = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _estatusPersonas = new();
    [ObservableProperty] private Catalogo _estatusPersona = new();

    /**
     * Método que carga los catálogos
     */
    private async void CargarCatalogos() =>
        EstatusPersonas = await ControlOgpiNetwork.GetEstatusPersonas();
}