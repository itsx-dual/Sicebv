using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using CommunityToolkit.Diagnostics;
using Newtonsoft.Json;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.core.modules.reportante.domain;

[method: JsonConstructor]
public class CatalogoCall(ObservableCollection<util.reporte.viewmodels.Catalogo> data)
{
    public ObservableCollection<util.reporte.viewmodels.Catalogo> Data = data;
}

public static class ReportanteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<Object> GetReporte(int reporteId)
    {
        Guard.IsGreaterThan(reporteId, 1);
        var request = await Client.GetAsync($"api/reportes/{reporteId}");
        var response = await request.Content.ReadAsStringAsync();

        ReporteQueryResponse? jsonResponse = JsonSerializer.Deserialize<ReporteQueryResponse>(response);

        ReporteResponse reporteResponse;

        Guard.IsNotNull(jsonResponse);

        reporteResponse = jsonResponse.Data;

        return reporteResponse;
    }

    public static async Task<ObservableCollection<Catalogo>> GetParentescos()
    {
        var request = await Client.GetAsync("/api/parentescos");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
    
    public static async Task PostArea(string nombreArea)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                nombre = nombreArea,
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await Client.PostAsync(
            "api/areas",
            jsonContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetColectivos()
    {
        var request = await Client.GetAsync("api/colectivos");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
}