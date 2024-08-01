using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.senas_particulares.domain;

[method: JsonConstructor]
class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

[method: JsonConstructor]
class CatalogoColorCall(ObservableCollection<CatalogoColor> data)
{
    public ObservableCollection<CatalogoColor> Data = data;
}

public class SenasParticularesNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
    
    public static async Task<ObservableCollection<CatalogoColor>> GetCatalogoColor(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoColorCall>(response)?.Data!;
    }
}