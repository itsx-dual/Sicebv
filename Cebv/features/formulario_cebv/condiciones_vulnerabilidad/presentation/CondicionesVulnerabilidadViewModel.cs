using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public partial class CondicionesVulnerabilidadViewModel : ObservableObject
{
    public CondicionesVulnerabilidadViewModel()
    {
        CargarCatalogos();
    }
    
    [ObservableProperty] private string _condicion = String.Empty;
    [ObservableProperty] private string _tratamiento = String.Empty;

    public List<string> Naturaleza = new List<string>()
    {
        "CONDICION",
        "TRATAMIENTO",
        "NATURALEZA"
    };
    
    public List<string> Opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _pertenenciaGrupalOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaGrupal = false;

    private async void CargarCatalogos() =>
        TiposSangre = await CondicionVulnerabilidadNetwork.GetTiposSangre();


    [ObservableProperty] private ObservableCollection<Catalogo> _tiposSangre = new();
    [ObservableProperty] private Catalogo _tipoSangre = new();
}