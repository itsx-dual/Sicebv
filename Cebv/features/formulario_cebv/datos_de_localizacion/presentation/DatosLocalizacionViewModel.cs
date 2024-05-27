using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosLocalizacionViewModel : ObservableObject
{
    /**
     * Constructor
     */
    public DatosLocalizacionViewModel() => CargarCatalogos();
    
    /**
     * Localización
     */
    [ObservableProperty] private bool _localizadaConVida;
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private Estado _estado = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private Municipio _municipio = new();
    
    
    private async void CargarCatalogos() =>
        Estados = await UbicacionNetwork.GetEstados();
    

    async partial void OnEstadoChanged(Estado value) =>
        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);
}