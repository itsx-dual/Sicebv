using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.persona.presentation;

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
     * Información básica de la persona
     */
    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private string _apellidoPaterno = String.Empty;
    [ObservableProperty] private string _apellidoMaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoNombre = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoPaterno = String.Empty;
    [ObservableProperty] private string _pseudonimoApellidoMaterno = String.Empty;

    /**
     * Fecha y lugar de nacimiento
     */
    [ObservableProperty] private DateTime? _fechaNacimiento;

    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
    [ObservableProperty] private Estado _lugarNacimiento = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionaliad = new();

    /**
     * Sexo y género de la persona
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();

    [ObservableProperty] private Catalogo _sexo = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private Catalogo _genero = new();

    /**
     * Identificaciones oficiales de la persona
     */
    [ObservableProperty] private string _rfc = String.Empty;

    [ObservableProperty] private string _curp = String.Empty;
    [ObservableProperty] private string _observacionesCurp = String.Empty;

    private async void CargarCatalogos()
    {
        Sexos = await PersonaNetwork.GetSexos();
        Generos = await PersonaNetwork.GetGeneros();
        LugaresNacimientos = await UbicacionNetwork.GetEstados();
        Nacionalidades = await UbicacionNetwork.GetNacionalidades();
    }
}