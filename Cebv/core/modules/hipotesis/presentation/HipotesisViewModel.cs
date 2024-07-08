using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.hipotesis.domain;
using Cebv.core.modules.reporte.domain;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.modules.hipotesis.presentation;

public partial class HipotesisViewModel : ObservableObject
{

    /**
     * Constructor de la Clase
     */
    public HipotesisViewModel()
    {
        LoadAsync();
    }
    
    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<TipoHipotesis> _tiposHipotesis = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _sitios = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    
    /**
     * Peticiones a la red
     */
    private async void LoadAsync()
    {
        TiposHipotesis = await HipotesisNetwork.GetTiposHipotesis();
        Sitios = await HipotesisNetwork.GetSitios();
        Areas = await ReporteNetwork.GetAreas();
    }
}