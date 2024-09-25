using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain.paginated_resource;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.core.domain;

public static class CebvNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<T>> GetRoute<T>(string endpoint)
    {
        var request = await Client.GetAsync($"/api/{endpoint}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)?.Data!;
    }

    public static async Task<ObservableCollection<T>> GetByFilter<T>(string endpoint, string filter, string value)
    {
        var request = await Client.GetAsync($"/api/{endpoint}?filter[{filter}]={value}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)!.Data;
    }
    
    public static async Task<ObservableCollection<T>> GetById<T>(string endpoint, string id)
    {
        var request = await Client.GetAsync($"/api/{endpoint}/{id}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)!.Data;
    }
}