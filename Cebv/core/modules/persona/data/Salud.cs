using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Salud : ObservableObject
{
    [JsonConstructor]
    public Salud(
        int? id,
        int? personaId,
        Catalogo? tipoSangre,
        Catalogo? complexion,
        Catalogo? colorPiel,
        Catalogo? formaCara,
        int? estaturaCentimetros,
        float? pesoKilogramos,
        string? factorRhesus
    )
    {
        Id = id;
        PersonaId = personaId;
        TipoSangre = tipoSangre;
        Complexion = complexion;
        ColorPiel = colorPiel;
        FormaCara = formaCara;
        EstaturaCentimetros = estaturaCentimetros;
        PesoKilogramos = pesoKilogramos;
        FactorRhesus = factorRhesus;
    }

    public Salud()
    {
    }


    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("tipo_sangre")]
    private Catalogo? _tipoSangre;

    [ObservableProperty, JsonProperty("complexion")]
    private Catalogo? _complexion;

    [ObservableProperty, JsonProperty("color_piel")]
    private Catalogo? _colorPiel;

    [ObservableProperty, JsonProperty("forma_cara")]
    private Catalogo? _formaCara;

    [ObservableProperty, JsonProperty("estatura_centimetros")]
    private int? _estaturaCentimetros;

    [ObservableProperty, JsonProperty("peso_kilogramos")]
    private float? _pesoKilogramos;

    [ObservableProperty, JsonProperty("factor_rhesus")]
    private string? _factorRhesus;
}