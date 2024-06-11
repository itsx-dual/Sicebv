using System.Net.Http;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.features.dashboard.data;

namespace Cebv.features.dashboard.domain;

public class DashboardNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<Usuario> GetUsuarioActualRequest()
    {
        Client.DefaultRequestHeaders.Add("Accept", "application/json");
        using HttpResponseMessage response = await Client.GetAsync("api/usuario_actual");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        if ((int)response.StatusCode == 200)
        {
            Dictionary<string, Usuario>
                usuario = JsonSerializer.Deserialize<Dictionary<string, Usuario>>(jsonResponse)!;
            return usuario["data"];
        }

        return null!;
    }

    public static async Task<List<Reporte>> GetReportesRequest()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/desaparecidos_folio");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        if ((int)response.StatusCode == 200)
        {
            Dictionary<string, List<Reporte>> reportes =
                JsonSerializer.Deserialize<Dictionary<string, List<Reporte>>>(jsonResponse)!;
            return reportes["data"];
        }

        return null!;
    }
}