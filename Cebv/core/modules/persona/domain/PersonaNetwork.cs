using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;

namespace Cebv.core.modules.persona.domain;

public class PersonaNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<ObservableCollection<Catalogo>> GetSexos()
    {
        var request = await Client.GetAsync("api/sexos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetGeneros()
    {
        var request = await Client.GetAsync("api/generos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetEscolaridades()
    {
        var request = await Client.GetAsync("api/escolaridades");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetEstadosConyugales()
    {
        var request = await Client.GetAsync("api/estados-conyugales");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}