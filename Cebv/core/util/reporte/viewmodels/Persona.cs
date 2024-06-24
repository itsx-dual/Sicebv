using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class Persona : ObservableObject
{
    [JsonConstructor]
    public Persona(
        int? id,
        Estado? lugar_nacimiento,
        string? nombre,
        string? apellido_paterno,
        string? apellido_materno,
        string? pseudonimo_nombre,
        string? pseudonimo_apellido_paterno,
        string? pseudonimo_apellido_materno,
        DateTime? fecha_nacimiento,
        string? curp,
        string? observaciones_curp,
        string? rfc,
        string? ocupacion,
        (int Id, string Nombre)? sexo,
        (int Id, string Nombre)? genero,
        ObservableCollection<(int Id, string Apodo)>? apodos,
        ObservableCollection<(int Id, string Nombre)>? nacionalidades)
    {
        Id = id;
        Nombre = nombre;
        LugarNacimiento = lugar_nacimiento;
        ApellidoPaterno = apellido_paterno;
        ApellidoMaterno = apellido_materno;
        PseudonimoNombre = pseudonimo_nombre;
        PseudonimoApellidoPaterno = pseudonimo_apellido_paterno;
        PseudonimoApellidoMaterno= pseudonimo_apellido_materno;
        FechaNacimiento = fecha_nacimiento;
        Curp = curp;
        ObservacionesCurp = ObservacionesCurp;
        Rfc = rfc;
        Ocupacion = ocupacion;
        Sexo = sexo;
        Genero = genero;
        Apodos = apodos;
        Nacionalidades = nacionalidades;
    }
    
    [ObservableProperty]
    private int? _id;
    
    [ObservableProperty]
    private Estado? _lugarNacimiento;
    
    [ObservableProperty]
    private string? _nombre;
    
    [ObservableProperty]
    private string? _apellidoPaterno;
    
    [ObservableProperty]
    private string? _apellidoMaterno;
    
    [ObservableProperty]
    private string? _pseudonimoNombre;
    
    [ObservableProperty]
    private string? _pseudonimoApellidoPaterno;
    
    [ObservableProperty]
    private string? _pseudonimoApellidoMaterno;
    
    [ObservableProperty]
    private DateTime? _fechaNacimiento;
    
    [ObservableProperty]
    private string? _curp;
    
    [ObservableProperty]
    private string? _observacionesCurp;
    
    [ObservableProperty]
    private string? _rfc;
    
    [ObservableProperty]
    private string? _ocupacion;
    
    [ObservableProperty]
    private (int Id, string Nombre)? _sexo;
    
    [ObservableProperty]
    private (int Id, string Nombre)? _genero;
    
    [ObservableProperty]
    private ObservableCollection<(int Id, string Apodo)>? _apodos = new();
    
    [ObservableProperty]
    private ObservableCollection<(int Id, string Nombre)>? _nacionalidades = new();
    
}