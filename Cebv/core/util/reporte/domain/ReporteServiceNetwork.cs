using System.Net.Http;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
{
    private static HttpClient Client = CebvClientHandler.SharedClient;
    public static IReporteService ReporteService { get; set; }
    public static ISnackbarService Snackbar { get; set; }

    public ReporteServiceNetwork(IReporteService reporteService, ISnackbarService snackbarService)
    {
        ReporteService = reporteService;
        Snackbar = snackbarService;
    }

    private static int? NullableBoolToInt(bool? boolean)
    {
        return boolean switch
        {
            true => 1,
            false => 0,
            _ => null
        };
    }

    public static async Task<ReporteResponse> ShowReporte(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ReporteQueryResponse>(json)!.Data;
    }

    public static async Task<ObservableCollection<HechosDesaparicionResponse>?> GetHechosDesaparicion(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/hechos-desapariciones?filter[reporte_id]={id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        var hechosDesaparicion = JsonSerializer.Deserialize<HechosDesaparicionQueryResponse>(json)!.Data;

        return hechosDesaparicion;
    }

    public static async void PostHechosDesaparicion(ModoTiempoLugarPost informacion)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/hechos-desapariciones", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "hechos_desaparicion", informacion.DescripcionHechosDesaparicion! },
                { "reporte_id", "1" },
            })
        };

        using var response = await Client.SendAsync(request);

        Console.WriteLine(response.IsSuccessStatusCode ? "Jalloooooo" : "No jalo");
    }
}