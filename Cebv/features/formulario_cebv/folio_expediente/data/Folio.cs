using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.folio_expediente.data;

public class FoliosWrapped
{
    [JsonPropertyName("data")] public ObservableCollection<Folio> Data { get; set; } = new();
}

public class Folio
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("persona_id")] public int PersonaId { get; set; }
    [JsonPropertyName("reporte_id")] public int ReporteId { get; set; }
    [JsonPropertyName("user")] public User User { get; set; } = new();
    [JsonPropertyName("folio_cebv")] public string FolioCebv { get; set; } = String.Empty;
    [JsonPropertyName("folio_fub")] public string FolioCFub { get; set; } = String.Empty;
    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public partial class FolioPretty : ObservableObject
{
    [Newtonsoft.Json.JsonConstructor]
    public FolioPretty(
        int? id,
        int? personaId,
        int? reporteId,
        UserPretty? user,
        string? folioCebv,
        string? folioFub,
        string? autoridadIngresaFub,
        DateTime? createdAt,
        DateTime? updatedAt
    )
    {
        Id = id;
        PersonaId = personaId;
        ReporteId = reporteId;
        User = user;
        FolioCebv = folioCebv;
        FolioFub = folioFub;
        AutoridadIngresaFub = autoridadIngresaFub;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public FolioPretty()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("user")]
    private UserPretty? _user;

    [ObservableProperty, JsonProperty("folio_cebv")]
    private string? _folioCebv;

    [ObservableProperty, JsonProperty("folio_fub")]
    private string? _folioFub;
    
    [ObservableProperty, JsonProperty("autoridad_ingresa_fub")]
    private string? _autoridadIngresaFub;
    
    [ObservableProperty, JsonProperty("created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty("updated_at")]
    private DateTime? _updatedAt;
}

[JsonObject(MemberSerialization.OptIn)]
public partial class UserPretty : ObservableObject
{
    [Newtonsoft.Json.JsonConstructor]
    public UserPretty(
        int? id,
        string? name,
        string? email,
        string? status
    )
    {
        Id = id;
        Name = name;
        Email = email;
        Status = status;
    }

    public UserPretty()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("name")]
    private string? _name;

    [ObservableProperty, JsonProperty("email")]
    private string? _email;

    [ObservableProperty, JsonProperty("status")]
    private string? _status;
}