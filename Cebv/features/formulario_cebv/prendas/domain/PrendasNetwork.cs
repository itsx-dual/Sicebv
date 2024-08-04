using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using System.Text.Json;
using Catalogo = Cebv.core.data.Catalogo;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.features.formulario_cebv.prendas.domain;

public class PrendasNetworkEncuadrePreeliminar
{
    [method: JsonConstructor]
    class PertenenciaCall(ObservableCollection<Pertenencia> data)
    {
        public ObservableCollection<Pertenencia> Data = data;
    }
    
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Pertenencia>> GetPertenencias(int pertenenciaId)
    {
        var request = await Client.GetAsync($"api/pertenencias?search={pertenenciaId}");
        var json = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PertenenciaCall>(json)?.Data!;
    }
    
    public static async Task<ObservableCollection<Pertenencia>> GetPertenencias()
    {
        var request = await Client.GetAsync("api/pertenencias");
        var json = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PertenenciaCall>(json)?.Data!;
    }
}

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

    public static async Task<ObservableCollection<Catalogo>> GetPertenencias()
    {
        var request = await Client.GetAsync("api/pertenencias");
        
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