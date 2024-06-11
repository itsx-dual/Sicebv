using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.persona.domain;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.core.modules.persona.presentation;

public partial class PersonaViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public PersonaViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    // Nombre de la persona
    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private string _apodo = String.Empty;
    [ObservableProperty] private string _apodoSelected = String.Empty;
    [ObservableProperty] private ObservableCollection<string> _apodos = new();

    [ObservableProperty] private string _apellidoPaterno = String.Empty;
    [ObservableProperty] private string _apellidoMaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoNombre = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoPaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoMaterno = String.Empty;

    // Fecha de nacimiento y nacionalidad de la persona
    [ObservableProperty] private DateTime? _fechaNacimiento;

    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
    [ObservableProperty] private Estado _lugarNacimiento = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionaliad = new();

    // Sexo y género de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();

    [ObservableProperty] private Catalogo _sexo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private Catalogo _genero = new();

    // Religión y lengua de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones = new();
    [ObservableProperty] private Catalogo _religion = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas = new();
    [ObservableProperty] private Catalogo _lengua = new();

    // Escolaridad de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private Catalogo _escolaridad = new();
    
    // Grupos vulnerables
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposVulnerables = new();
    [ObservableProperty] private Catalogo _grupoVulnerable = new();

    //  Ocupaciones de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _ocupaciones = new();
    [ObservableProperty] private Catalogo _ocupacionUno = new();
    [ObservableProperty] private string _ocupacionUnoObservaciones = String.Empty;
    [ObservableProperty] private Catalogo _ocupacionDos = new();
    [ObservableProperty] private string _ocupacionDosObservaciones = String.Empty;

    // Estado Conyugal
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private Catalogo _estadoConyugal = new();
    [ObservableProperty] private string _nombrePareja = String.Empty;

    // Identificaciones oficiales de la persona
    [ObservableProperty] private string _rfc = String.Empty;

    [ObservableProperty] private string _curp = String.Empty;
    [ObservableProperty] private string _observacionesCurp = String.Empty;

    /**
     * Lógica de la clase
     */
    [RelayCommand]
    private void AddApodo()
    {
        if (Apodo == String.Empty) return;
        Apodos.Add(Apodo);
        Apodo = String.Empty;
    }

    [RelayCommand]
    private void RemoveApodo()
    {
        if (ApodoSelected == String.Empty) return;
        Apodos.Remove(ApodoSelected);
    }


    /**
     * Peticiones a la API para obtener los catálogos
     */
    private async void CargarCatalogos()
    {
        Sexos = await PersonaNetwork.GetSexos();
        Generos = await PersonaNetwork.GetGeneros();
        Religiones = await PersonaNetwork.GetReligiones();
        Lenguas = await PersonaNetwork.GetLenguas();
        LugaresNacimientos = await UbicacionNetwork.GetEstados();
        Nacionalidades = await UbicacionNetwork.GetNacionalidades();
        Escolaridades = await PersonaNetwork.GetEscolaridades();
        Ocupaciones = await PersonaNetwork.GetOcupaciones();
        EstadosConyugales = await PersonaNetwork.GetEstadosConyugales();
        GruposVulnerables = await PersonaNetwork.GetGruposVulnerables();
    }
}