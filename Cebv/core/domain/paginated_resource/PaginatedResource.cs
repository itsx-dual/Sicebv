using Newtonsoft.Json;

namespace Cebv.core.domain.paginated_resource;

[JsonObject]
public class PaginatedResource<T>
{
    [JsonProperty(PropertyName = "data")] public T Data;
    [JsonProperty(PropertyName = "links")] public Links Links;
    [JsonProperty(PropertyName = "meta")] public Meta Meta;
}
