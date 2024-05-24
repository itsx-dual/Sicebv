using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.reportante.domain;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableObject
{
    /**
     * Constructor
     */
    public EncuadrePreeliminarViewModel() =>
        CargarCatalogos();
    
    /**
     * Inicio
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();

    [ObservableProperty] private Catalogo _tipoMedio = new();
    [ObservableProperty] private ObservableCollection<Medio> _medios = new();
    [ObservableProperty] private Medio _medio = new();

    /**
     * Reportante
     */
    [ObservableProperty] private string _nombre = String.Empty;
    [ObservableProperty] private string _apellidoPaterno = String.Empty;
    [ObservableProperty] private string _apellidoMaterno = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private Catalogo _sexo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();

    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos()
    {
        Sexos = await PersonaNetwork.GetSexos();
        Parentescos = await ReportanteNetwork.GetParentescos();
        TiposMedios = await ReporteNetwork.GetTiposMedios();
    }

    async partial void OnTipoMedioChanged(Catalogo value) =>
        Medios = await ReporteNetwork.GetMedios(value.Id);
}