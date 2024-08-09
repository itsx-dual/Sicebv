using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public enum TipoExpediente
{
    Directo,
    Indirecto
}

[JsonObject(MemberSerialization.OptIn)]
public partial class Expediente : ObservableObject
{
    [JsonConstructor]
    public Expediente(
        int? id,
        string? tipo,
        Persona? persona,
        Catalogo? parentesco
    )
    {
        Id = id;
        Tipo = tipo;
        Persona = persona;
        Parentesco = parentesco;
    }

    public Expediente()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("tipo")]
    private string? _tipo;

    [ObservableProperty, JsonProperty("persona")]
    private Persona? _persona;

    [ObservableProperty, JsonProperty("parentesco")]
    private Catalogo? _parentesco;
}