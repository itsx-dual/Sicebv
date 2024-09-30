using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.persona.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Embarazo : ObservableObject
{
    [JsonConstructor]
    public Embarazo(
        int? id,
        int? personaId,
        bool estaEmbarazada,
        int? meses
    )
    {
        Id = id;
        PersonaId = personaId;
        EstaEmbarazada = estaEmbarazada;
        Meses = meses;
    }

    public Embarazo()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("esta_embarazada")]
    private bool _estaEmbarazada;

    [ObservableProperty, JsonProperty("meses")]
    private int? _meses;

    partial void OnMesesChanged(int? value)
    {
        Meses = value switch
        {
            < 0 => 0,
            > 9 => 9,
            _ => Meses
        };
    }
}