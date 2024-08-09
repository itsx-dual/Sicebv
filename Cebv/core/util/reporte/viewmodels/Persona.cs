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
        string? nivel_escolaridad,
        Catalogo? sexo,
        Catalogo? genero,
        Catalogo? religion,
        Catalogo? lengua,
        Catalogo? escolaridad,
        Catalogo? estado_conyugal,
        MediaFiliacion? media_filiacion,
        ObservableCollection<Apodo>? apodos,
        ObservableCollection<Catalogo>? nacionalidades,
        ObservableCollection<Catalogo>? grupos_vulnerables,
        ObservableCollection<Telefono>? telefonos,
        ObservableCollection<SenaParticular>? senas_particulares,
        int? numeroPersonasVive   
    )
    {
        Id = id;
        Nombre = nombre;
        LugarNacimiento = lugar_nacimiento;
        ApellidoPaterno = apellido_paterno;
        ApellidoMaterno = apellido_materno;
        PseudonimoNombre = pseudonimo_nombre;
        PseudonimoApellidoPaterno = pseudonimo_apellido_paterno;
        PseudonimoApellidoMaterno = pseudonimo_apellido_materno;
        FechaNacimiento = fecha_nacimiento;
        Curp = curp;
        ObservacionesCurp = observaciones_curp;
        Rfc = rfc;
        Ocupacion = ocupacion;
        NivelEscolaridad = nivel_escolaridad;
        Sexo = sexo;
        Genero = genero;
        Apodos = apodos;
        Nacionalidades = nacionalidades;
        Religion = religion;
        Lengua = lengua;
        Telefonos = telefonos;
        Escolaridad = escolaridad;
        EstadoConyugal = estado_conyugal;
        GruposVulnerables = grupos_vulnerables;
        SenasParticulares = senas_particulares;
        MediaFiliacion = media_filiacion;
        NumeroPersonasVive = numeroPersonasVive;
    }

    public Persona()
    {
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

    [ObservableProperty, JsonProperty(PropertyName = "lugar_nacimiento")]
    private Estado? _lugarNacimiento;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_paterno")]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_materno")]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
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

    [ObservableProperty, JsonProperty(PropertyName = "escolaridad")]
    private Catalogo? _escolaridad;

    [ObservableProperty, JsonProperty(PropertyName = "estado_conyugal")]
    private Catalogo? _estadoConyugal;

    [ObservableProperty, JsonProperty(PropertyName = "media_filiacion")]
    private MediaFiliacion? _mediaFiliacion = new();

    [ObservableProperty, JsonProperty(PropertyName = "nivel_escolaridad")]
    private string? _nivelEscolaridad;

    [ObservableProperty, JsonProperty(PropertyName = "telefonos")]
    private ObservableCollection<Telefono>? _telefonos = new();

    [ObservableProperty, JsonProperty(PropertyName = "contactos")]
    private ObservableCollection<Contacto>? _contactos = new();

    [ObservableProperty, JsonProperty(PropertyName = "direcciones")]
    private ObservableCollection<Direccion>? _direcciones = new();

    [ObservableProperty, JsonProperty(PropertyName = "grupos_vulnerables")]
    private ObservableCollection<Catalogo>? _gruposVulnerables = new();

    [ObservableProperty, JsonProperty(PropertyName = "senas_particulares")]
    private ObservableCollection<SenaParticular> _senasParticulares = [];
    
    [ObservableProperty, JsonProperty("numero_personas_vive")]
    private int? _numeroPersonasVive;


    /**
     * Equals, GetHashCode, ToString
     */
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((Persona)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Nombre, ApellidoPaterno, ApellidoMaterno);
    }

    private bool Equals(Persona persona)
    {
        return Id == persona.Id &&
               Nombre == persona.Nombre &&
               ApellidoPaterno == persona.ApellidoPaterno &&
               ApellidoMaterno == persona.ApellidoMaterno;
    }
}