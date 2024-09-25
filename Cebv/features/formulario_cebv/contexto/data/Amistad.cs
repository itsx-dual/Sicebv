using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.contexto.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Amistad : ObservableObject
{
    [JsonConstructor]
    public Amistad(
        int? id,
        int? personaId,
        Catalogo? tipoRedSocial,
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno,
        string? apodo,
        string? telefono,
        string? usuarioRedSocial,
        string? observacionesRedSocial
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoRedSocial = tipoRedSocial;
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Apodo = apodo;
        Telefono = telefono;
        UsuarioRedSocial = usuarioRedSocial;
        ObservacionesRedSocial = observacionesRedSocial;
    }

    public Amistad()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_red_social")]
    private Catalogo? _tipoRedSocial;

    [ObservableProperty, JsonProperty("nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty("apellido_paterno")]
    private string? _apellidoPaterno;

    [ObservableProperty, JsonProperty("apellido_materno")]
    private string? _apellidoMaterno;

    [ObservableProperty, JsonProperty("apodo")]
    private string? _apodo;

    [ObservableProperty, JsonProperty("telefono")]
    private string? _telefono;

    [ObservableProperty, JsonProperty("usuario_red_social")]
    private string? _usuarioRedSocial;

    [ObservableProperty, JsonProperty("observaciones_red_social")]
    private string? _observacionesRedSocial;
}