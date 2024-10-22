using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class PersonaCompact : ObservableObject
{
    [JsonConstructor]
    public PersonaCompact(
        int? id,
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno,
        Catalogo? sexo,
        int? edad,
        string? curp,
        DateTime? fechaNacimiento
    )
    {
        Id = id;
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Sexo = sexo;
        Edad = edad;
        Curp = curp;
        FechaNacimiento = fechaNacimiento;
    }

    public PersonaCompact()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty("apellido_paterno")]
    private string? _apellidoPaterno;

    [ObservableProperty, JsonProperty("apellido_materno")]
    private string? _apellidoMaterno;

    [ObservableProperty, JsonProperty("sexo")]
    private Catalogo? _sexo;

    [ObservableProperty, JsonProperty("edad")]
    private int? _edad;

    [ObservableProperty, JsonProperty("curp")]
    private string? _curp;

    [ObservableProperty, JsonProperty("fecha_nacimiento")]
    private DateTime? _fechaNacimiento;
}