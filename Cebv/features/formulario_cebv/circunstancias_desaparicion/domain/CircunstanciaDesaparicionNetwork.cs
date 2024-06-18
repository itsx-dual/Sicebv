using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;

public class CircunstanciaDesaparicionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<TipoHipotesis>> GetTiposHipotesis()
    {
        var request = await Client.GetAsync("api/tipos-hipotesis");

        var response = await request.Content.ReadAsStringAsync();

        TiposHipotesisWrapped jsonResponse = JsonSerializer.Deserialize<TiposHipotesisWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetSitios()
    {
        var request = await Client.GetAsync("api/sitios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}