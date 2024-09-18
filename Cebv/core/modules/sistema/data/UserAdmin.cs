using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.modules.sistema.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class UserAdmin : ObservableObject
{
    [JsonConstructor]
    public UserAdmin(
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

    public UserAdmin()
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