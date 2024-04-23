using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Cebv.features.Prendas.data.Prendas_Vestir;
using Cebv.features.prendas.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

namespace Cebv.features.Prendas.presentation.prendas_vestir;

public partial class prendasViewModel : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<Grupo_pertenencias> _grupoPertenencias;

    [ObservableProperty] 
    private ObservableCollection<Material> _material;

    [ObservableProperty] 
    private ObservableCollection<Pertenencias>  _pertenencias;
    
    [ObservableProperty] 
    private ObservableCollection<PrendaColor>  _prendaColor;

    [ObservableProperty]
    private string _marca;

    [ObservableProperty] 
    private string _descripcion;
    
    public prendasViewModel()
    {
        CargarCatalogos();
    }

    private async void CargarCatalogos()
    {
        PrendaColor = new ObservableCollection<PrendaColor>(await PrendasVestirNetwork.GetCatalogoColor());
        //var response = await PrendasVestirNetwork.GetCatalogoColor();
        //PrendaColor = new ObservableCollection<PrendaColor>(response);
    }
    
    public ObservableCollection<PrendasData> PrendasDatas { get; set; }
    public ObservableCollection<MainTabla> MainTablas { get; set; }
    
    
}