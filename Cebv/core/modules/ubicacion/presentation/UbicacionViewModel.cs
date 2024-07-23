using System.Collections.ObjectModel;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.modules.ubicacion.presentation;

public partial class UbicacionViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public UbicacionViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private string _calle = String.Empty;

    [ObservableProperty] private string _numeroExterior = String.Empty;
    [ObservableProperty] private string _numeroInterior = String.Empty;
    [ObservableProperty] private string _colonia = String.Empty;
    [ObservableProperty] private string _codigoPostal = String.Empty;

    [ObservableProperty] private ObservableCollection<util.reporte.viewmodels.Estado> _estados = new();
    [ObservableProperty] private Estado? _estado;
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private Municipio _municipio = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();
    [ObservableProperty] private Asentamiento _asentamiento = new();

    [ObservableProperty] private string _entreCalle1 = String.Empty;
    [ObservableProperty] private string _entreCalle2 = String.Empty;
    [ObservableProperty] private string _tramoCarretero = String.Empty;
    [ObservableProperty] private string _referencia = String.Empty;

    /**
     * Peticiones a la API para obtener los catálogos
     */
    private async void CargarCatalogos()
    {
       Estados = await UbicacionNetwork.GetEstados();
    }


    async partial void OnEstadoChanged(Estado? value)
    {
        if (value is null) return;

        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);
    }


    async partial void OnMunicipioChanged(Municipio value) =>
        Asentamientos = await UbicacionNetwork.GetAsentamientos(value.Id);
}