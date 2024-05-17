using System.Net.Http;
using System.Text.Json;
using Cebv.features.dashboard.reportes_no_terminados.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;

namespace Cebv.features.dashboard.reportes_no_terminados.domain;

public class ReportesNoTerminadosRequest
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<dynamic?> GET()
    {
        var response = await Client.GetAsync("api/reportes?search=0");
        string jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
        var reportes = JsonSerializer.Deserialize<Reporte>(jsonResponse);
        return reportes;
    }
}