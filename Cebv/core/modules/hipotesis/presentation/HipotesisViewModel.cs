using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
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
    [ObservableProperty] private ObservableCollection<BasicResource> _hipotesisInmediata = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _sitios = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();

    /**
     * Peticiones a la red
     */
    private async void LoadAsync()
    {
        TiposHipotesis = await CebvNetwork.GetRoute<TipoHipotesis>("tipos-hipotesis");
        HipotesisInmediata = await CebvNetwork.GetRoute<BasicResource>("tipos-hipotesis-inmediata");
        Sitios = await CebvNetwork.GetRoute<Catalogo>("sitios");
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas");
    }
}