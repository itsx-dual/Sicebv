using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.sistema.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class UserAdmin : ObservableObject
{
    [JsonConstructor]
    public UserAdmin(
        int? id,
        string? email,
        string? status,
        string? nombreCompleto
    )
    {
        Id = id;
        Email = email;
        Status = status;
        NombreCompleto = nombreCompleto;
    }

    public UserAdmin()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("email")]
    private string? _email;

    [ObservableProperty, JsonProperty("status")]
    private string? _status;

    [ObservableProperty, JsonProperty("nombre_completo")]
    private string? _nombreCompleto;

    public override string ToString()
    {
        return $"{NombreCompleto}";
    }
}