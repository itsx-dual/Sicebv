using Cebv.core.modules.sistema.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.desaparecido.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Folio : ObservableObject
{
    [JsonConstructor]
    public Folio(
        int? id,
        int? personaId,
        int? reporteId,
        UserAdmin user,
        string? folioCebv,
        string? folioFub,
        string? autoridadIngresaFub,
        DateTime createdAt,
        DateTime updatedAt
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

    public Folio()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("persona_id")]
    private int? _personaId;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("user")]
    private UserAdmin? _user;

    [ObservableProperty, JsonProperty("folio_cebv")]
    private string? _folioCebv;

    [ObservableProperty, JsonProperty("folio_fub")]
    private string? _folioFub;

    [ObservableProperty, JsonProperty("autoridad_ingresa_fub")]
    private string? _autoridadIngresaFub;

    [ObservableProperty, JsonProperty("created_at")]
    private DateTime _createdAt;

    [ObservableProperty, JsonProperty("updated_at")]
    private DateTime _updatedAt;
}