using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.persona.presentation;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    [ObservableProperty] private PersonaViewModel _desaparecido = new();
    /**
     * Información de la persona desaparecida.
     */
    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;
    
    [ObservableProperty] private string _apodo = String.Empty;

    // Informacion de nacimiento
    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private DateTime? _fechaNacimiento;
    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
    [ObservableProperty] private Estado _lugarNacimiento = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionaliad = new();

    [ObservableProperty] private string _rfc = String.Empty;
    [ObservableProperty] private string _curp = String.Empty;
    [ObservableProperty] private string _observacionesCurp = String.Empty;

    [ObservableProperty] private List<string> _transitoEstadosUnidosList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _transitoEstadosUnidosSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _transitoEstadosUnidos;
    [ObservableProperty] private bool _sinoSeEncuentraEnTransito;
    
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

    /**
     * Datos sociemográficos de la persona desaparecida.
     */
    [ObservableProperty] private List<string> _hablaEspanolList = OpcionesCebv.Opciones;

    [ObservableProperty] private string _hablaEspanolSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _hablaEspanol;

    [ObservableProperty] private List<string> _sabeLeerList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _sabeLeerSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeLeer;

    [ObservableProperty] private List<string> _sabeEscribirList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _sabeEscribirSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeEscribir;

    /**
     * Constructor de la clase.
     */
    public PersonaDesaparecidaViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Peticiones a la API
     */
    public async void CargarCatalogos()
    {
        Nacionalidades = await UbicacionNetwork.GetNacionalidades();
    }

    /**
     * Mapeo de los valores string a boolean.
     */
    partial void OnTransitoEstadosUnidosSelectedChanged(string value)
    {
        if (value is OpcionesCebv.No && Nacionaliad.Id is not 109)
            SinoSeEncuentraEnTransito = true;
        else SinoSeEncuentraEnTransito = false;
    }

    partial void OnNacionaliadChanged(Catalogo value)
    {
        if (TransitoEstadosUnidosSelected is OpcionesCebv.No && value.Id is not 109)
            SinoSeEncuentraEnTransito = true;
        else SinoSeEncuentraEnTransito = false;
    }


    /**
     * Encabezado del formulario con el nombre completo de la persona desaparecida.
     */
    partial void OnNombreChanged(string? value) => Encabezado();

    partial void OnApellidoPaternoChanged(string? value) => Encabezado();

    partial void OnApellidoMaternoChanged(string? value) => Encabezado();

    public void Encabezado()
    {
        WeakReferenceMessenger.Default.Send(new NombreCompletoMessage(NombreCompleto));
    }
}