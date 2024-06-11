using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.domain;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableObject
{
    /**
     * Constructor
     */
    public EncuadrePreeliminarViewModel() =>
        CargarCatalogos();

    /**
     * Inicio
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();

    [ObservableProperty] private Catalogo _tipoMedio = new();
    [ObservableProperty] private ObservableCollection<Medio> _medios = new();
    [ObservableProperty] private Medio _medio = new();

    /**
     * Reportante
     */
    [ObservableProperty] private string _nombreR = String.Empty;

    [ObservableProperty] private string _apellidoPaternoR = String.Empty;
    [ObservableProperty] private string _apellidoMaternoR = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _sexosR = new();
    [ObservableProperty] private Catalogo _sexoR = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();

    /**
     * Desaparecido
     */
    [ObservableProperty] private string _nombreD = String.Empty;

    [ObservableProperty] private string _apellidoPaternoD = String.Empty;
    [ObservableProperty] private string _apellidoMaternoD = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _sexosD = new();
    [ObservableProperty] private Catalogo _sexoD = new();
    [ObservableProperty] private bool _fechaAproximada;
    [ObservableProperty] private bool _fechaAproximada2;
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionaliad = new();
    [ObservableProperty] private string _curp = String.Empty;

    // Domicilio
    [ObservableProperty] private string _calle = String.Empty;
    [ObservableProperty] private string _numeroExterior = String.Empty;
    [ObservableProperty] private string _numeroInterior = String.Empty;
    [ObservableProperty] private string _colonia = String.Empty;
    [ObservableProperty] private string _codigoPostal = String.Empty;

    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private Estado _estado = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private Municipio _municipio = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();
    [ObservableProperty] private Asentamiento _asentamiento = new();

    [ObservableProperty] private string _entreCalle1 = String.Empty;
    [ObservableProperty] private string _entreCalle2 = String.Empty;
    [ObservableProperty] private string _tramoCarretero = String.Empty;
    [ObservableProperty] private string _referencia = String.Empty;

    /**
     * Peticiones a la API.
     */
    private async void CargarCatalogos()
    {
        SexosR = await PersonaNetwork.GetSexos();
        SexosD = await PersonaNetwork.GetSexos();
        Parentescos = await ReportanteNetwork.GetParentescos();
        TiposMedios = await ReporteNetwork.GetTiposMedios();
        Nacionalidades = await UbicacionNetwork.GetNacionalidades();
        Estados = await UbicacionNetwork.GetEstados();
    }

    async partial void OnTipoMedioChanged(Catalogo value) =>
        Medios = await ReporteNetwork.GetMedios(value.Id);

    async partial void OnEstadoChanged(Estado value) =>
        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);

    async partial void OnMunicipioChanged(Municipio value) =>
        Asentamientos = await UbicacionNetwork.GetAsentamientos(value.Id);
}