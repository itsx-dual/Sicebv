using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.core.modules.hipotesis.domain;

[method: JsonConstructor]
public class TiposHipotesisCall(ObservableCollection<TipoHipotesis> data)
{
    public ObservableCollection<TipoHipotesis> Data = data;
}

[method: JsonConstructor]
public class SitiosCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

[method: JsonConstructor]
public class AreasCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

public class HipotesisNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<TipoHipotesis>> GetTiposHipotesis()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/tipos-hipotesis", UriKind.Relative),
            Method = HttpMethod.Get
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TiposHipotesisCall>(json)?.Data!;
    }

    public static async Task<ObservableCollection<Catalogo>> GetSitios()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/sitios", UriKind.Relative),
            Method = HttpMethod.Get
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SitiosCall>(json)?.Data!;
    }

    public static async Task<ObservableCollection<Catalogo>> GetAreas()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/areas", UriKind.Relative),
            Method = HttpMethod.Get
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SitiosCall>(json)?.Data!;
    }
}