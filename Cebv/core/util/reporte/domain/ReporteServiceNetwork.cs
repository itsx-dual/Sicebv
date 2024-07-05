using System.Net.Http;
using System.Text;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteResponse : ObservableObject
{
    [JsonConstructor]
    public ReporteResponse(Reporte data)
    {
        Data = data;
    }

    [ObservableProperty] private Reporte? _data;
}

public partial class ReporteServiceNetwork
{
    private static HttpClient Client = CebvClientHandler.SharedClient;

    public static async Task<Reporte> ShowReporte(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Reportes>(json)?.Data;
    }

    public static async Task<Reporte> Sync(Reporte reporte)
    {
        var json = JsonConvert.SerializeObject(reporte);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Entrada: {JObject.Parse(json).ToString(Formatting.Indented)}\n");

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/actualizar/reporte", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        
        var response = await Client.SendAsync(request);
        json = await response.Content.ReadAsStringAsync();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Salida: {JObject.Parse(json).ToString(Formatting.Indented)}");
        Console.ForegroundColor = ConsoleColor.White;
        return JsonConvert.DeserializeObject<ReporteResponse>(json)?.Data!;
    }
}