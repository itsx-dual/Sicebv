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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (ReferenceEquals(obj, null)) return false;
        if (obj.GetType() != GetType()) return false;

        return Equals((UserAdmin)obj);
    }

    public override int GetHashCode() //wao
    {
        return HashCode.Combine(Id, Email, Status, NombreCompleto);
    }

    private bool Equals(UserAdmin userAdmin)
    {
        return Id == userAdmin.Id &&
               Email == userAdmin.Email &&
               Status == userAdmin.Status &&
               NombreCompleto == userAdmin.NombreCompleto;
    }

    public override string ToString()
    {
        return $"{NombreCompleto}";
    }
}