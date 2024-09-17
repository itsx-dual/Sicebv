using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Cebv.features.dashboard.filtro_busqueda.Domain;

public class FiltroBusquedaNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<ReporteResponse>> GetReportes(string filter)
    {
        var request = await Client.GetAsync($"/api/reportes?{filter}");
        var response = await request.Content.ReadAsStringAsync();
        ReportesQueryResponse reportes = JsonSerializer.Deserialize<ReportesQueryResponse>(response)!;
        return new ObservableCollection<ReporteResponse>(reportes.Data);
    }
}

[method: JsonConstructor]
class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

public class CargarCatalogo
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
}