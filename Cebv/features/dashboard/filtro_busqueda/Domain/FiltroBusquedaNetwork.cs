using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.features.dashboard.filtro_busqueda.Domain;

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
    
    public static async Task<ObservableCollection<ReporteResponse>> GetReportes(string filtros)
    {
        var request = await Client.GetAsync($"api/reportes{filtros}");
        var response = await request.Content.ReadAsStringAsync();
        ReportesQueryResponse reportes = JsonSerializer.Deserialize<ReportesQueryResponse>(response)!;
        return new ObservableCollection<ReporteResponse>(reportes.Data);
    }
}