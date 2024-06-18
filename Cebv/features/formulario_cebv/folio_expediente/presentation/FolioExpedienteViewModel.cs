using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.folio_expediente.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedienteViewModel : ObservableObject
{
    public FolioExpedienteViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes = new();
    [ObservableProperty] private Catalogo _tipoReporte = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private Catalogo _area = new();
    
    /**
     * Peticiones a la APi para cargar los catalagos
     */
    private async void CargarCatalogos()
    {
        TiposReportes = await FolioExpedienteNetwork.GetTiposReportes();
        Areas = await FolioExpedienteNetwork.GetAreas();
    }
}