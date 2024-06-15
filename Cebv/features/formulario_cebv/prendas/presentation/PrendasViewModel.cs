using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.prendas.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendasViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public PrendasViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();

    [ObservableProperty] private Catalogo _pertenencia = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _pertenencias = new();
    [ObservableProperty] private Catalogo _grupoPertenencia = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo _color = new();
    
    [ObservableProperty] private string _marca = string.Empty;
    [ObservableProperty] private string _descripcion = string.Empty;

    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void CargarCatalogos()
    {
        GruposPertenencias = await PrendasNetwork.GetGruposPertenencias();
        Colores = await PrendasNetwork.GetColores();
    }

    async partial void OnGrupoPertenenciaChanged(Catalogo value) =>
        Pertenencias = await PrendasNetwork.GetPertenencias(value.Id);
}