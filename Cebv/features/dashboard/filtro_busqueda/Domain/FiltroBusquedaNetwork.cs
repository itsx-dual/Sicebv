using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.features.dashboard.filtro_busqueda.Domain;

class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

public static class CatalogosNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
}

public class FiltroBusquedaNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<ReporteResponse>> GetReportes()
    {
        var request = await Client.GetAsync("api/reportes");
        var response = await request.Content.ReadAsStringAsync();
        ReportesQueryResponse reportes = JsonSerializer.Deserialize<ReportesQueryResponse>(response)!;
        return new ObservableCollection<ReporteResponse>(reportes.Data);
    }
    
    public static async Task<ObservableCollection<ReporteResponse>> GetReportes(string filter)
    {
        var request = await Client.GetAsync($"/api/reportes?{filter}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ObservableCollection<ReporteResponse>>(response)!;
    }
}