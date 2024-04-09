using System.Text.Json.Serialization;

namespace Cebv.features.persona.data;

public class Persona
{
    [property: JsonPropertyName("id")] public int? Id { get; set; }

    [property: JsonPropertyName("nombre")] public string? Nombre { get; set; }

    [property: JsonPropertyName("apellido_paterno")]
    public string? ApellidoPaterno { get; set; }

    [property: JsonPropertyName("apellido_materno")]
    public string? ApellidoMaterno { get; set; }

    [property: JsonPropertyName("fecha_nacimiento")]
    public string? FechaNacimiento { get; set; }

    [property: JsonPropertyName("curp")] public string? Curp { get; set; }

    [property: JsonPropertyName("ocupacion")]
    public string? Ocupacion { get; set; }

    [property: JsonPropertyName("sexo")] public string? Sexo { get; set; }

    [property: JsonPropertyName("genero")] public string? Genero { get; set; }
}