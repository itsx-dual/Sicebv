using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.domain.paginated_resource;
using Cebv.features.dashboard.reportes_desaparicion.data;
using Newtonsoft.Json;

namespace Cebv.features.dashboard.reportes_desaparicion.domain;

public class ReportesDesaparicionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<PaginatedResource<List<ReporteCompactResource>>> GetReportes(int page = 1)
    {
        var response = await Client.GetAsync($"/api/reportes?compact&page={page}");
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<List<ReporteCompactResource>>>(json)!;
    }
    
    public static async Task<PaginatedResource<List<ReporteCompactResource>>> GetFavoritos(int page = 1)
    {
        var response = await Client.GetAsync($"/api/favoritos?page={page}");
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<List<ReporteCompactResource>>>(json)!;
    }
}