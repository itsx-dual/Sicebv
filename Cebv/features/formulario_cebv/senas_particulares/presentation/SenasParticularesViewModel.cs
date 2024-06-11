using System.Collections.ObjectModel;
using Cebv.features.formulario_cebv.senas_particulares.data;
using Cebv.features.formulario_cebv.senas_particulares.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.senas_particulares.presentation;

public partial class SenasParticularesViewModel : ObservableObject
{
    [ObservableProperty] private TipoSenas _tipoSenasSelected;

    [ObservableProperty]
    private VistaSenas _vistaSenasSelected;

    [ObservableProperty]
    private int _cantidad = 1;

    [ObservableProperty]
    private string _descripcion;

    [ObservableProperty]
    private string _colorRegionCuerpo;

    [ObservableProperty]
    private string _colorLado;

    [ObservableProperty]
    private Dictionary<string, RegionCuerpo> _regionCuerpo;

    [ObservableProperty]
    private ObservableCollection<TipoSenas> _tipoSenas;

    [ObservableProperty]
    private ObservableCollection<VistaSenas> _vistaSenas;

    [ObservableProperty]
    private Dictionary<string, LadoSenas> _ladoSenas;

    public SenasParticularesViewModel()
    {
        SenasParticularesData = new ObservableCollection<SenasParticularesData>();
        SenasParticularesTabla = new ObservableCollection<SenasParticularesTabla>();
        
        DefaultValues();
        CargarCatalogos();
    }

    private void DefaultValues()
    {
        ColorRegionCuerpo = "3F48CC";
        ColorLado = "6C7156";
        Descripcion = "";
        Cantidad = 1;
    }

    [RelayCommand]
    public async void Insertar()
    {
        SenasParticularesData.Add(new SenasParticularesData(
            1,
            (int)RegionCuerpo[ColorRegionCuerpo].id,
            (int)VistaSenasSelected.id,
            (int)LadoSenas[ColorLado].id,
            (int)TipoSenasSelected.id,
            Cantidad,
            Descripcion,
            "https://www.url.com"
        ));

        SenasParticularesTabla.Add(new SenasParticularesTabla((string)
            RegionCuerpo[ColorRegionCuerpo].nombre,
            VistaSenasSelected.nombre,
            LadoSenas[ColorLado].nombre,
            TipoSenasSelected.nombre,
            Cantidad,
            Descripcion,
            "https://www.url.com"
        ));
        
        DefaultValues();
    }
    
    public ObservableCollection<SenasParticularesData> SenasParticularesData { get; set; }
    public ObservableCollection<SenasParticularesTabla> SenasParticularesTabla { get; set; }

    public async void CargarCatalogos()
    {
        RegionCuerpo = await SenasParticularesNetwork.GetRegionCuerpo();;
        TipoSenas = new ObservableCollection<TipoSenas>(await SenasParticularesNetwork.GetTipo());
        VistaSenas = new ObservableCollection<VistaSenas>(await SenasParticularesNetwork.GetVista());
        LadoSenas = await SenasParticularesNetwork.GetLado();
    }

    public async void CargarSenasParticulares()
    {
        SenasParticularesTabla = new ObservableCollection<SenasParticularesTabla>();
        SenasParticularesData = new ObservableCollection<SenasParticularesData>(await SenasParticularesNetwork.GetSenasParticulares());
        foreach (var senaParticular in SenasParticularesData)
        {
            //TODO: Implementar representacion de lista para el datagrid.
        }
    }
}