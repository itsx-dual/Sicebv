using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.features.reporte.data;
using HttpClientHandler = Cebv.core.network.HttpClientHandler;

namespace Cebv.features.reporte.domain;

public class ReporteNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<ReporteWrapped> GetReportes()
    {
        var response = await Client.GetAsync("api/reportes");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        ReporteWrapped reportes = JsonSerializer.Deserialize<ReporteWrapped>(jsonResponse);

        return reportes;
    }

    public static async Task<Object> GetTiposReportes()
    {
        var response = await Client.GetAsync("api/tipos-reportes");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        CatalogoWrapped prettyResponse = JsonSerializer.Deserialize<CatalogoWrapped>(jsonResponse);

        ObservableCollection<Catalogo> tiposReportes = new ObservableCollection<Catalogo>();

        foreach (var tipoReporte in prettyResponse?.data)
        {
            tiposReportes.Add(tipoReporte);
        }

        return tiposReportes;
    }

    public static async Task<Object> GetAreas()
    {
        var response = await Client.GetAsync("api/areas");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        CatalogoWrapped prettyResponse = JsonSerializer.Deserialize<CatalogoWrapped>(jsonResponse);

        ObservableCollection<Catalogo> areas = new ObservableCollection<Catalogo>();

        foreach (var area in prettyResponse?.data)
        {
            areas.Add(area);
        }

        return areas;
    }
}