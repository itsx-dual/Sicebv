using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.persona.presentation;
using Cebv.features.reportante.domain;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    /**
     * Datos de control
     */
    [ObservableProperty] private bool _esAnonimo;

    [ObservableProperty] private bool _puedeGuardar;

    [ObservableProperty] private PersonaViewModel _reportante = new();

    // Datos de identificación de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();
    
    // Datos de contacto
    [ObservableProperty] private string _telefonoMovil = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private Catalogo _companiaTelefonica = new();
    [ObservableProperty] private string _observacionesMovil = String.Empty;

    [ObservableProperty] private string _telefonoFijo = String.Empty;
    [ObservableProperty] private string _observacionesFijo = String.Empty;

    [ObservableProperty] private string _correoElectronico = String.Empty;
    [ObservableProperty] private string _observacionesCorreoElectronico = String.Empty;

    // Datos de domicilio
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

    // Información relevante
    [ObservableProperty] private string _informacionRelevante = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private Catalogo _escolaridad = new();

    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "Terminada", "En curso", "No especifica"
    };

    [ObservableProperty] private string _estatusEscolaridad = "No especifica";

    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private Catalogo _estadoConyugal = new();
    [ObservableProperty] private List<string> _pertenenciasColectivos = OpcionesCebv.Opciones;
    [ObservableProperty] private string _pertenciaColectivo = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaC = false;
    [ObservableProperty] private string _nombreColectivo = String.Empty;

    /**
     * Constructor
     */
    public ReportanteViewModel()
    {
        CargarCatalogos();
    }


    /**
     * Peticiones a la Api
     */
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetParentescos();
        Estados = await UbicacionNetwork.GetEstados();
        Escolaridades = await PersonaNetwork.GetEscolaridades();
        EstadosConyugales = await PersonaNetwork.GetEstadosConyugales();
    }

    async partial void OnEstadoChanged(Estado value) =>
        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);

    async partial void OnMunicipioChanged(Municipio value) =>
        Asentamientos = await UbicacionNetwork.GetAsentamientos(value.Id);

    partial void OnPertenciaColectivoChanging(string value) =>
        PertenenciaC = OpcionesCebv.MappingToBool(value);

    /**
     * Guardar Borrador
     */
    private void ValidarBorrador()
    {
        if (Reportante.Nombre == String.Empty ||
            Reportante.ApellidoPaterno == String.Empty ||
            Reportante.ApellidoMaterno == String.Empty)
            PuedeGuardar = false;
        else PuedeGuardar = true;

        WeakReferenceMessenger.Default.Send(new GuardarBorradorMessage(PuedeGuardar));
    }

    [RelayCommand]
    public void GuardarBorrador()
    {
        ValidarBorrador();
        Console.WriteLine(Reportante.Nombre);
    }
}