using Newtonsoft.Json;

namespace Cebv.core.domain.paginated_resource;

[JsonObject]
public class Page
{
    [JsonConstructor]
    public Page(string? url, string? label, bool? active)
    {
        Url = url;
        Label = label;
        Active = active;
    }
    
    [JsonProperty(PropertyName = "url")]
    public string? Url;
    
    [JsonProperty(PropertyName = "label")]
    public string? Label;

    [JsonProperty(PropertyName = "active")]
    public bool? Active;
}