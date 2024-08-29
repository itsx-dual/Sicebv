using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Estudio : ObservableObject
{
    [JsonConstructor]
    public Estudio(
        int? id,
        int? personaId,
        Catalogo? escolaridad,
        Catalogo? estatusEscolaridad,
        int? direccionId,
        string? nombreInstitucion
    )
    {
        Id = id;
        PersonaId = personaId;
        Escolaridad = escolaridad;
        EstatusEscolaridad = estatusEscolaridad;
        DireccionId = direccionId;
        NombreInstitucion = nombreInstitucion;
    }

    public Estudio()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("escolaridad")]
    private Catalogo? _escolaridad;

    [ObservableProperty, JsonProperty("estatus_escolaridad")]
    private Catalogo? _estatusEscolaridad;

    [ObservableProperty, JsonProperty("direccion_id")]
    private int? _direccionId;

    [ObservableProperty, JsonProperty("nombre_institucion")]
    private string? _nombreInstitucion;
}