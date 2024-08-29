using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.core.domain;

class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

class GenericCall<T>(ObservableCollection<T> data)
{
    public ObservableCollection<T> Data = data;
}

public static class CebvNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string endpoint)
    {
        var request = await Client.GetAsync($"/api/{endpoint}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }

    public static async Task<ObservableCollection<T>> GetByFilter<T>(string endpoint, string filter, string value)
    {
        var request = await Client.GetAsync($"/api/{endpoint}?filter[{filter}]={value}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GenericCall<T>>(response)!.Data;
    }
}