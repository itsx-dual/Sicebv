using System.Collections.ObjectModel;
using System.Security;
using Cebv.core.data;
using Cebv.core.modules.persona.data;
using Cebv.core.modules.persona.domain;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
using Cebv.core.util.reporte.data;
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

    public static async Task<PersonaViewModel> CreateAsync(Persona persona = null)
    {
        var result = new PersonaViewModel();
        result.Sexos = await PersonaNetwork.GetSexos();
        result.Generos = await PersonaNetwork.GetGeneros();
        result.Religiones = await PersonaNetwork.GetReligiones();
        result.Lenguas = await PersonaNetwork.GetLenguas();
        result.LugaresNacimientos = await UbicacionNetwork.GetEstados();
        result.Nacionalidades = await UbicacionNetwork.GetNacionalidades();
        result.Escolaridades = await PersonaNetwork.GetEscolaridades();
        result.TiposOcupaciones = await PersonaNetwork.GetTiposOcupaciones();
        result.EstadosConyugales = await PersonaNetwork.GetEstadosConyugales();
        result.GruposVulnerables = await PersonaNetwork.GetGruposVulnerables();

        if (persona != null)
        {
            result.Nombre = persona.Nombre!;
            result.ApellidoPaterno = persona.ApellidoPaterno;
            result.ApellidoMaterno = persona.ApellidoMaterno;
            result.PseudonimoNombre = persona.PseudonimoNombre;
            result.PseudonimoApellidoPaterno = persona.PseudonimoApellidoPaterno;
            result.PseudonimoApellidoMaterno = persona.PseudonimoApellidoMaterno;
            result.FechaNacimiento = persona.FechaNacimiento;
            result.Curp = persona.Curp;
            result.ObservacionesCurp = persona.ObservacionesCurp;
            result.Rfc = persona.Rfc;
            result.OcupacionUno.Nombre = persona.Ocupacion;
            result.Sexo = result.Sexos.FirstOrDefault(catalogo => persona.Sexo != null && catalogo.Id == persona.Sexo.Id);
            result.Genero = result.Generos.FirstOrDefault(catalogo => persona.Sexo != null && catalogo.Id == persona.Sexo.Id);
        }
        
        return result;
    }

    /**
     * Variables de la clase
     */
    // Nombre de la persona
    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private string _apodo = String.Empty;
    [ObservableProperty] private string _apellidoPaterno = String.Empty;
    [ObservableProperty] private string _apellidoMaterno = String.Empty;

    // Pseudónimos de la persona
    [ObservableProperty] private string _pseudonimoNombre = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoPaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoMaterno = String.Empty;
    [ObservableProperty] private ObservableCollection<Pseudonimo> _pseudonimos = new();

    // Fecha de nacimiento y nacionalidad de la persona
    [ObservableProperty] private DateTime? _fechaNacimiento;

    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
    [ObservableProperty] private Estado _lugarNacimiento = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionalidad = new();

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
    [ObservableProperty] private Catalogo? _grupoVulnerable;
    [ObservableProperty] private ObservableCollection<Catalogo> _pertenciasGrupales = new();

    //  Ocupaciones de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposOcupaciones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _ocupacionesUno = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _ocupacionesDos = new();

    [ObservableProperty] private Catalogo _tipoOcupacionUno = new();
    [ObservableProperty] private Catalogo _ocupacionUno = new();
    [ObservableProperty] private string _ocupacionUnoObservaciones = String.Empty;

    [ObservableProperty] private Catalogo _tipoOcupacionDos = new();
    [ObservableProperty] private Catalogo _ocupacionDos = new();
    [ObservableProperty] private string _ocupacionDosObservaciones = String.Empty;
    [ObservableProperty] private string _ocupacionesObservaciones = String.Empty;

    // Estado Conyugal
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private Catalogo _estadoConyugal = new();
    [ObservableProperty] private string _nombrePareja = String.Empty;

    // Identificaciones oficiales de la persona
    [ObservableProperty] private string _rfc = String.Empty;

    [ObservableProperty] private string _curp = String.Empty;
    [ObservableProperty] private string _observacionesCurp = String.Empty;

    /**
     * Añadir y eliminar pertenencias grupales
     */
    [RelayCommand]
    private void AddPertenenciaGrupal()
    {
        if (GrupoVulnerable is null) return;

        PertenciasGrupales.Add(GrupoVulnerable);
        GrupoVulnerable = null;
    }

    [RelayCommand]
    private void RemovePertenenciaGrupal(Catalogo pertenencia) =>
        PertenciasGrupales.Remove(pertenencia);


    /**
     * Añadir y eliminar pseudónimos
     */
    [RelayCommand]
    private void AddPseudonimo()
    {
        if (PseudonimoNombre == String.Empty) return;

        Pseudonimos.Add(new Pseudonimo
        {
            Nombre = PseudonimoNombre,
            ApellidoPaterno = PseudonimoApellidoPaterno,
            ApellidoMaterno = PseudonimoApellidoMaterno
        });
        
        PseudonimoNombre = String.Empty;
        PseudonimoApellidoPaterno = String.Empty;
        PseudonimoApellidoMaterno = String.Empty;
    }

    [RelayCommand]
    private void RemovePseudonimo(Pseudonimo pseudonimo) =>
        Pseudonimos.Remove(pseudonimo);


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
        TiposOcupaciones = await PersonaNetwork.GetTiposOcupaciones();
        EstadosConyugales = await PersonaNetwork.GetEstadosConyugales();
        GruposVulnerables = await PersonaNetwork.GetGruposVulnerables();
    }

    async partial void OnTipoOcupacionUnoChanged(Catalogo value) =>
        OcupacionesUno = await PersonaNetwork.GetOcupaciones(value.Id);

    async partial void OnTipoOcupacionDosChanged(Catalogo value) =>
        OcupacionesDos = await PersonaNetwork.GetOcupaciones(value.Id);
}