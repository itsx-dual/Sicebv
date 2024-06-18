using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;

namespace Cebv.features.formulario_cebv.prendas.domain;

public class PrendasNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetGruposPertenencias()
    {
        var request = await Client.GetAsync("api/grupos-pertenencias");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetPertenencias(int pertenenciaId)
    {
        var request = await Client.GetAsync($"api/pertenencias?search={pertenenciaId}");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetColores()
    {
        var request = await Client.GetAsync("api/colores");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}