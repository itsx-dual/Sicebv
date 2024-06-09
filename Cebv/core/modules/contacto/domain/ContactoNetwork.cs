using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;


namespace Cebv.core.modules.contacto.domain;

public class ContactoNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCompaniasTelefonicas()
    {
        var request = await Client.GetAsync("api/companias-telefonicas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTiposRedesSociales()
    {
        var request = await Client.GetAsync("api/tipos-redes-sociales");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}