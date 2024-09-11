using System.Collections.ObjectModel;
using Cebv.core.modules.persona.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Persona : ObservableObject
{
    [JsonConstructor]
    public Persona(
        int? id,
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno,
        string? apodo,
        DateTime? fechaNacimiento,
        string? curp,
        string? observacionesCurp,
        string? rfc,
        bool? hablaEspanhol,
        string? especificacionesOcupacion,
        Catalogo? sexo,
        Catalogo? genero,
        Catalogo? religion,
        Catalogo? lengua,
        Catalogo? razonCurp,
        Estado? lugarNacimiento,
        ObservableCollection<Pseudonimo>? pseudonimos,
        ObservableCollection<Catalogo>? nacionalidades,
        ObservableCollection<Catalogo>? gruposVulnerables,
        ObservableCollection<Telefono>? telefonos,
        ObservableCollection<Contacto>? contactos,
        ObservableCollection<SenaParticular> senasParticulares,
        Estudio? estudios,
        ContextoFamiliar? contextoFamiliar,
        Salud? salud,
        Ojos? ojos,
        Cabello? cabello,
        VelloFacial? velloFacial,
        Nariz? nariz,
        Boca? boca,
        Orejas? orejas,
        MediaFiliacionComplementaria? mediaFiliacionComplementaria,
        ObservableCollection<IntervencionQuirurgica> intervencionesQuirurgicas,
        ObservableCollection<EnfermedadPiel> enfermedadesPiel
    )
    {
        Id = id;
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Apodo = apodo;
        FechaNacimiento = fechaNacimiento;
        Curp = curp;
        ObservacionesCurp = observacionesCurp;
        Rfc = rfc;
        HablaEspanhol = hablaEspanhol;
        EspecificacionesOcupacion = especificacionesOcupacion;
        Sexo = sexo;
        Genero = genero;
        Religion = religion;
        Lengua = lengua;
        RazonCurp = razonCurp;
        LugarNacimiento = lugarNacimiento;
        Pseudonimos = pseudonimos;
        Nacionalidades = nacionalidades;
        Telefonos = telefonos;
        Contactos = contactos;
        GruposVulnerables = gruposVulnerables;
        SenasParticulares = senasParticulares;
        Estudios = estudios;
        ContextoFamiliar = contextoFamiliar;
        Salud = salud;
        Ojos = ojos;
        Cabello = cabello;
        VelloFacial = velloFacial;
        Nariz = nariz;
        Boca = boca;
        Orejas = orejas;
        MediaFiliacionComplementaria = mediaFiliacionComplementaria;
        IntervencionesQuirurgicas = intervencionesQuirurgicas;
        EnfermedadesPiel = enfermedadesPiel;
    }

    public Persona()
    {
    }

    /**
     * Nombre completo de la persona.
     */
    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

    /**
     * Atributos de la clase.
     */
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_paterno")]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty, JsonProperty(PropertyName = "apellido_materno")]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;

    [ObservableProperty, JsonProperty(PropertyName = "apodo")]
    private string? _apodo;

    [ObservableProperty, JsonProperty(PropertyName = "fecha_nacimiento")]
    private DateTime? _fechaNacimiento;

    [ObservableProperty, JsonProperty(PropertyName = "curp")]
    private string? _curp;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones_curp")]
    private string? _observacionesCurp;

    [ObservableProperty, JsonProperty(PropertyName = "rfc")]
    private string? _rfc;

    [ObservableProperty, JsonProperty(PropertyName = "habla_espanhol")]
    private bool? _hablaEspanhol;

    [ObservableProperty, JsonProperty(PropertyName = "especificaciones_ocupacion")]
    private string? _especificacionesOcupacion;

    /**
     * Llaves foráneas.
     */
    [ObservableProperty, JsonProperty(PropertyName = "sexo")]
    private Catalogo? _sexo;

    [ObservableProperty, JsonProperty(PropertyName = "genero")]
    private Catalogo? _genero;

    [ObservableProperty, JsonProperty(PropertyName = "religion")]
    private Catalogo? _religion;

    [ObservableProperty, JsonProperty(PropertyName = "lengua")]
    private Catalogo? _lengua;

    [ObservableProperty, JsonProperty(PropertyName = "razon_curp")]
    private Catalogo? _razonCurp;

    [ObservableProperty, JsonProperty(PropertyName = "lugar_nacimiento")]
    private Estado? _lugarNacimiento;

    /**
     * Relaciones.
     */
    [ObservableProperty, JsonProperty(PropertyName = "pseudonimos")]
    private ObservableCollection<Pseudonimo>? _pseudonimos = new();

    [ObservableProperty, JsonProperty(PropertyName = "nacionalidades")]
    private ObservableCollection<Catalogo>? _nacionalidades = new();

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

    [ObservableProperty, JsonProperty("estudios")]
    private Estudio? _estudios;

    [ObservableProperty, JsonProperty("contexto_familiar")]
    private ContextoFamiliar? _contextoFamiliar;

    [ObservableProperty, JsonProperty("salud")]
    private Salud? _salud;

    [ObservableProperty, JsonProperty("ojos")]
    private Ojos? _ojos;

    [ObservableProperty, JsonProperty("cabello")]
    private Cabello? _cabello;

    [ObservableProperty, JsonProperty("vello_facial")]
    private VelloFacial? _velloFacial;

    [ObservableProperty, JsonProperty("nariz")]
    private Nariz? _nariz;

    [ObservableProperty, JsonProperty("boca")]
    private Boca? _boca;

    [ObservableProperty, JsonProperty("orejas")]
    private Orejas? _orejas;

    [ObservableProperty, JsonProperty("media_filiacion_complementaria")]
    private MediaFiliacionComplementaria? _mediaFiliacionComplementaria;

    [ObservableProperty, JsonProperty("intervenciones_quirurgicas")]
    private ObservableCollection<IntervencionQuirurgica> _intervencionesQuirurgicas = [];

    [ObservableProperty, JsonProperty("enfermedades_piel")]
    private ObservableCollection<EnfermedadPiel> _enfermedadesPiel = [];

    // TODO: Añadir ocupaciones

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