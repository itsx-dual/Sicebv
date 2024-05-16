using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Documents;
using Cebv.core.data;
using Cebv.features.reporte.data;

namespace Cebv.core.domain;

public class UbicacionNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<ObservableCollection<Estado>> GetEstados()
    {
        var request = await Client.GetAsync("api/estados");

        var response = await request.Content.ReadAsStringAsync();

        EstadosWrapped jsonResponse = JsonSerializer.Deserialize<EstadosWrapped>(response)!;

        return new ObservableCollection<Estado>(jsonResponse.Data);
    }

    public static async Task<ObservableCollection<Municipio>> GetMuncipios(string id)
    {
        var request = await Client.GetAsync($"api/municipios?search={id}");

        var response = await request.Content.ReadAsStringAsync();

        MunicipiosWrapped jsonResponse = JsonSerializer.Deserialize<MunicipiosWrapped>(response)!;

        return new ObservableCollection<Municipio>(jsonResponse.Data);
    }

    public static async Task<ObservableCollection<Asentamiento>> GetAsentamientos(string id)
    {
        var request = await Client.GetAsync($"api/asentamientos?search={id}");

        var response = await request.Content.ReadAsStringAsync();

        AsentamientosWrapped jsonResponse = JsonSerializer.Deserialize<AsentamientosWrapped>(response)!;

        return new ObservableCollection<Asentamiento>(jsonResponse.Data);
    }

    public static async Task<Object> GetZonasEstados()
    {
        var request = await Client.GetAsync("api/zonas-estados");

        var response = await request.Content.ReadAsStringAsync();

        ZonaEstadoWrapped jsonResponse = JsonSerializer.Deserialize<ZonaEstadoWrapped>(response)!;

        ObservableCollection<ZonaEstado> estados = new ObservableCollection<ZonaEstado>();

        foreach (var estado in jsonResponse?.data!) estados.Add(estado);

        return estados;
    }

    public static async Task<ObservableCollection<Catalogo>> GetNacionalidades()
    {
        var request = await Client.GetAsync("api/nacionalidades");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return new ObservableCollection<Catalogo>(jsonResponse.Data);
    }
}