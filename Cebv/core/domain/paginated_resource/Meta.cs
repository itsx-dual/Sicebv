using Newtonsoft.Json;

namespace Cebv.core.domain.paginated_resource;

[JsonObject]
public class Meta
{
    [JsonConstructor]
    public Meta(
        int? current_page,
        int? from,
        int? last_page,
        string? path,
        int? per_page,
        int? to,
        int? total,
        List<Page> links)
    {
        CurrentPage = current_page;
        From = from;
        LastPage = last_page;
        Path = path;
        PerPage = per_page;
        To = to;
        Total = total;
        Links = links;
    }
    
    [JsonProperty(PropertyName = "current_page")]
    public int? CurrentPage;
    
    [JsonProperty(PropertyName = "from")]
    public int? From;

    [JsonProperty(PropertyName = "last_page")]
    public int? LastPage;

    [JsonProperty(PropertyName = "path")]
    public string? Path;

    [JsonProperty(PropertyName = "per_page")]
    public int? PerPage;

    [JsonProperty(PropertyName = "to")]
    public int? To;
    
    [JsonProperty(PropertyName = "total")]
    public int? Total;

    [JsonProperty(PropertyName = "links")]
    public List<Page>? Links;
}
