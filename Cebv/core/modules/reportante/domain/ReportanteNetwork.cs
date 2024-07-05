using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.core.modules.reportante.domain;

[method: JsonConstructor]
class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

class EstadosCall(ObservableCollection<Estado> data)
{
    public ObservableCollection<Estado> Data = data;
}

public static class ReportanteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
    public static async Task<ObservableCollection<Estado>> GetEstados()
    {
        var request = await Client.GetAsync("api/estados");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<EstadosCall>(response)?.Data!;
    }
}