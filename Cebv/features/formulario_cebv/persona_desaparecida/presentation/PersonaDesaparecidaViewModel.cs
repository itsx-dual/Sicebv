using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    /**
     * Información de la persona desaparecida.
     */
    // Nombres y pseudónimos
    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;

    [ObservableProperty] private string _pseudonimoNombre = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoPaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoMaterno = String.Empty;
    [ObservableProperty] private string _apodo = String.Empty;

    // Información sexual
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private Catalogo _sexo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private Catalogo _genero = new();

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