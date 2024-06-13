using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public class CondicionesSalud
{
    public string Condicion { get; set; }
    public string Tratamiento { get; set; }
    public string Naturaleza { get; set; }

    public bool PadeceCondicion { get; set; }
}

public partial class CondicionesVulnerabilidadViewModel : ObservableObject
{
    public CondicionesVulnerabilidadViewModel()
    {
        CargarCatalogos();
    }

    [ObservableProperty] private ObservableCollection<CondicionesSalud> _condicionesSalud = new();

    [ObservableProperty] private List<string> _naturalezaOpciones = new()
    {
        "CONDICION",
        "TRATAMIENTO",
        "NATURALEZA",
        "Nose"
    };

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _transitoEstadosUnidosOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _transitoEstadosUnidos = false;

    [ObservableProperty] private ObservableCollection<Catalogo> _situacionesMigratorias = new();
    [ObservableProperty] private Catalogo _situacionMigratoria = new();

    [ObservableProperty] private string _procesoMigratorioDescripcion = String.Empty;

    [ObservableProperty] private string _pertenenciaGrupalOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaGrupal = false;

    [ObservableProperty] private ObservableCollection<Catalogo> _enfoquesDiferenciados = new();
    [ObservableProperty] private Catalogo _enfoqueDiferenciado = new();

    [ObservableProperty] private string _caracteristicaPersonal = String.Empty;

    [ObservableProperty] private string _informacionPersonal = String.Empty;

    [ObservableProperty] private bool _estaEmbarazada;

    [ObservableProperty] private int _mesesEmbarazo;

    private async void CargarCatalogos()
    {
        TiposSangre = await CondicionVulnerabilidadNetwork.GetTiposSangre();
        EnfoquesDiferenciados = await CondicionVulnerabilidadNetwork.GetEnfoquesDiferenciados();
        SituacionesMigratorias = await CondicionVulnerabilidadNetwork.GetSituacionesMigratorias();
    }


    [ObservableProperty] private ObservableCollection<Catalogo> _tiposSangre = new();
    [ObservableProperty] private Catalogo _tipoSangre = new();
}