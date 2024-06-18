using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.domain;

public class MediaFiliacionComplementariaNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetTiposMentones()
    {
        var request = await Client.GetAsync("api/tipos-mentones");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetIntervencionesQuirurgicas()
    {
        var request = await Client.GetAsync("api/intervenciones-quirurgicas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetEnfermedadesPiel()
    {
        var request = await Client.GetAsync("api/enfermedades-pieles");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}