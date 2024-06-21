namespace Cebv.core.util.reporte.data;

public class PersonaPostObject
{
    public string LugarNacimientoId { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? PseudonimoNombre { get; set; }
    public string? PseudonimoApellidoPaterno { get; set; }
    public string? PseudonimoApellidoMaterno { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Curp { get; set; }
    public string? ObservacionesCurp { get; set; }
    public string? Rfc { get; set; }
    public string? Ocupacion { get; set; }
    public int? Sexo { get; set; }
    public int? Genero { get; set; }
}