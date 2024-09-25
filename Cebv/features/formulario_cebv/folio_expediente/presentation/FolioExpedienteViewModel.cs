using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Estado = Cebv.core.modules.ubicacion.data.Estado;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedienteViewModel : ObservableObject
{
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    
    public FolioExpedienteViewModel()
    {
        TiposDesapariciones.Add("UNICA", "U");
        TiposDesapariciones.Add("MULTIPLE", "M");
        Reporte = _reporteService.GetReporte();
        CargarCatalogos();
    }

    [ObservableProperty] private Estado? _estado;
    [ObservableProperty] private UbicacionViewModel? _ubicacionHechos;

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposReportes = new();

    [ObservableProperty] private Catalogo _tipoReporte = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();
    [ObservableProperty] private Catalogo _area = new();

    [ObservableProperty] private Dictionary<string, string> _tiposDesapariciones = new();

    [ObservableProperty] private string _tipoDesaparicion = String.Empty;
    [ObservableProperty] private string _zonaEstado = String.Empty;

    [ObservableProperty] private ObservableCollection<string> _canalizaNorte = new();
    [ObservableProperty] private ObservableCollection<string> _canalizaCentro = new();
    [ObservableProperty] private ObservableCollection<string> _canalizaSur = new();

    [ObservableProperty] private ObservableCollection<string> _zonaNorte = new();
    [ObservableProperty] private ObservableCollection<string> _zonaCentro = new();
    [ObservableProperty] private ObservableCollection<string> _zonaSur = new();
    
    [ObservableProperty] private Folio _folio = new();

    partial void OnTipoDesaparicionChanged(string value)
    {
        Console.WriteLine(value);
    }


    /**
     * Peticiones a la APi para cargar los catalagos
     */
    private async void CargarCatalogos()
    {
        TiposReportes = await CebvNetwork.GetRoute<Catalogo>("tipos-reportes");
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas");
    }
    

    [RelayCommand]
    private async Task SetFolio()
    { 
       // await FolioExpedienteNetwork.SetFolio(Reporte.Id);
    }

    /**
    * Logica de la zona que atiende el reporte
    */
    private void Norte()
    {
        CanalizaNorte =
        [
            "30001", "30002", "30160", "30009", "30010", "30023", "30027", "30157", "30033", "30034",
            "30055", "30056", "30057", "30058", "30060", "30063", "30064", "30035", "30036", "30037",
            "30040", "30042", "30050", "30051", "30205", "30066", "30067", "30069", "30072", "30076",
            "30078", "30083", "30088", "30093", "30095", "30096", "30107", "30132", "30102", "30103",
            "30106", "30109", "30112", "30013", "30114", "30121", "30123", "30124", "30128", "30129",
            "30131", "30133", "30136", "30211", "30150", "30151", "30152", "30153", "30154", "30155",
            "30156", "30158", "30161", "30163", "30166", "30167", "30170", "30175", "30180", "30177",
            "30183", "30187", "30189", "30192", "30194", "30197", "30198", "30202", "30203"
        ];

        ZonaNorte =
        [
            "30160", "30023", "30027", "30157", "30033", "30034", "30055", "30056", "30057", "30058",
            "30060", "30063", "30064", "30035", "30037", "30040", "30042", "30050", "30051", "30205",
            "30066", "30067", "30069", "30072", "30076", "30078", "30083", "30095", "30102", "30103",
            "30109", "30013", "30114", "30121", "30123", "30124", "30129", "30131", "30133", "30211",
            "30150", "30151", "30152", "30153", "30154", "30155", "30158", "30161", "30163", "30167",
            "30170", "30175", "30180", "30183", "30189", "30192", "30197", "30198", "30202", "30203"
        ];
    }

    private void Centro()
    {
        CanalizaCentro =
        [
            "30004", "30006", "30008", "30014", "30017", "30018", "30019", "30020", "30021", "30022",
            "30025", "30026", "30029", "30007", "30030", "30031", "30062", "30038", "30041", "30043",
            "30044", "30046", "30047", "30049", "30052", "30053", "30065", "30068", "30071", "30074",
            "30079", "30080", "30081", "30085", "30086", "30090", "30016", "30127", "30137", "30098",
            "30099", "30100", "30101", "30110", "30113", "30115", "30117", "30118", "30126", "30125",
            "30134", "30135", "30138", "30140", "30146", "30147", "30148", "30159", "30162", "30164",
            "30165", "30168", "30171", "30173", "30185", "30179", "30182", "30024", "30184", "30186",
            "30188", "30191", "30087", "30092", "30195", "30196", "30200", "30201"
        ];

        ZonaCentro =
        [
            "30001", "30002", "30004", "30006", "30008", "30009", "30010", "30014", "30017", "30018",
            "30019", "30020", "30021", "30022", "30025", "30026", "30028", "30029", "30007", "30030",
            "30031", "30062", "30036", "30038", "30041", "30043", "30044", "30046", "30047", "30049",
            "30052", "30053", "30065", "30068", "30071", "30074", "30079", "30080", "30081", "30085",
            "30086", "30088", "30090", "30093", "30016", "30127", "30096", "30107", "30132", "30137",
            "30098", "30099", "30100", "30101", "30105", "30106", "30110", "30112", "30113", "30115",
            "30117", "30118", "30126", "30125", "30128", "30134", "30135", "30136", "30138", "30140",
            "30146", "30147", "30148", "30156", "30159", "30162", "30164", "30166", "30165", "30168",
            "30171", "30173", "30185", "30177", "30179", "30181", "30182", "30024", "30184", "30186",
            "30187", "30188", "30191", "30193", "30194", "30087", "30092", "30195", "30196", "30200",
            "30201"
        ];
    }

    private void Sur()
    {
        CanalizaSur =
        [
            "30003", "30005", "30204", "30011", "30012", "30015", "30028", "30208", "30032", "30054",
            "30059", "30039", "30045", "30048", "30070", "30073", "30075", "30077", "30082", "30084",
            "30089", "30091", "30169", "30094", "30061", "30097", "30104", "30105", "30108", "30111",
            "30206", "30116", "30119", "30120", "30122", "30130", "30139", "30141", "30142", "30212",
            "30143", "30144", "30145", "30149", "30209", "30172", "30174", "30176", "30178", "30181",
            "30207", "30190", "30210", "30193", "30199"
        ];

        ZonaSur =
        [
            "30003", "30005", "30204", "30011", "30012", "30015", "30208", "30032", "30054", "30059",
            "30039", "30045", "30048", "30070", "30073", "30075", "30077", "30082", "30084", "30089",
            "30091", "30169", "30094", "30061", "30097", "30104", "30108", "30111", "30206", "30116",
            "30119", "30120", "30122", "30130", "30139", "30141", "30142", "30212", "30143", "30144",
            "30145", "30149", "30209", "30172", "30174", "30176", "30178", "30207", "30190", "30210",
            "30199"
        ];
    }

    /*private void MatchArea()
    {
        Norte();
        Centro();
        Sur();

        //var id = _reporteService.UbicacionHechos!.Municipio.Id;

        Catalogo area = new();

        //if (CanalizaNorte.Contains(id))
        //{
        //    area = new Catalogo
        //    {
        //        Id = 1,
        //        Nombre = "Celula Norte"
        //    };
        //}
//
        //if (CanalizaCentro.Contains(id))
        //{
        //    area = new Catalogo
        //    {
        //        Id = 2,
        //        Nombre = "Celula Centro"
        //    };
        //}
//
        //if (CanalizaSur.Contains(id))
        //{
        //    area = new Catalogo
        //    {
        //        Id = 3,
        //        Nombre = "Celula Sur"
        //    };
        //}
        //
        //if (ZonaNorte.Contains(id)) ZonaEstado = "Norte";
        //if (ZonaCentro.Contains(id)) ZonaEstado = "Centro";
        //if (ZonaSur.Contains(id)) ZonaEstado = "Sur";
        //
        //Area = area;
        //
        //
        //Console.WriteLine(Area.Nombre);
    }*/

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}