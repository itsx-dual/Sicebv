using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Cebv.core.data;

namespace Cebv.core.modules.persona.data;

public class PersonasWrapped
{
    [JsonPropertyName("data")] public ObservableCollection<Persona>? Data { get; set; }
}

public class PersonaWrapped
{
    [JsonPropertyName("data")] public Persona Data { get; set; }
}

public class Persona
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("lugar_nacimiento_id")]
    public string? LugarNacimientoId { get; set; }

    [JsonPropertyName("nombre")] public string? Nombre { get; set; }

    [JsonPropertyName("apellido_paterno")] public string? ApellidoPaterno { get; set; }

    [JsonPropertyName("apellido_materno")] public string? ApellidoMaterno { get; set; }

    [JsonPropertyName("pseudonimo_nombre")]
    public string? PseudonimoNombre { get; set; }

    [JsonPropertyName("pseudonimo_apellido_paterno")]
    public string? PseudonimoApellidoPaterno { get; set; }

    [JsonPropertyName("pseudonimo_apellido_materno")]
    public string? PseudonimoApellidoMaterno { get; set; }

    [JsonPropertyName("fecha_nacimiento")] public DateTime? FechaNacimiento { get; set; }

    [JsonPropertyName("curp")] public string? Curp { get; set; }

    [JsonPropertyName("observaciones_curp")]
    public string? ObservacionesCurp { get; set; }

    [JsonPropertyName("rfc")] public string? Rfc { get; set; }

    [JsonPropertyName("ocupacion")] public string? Ocupacion { get; set; }

    [JsonPropertyName("sexo")] public Catalogo? Sexo { get; set; }

    [JsonPropertyName("genero")] public Catalogo? Genero { get; set; }

    [JsonPropertyName("apodos")] public List<ApodoResponse>? Apodos { get; set; }

    [JsonPropertyName("nacionalidades")] public List<Catalogo>? Nacionalidades { get; set; }
}

public class PersonaRequest
{
    [JsonPropertyName("lugar_nacimiento_id")]
    public string? LugarNacimientoId { get; set; }

    [JsonPropertyName("nombre")] public string? Nombre { get; set; }

    [JsonPropertyName("apellido_paterno")] public string? ApellidoPaterno { get; set; }

    [JsonPropertyName("apellido_materno")] public string? ApellidoMaterno { get; set; }

    [JsonPropertyName("pseudonimo_nombre")]
    public string? PseudonimoNombre { get; set; }

    [JsonPropertyName("pseudonimo_apellido_paterno")]
    public string? PseudonimoApellidoPaterno { get; set; }

    [JsonPropertyName("pseudonimo_apellido_materno")]
    public string? PseudonimoApellidoMaterno { get; set; }

    [JsonPropertyName("fecha_nacimiento")] public DateTime? FechaNacimiento { get; set; }

    [JsonPropertyName("curp")] public string? Curp { get; set; }

    [JsonPropertyName("observaciones_curp")]
    public string? ObservacionesCurp { get; set; }

    [JsonPropertyName("rfc")] public string? Rfc { get; set; }

    [JsonPropertyName("ocupacion")] public string? Ocupacion { get; set; }

    [JsonPropertyName("sexo_id")] public int? SexoId { get; set; }

    [JsonPropertyName("genero_id")] public int? GeneroId { get; set; }

    [JsonPropertyName("apodos")] public List<ApodoResponse>? Apodos { get; set; }

    [JsonPropertyName("nacionalidades")] public List<Catalogo>? Nacionalidades { get; set; }
}