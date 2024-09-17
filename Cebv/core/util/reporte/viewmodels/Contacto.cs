using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Contacto : ObservableObject
{
    [JsonConstructor]
    public Contacto(int? id, int? persona_id, string? tipo, string? nombre, string? observaciones,
        Catalogo tipo_red_social)
    {
        Id = id;
        PersonaId = persona_id;
        Tipo = tipo;
        Nombre = nombre;
        TipoRedSocial = tipo_red_social;
        Observaciones = observaciones;
    }

    public Contacto()
    {
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty(PropertyName = "persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty(PropertyName = "tipo")]
    private string? _tipo;

    [ObservableProperty, JsonProperty(PropertyName = "nombre")]
    private string? _nombre;

    [ObservableProperty, JsonProperty(PropertyName = "observaciones")]
    private string? _observaciones;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_red_social")]
    private Catalogo? _tipoRedSocial;
}