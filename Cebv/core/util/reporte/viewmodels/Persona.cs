using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
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
        Catalogo? sexo,
        Catalogo? genero,
        Catalogo? religion,
        Catalogo? lengua,
        ObservableCollection<Apodo>? apodos,
        ObservableCollection<Catalogo>? nacionalidades
        )
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
        ObservacionesCurp = observaciones_curp;
        Rfc = rfc;
        Ocupacion = ocupacion;
        Sexo = sexo;
        Genero = genero;
        Apodos = apodos;
        Nacionalidades = nacionalidades;
        Religion = religion;
        Lengua = lengua;
    }

    public Persona() { }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;
    
    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
    
    [ObservableProperty, JsonProperty(PropertyName = "lugar_nacimiento")]
    private Estado? _lugarNacimiento;
    
    [ObservableProperty, JsonProperty(PropertyName = "nombre")] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;
    
    [ObservableProperty, JsonProperty(PropertyName = "apellido_paterno")] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;
    
    [ObservableProperty, JsonProperty(PropertyName = "apellido_materno")] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;
    
    [ObservableProperty, JsonProperty(PropertyName = "pseudonimo_nombre")]
    private string? _pseudonimoNombre;
    
    [ObservableProperty, JsonProperty(PropertyName = "pseudonimo_apellido_paterno")]
    private string? _pseudonimoApellidoPaterno;
    
    [ObservableProperty, JsonProperty(PropertyName = "pseudonimo_apellido_materno")]
    private string? _pseudonimoApellidoMaterno;
    
    [ObservableProperty, JsonProperty(PropertyName = "fecha_nacimiento")]
    private DateTime? _fechaNacimiento;
    
    [ObservableProperty, JsonProperty(PropertyName = "curp")]
    private string? _curp;
    
    [ObservableProperty, JsonProperty(PropertyName = "observaciones_curp")]
    private string? _observacionesCurp;
    
    [ObservableProperty, JsonProperty(PropertyName = "rfc")]
    private string? _rfc;
    
    [ObservableProperty, JsonProperty(PropertyName = "ocupacion")]
    private string? _ocupacion;
    
    [ObservableProperty, JsonProperty(PropertyName = "sexo")]
    private Catalogo? _sexo;
    
    [ObservableProperty, JsonProperty(PropertyName = "genero")]
    private Catalogo? _genero;
    
    [ObservableProperty, JsonProperty(PropertyName = "apodos")]
    private ObservableCollection<Apodo>? _apodos = new();
    
    [ObservableProperty, JsonProperty(PropertyName = "nacionalidades")]
    private ObservableCollection<Catalogo>? _nacionalidades = new();
    
    [ObservableProperty, JsonProperty(PropertyName = "religion")]
    private Catalogo? _religion;
    
    [ObservableProperty, JsonProperty(PropertyName = "lengua")]
    private Catalogo? _lengua;
}