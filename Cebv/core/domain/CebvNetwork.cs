using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.core.domain;

class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

public static class CebvNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
}