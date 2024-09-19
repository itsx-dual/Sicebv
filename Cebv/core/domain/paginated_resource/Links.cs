using Newtonsoft.Json;

namespace Cebv.core.domain.paginated_resource;

[JsonObject]
public class Links
{
    [JsonConstructor]
    public Links(
        string first,
        string last,
        string prev,
        string next
    )
    {
        First = first;
        Last = last;
        Prev = prev;
        Next = next;
    }
    
    [JsonProperty(PropertyName = "first")] public string? First; 
    [JsonProperty(PropertyName = "last")] public string? Last; 
    [JsonProperty(PropertyName = "prev")] public string? Prev; 
    [JsonProperty(PropertyName = "next")] public string? Next; 
}