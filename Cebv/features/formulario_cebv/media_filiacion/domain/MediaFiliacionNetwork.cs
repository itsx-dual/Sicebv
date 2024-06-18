using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;

namespace Cebv.features.formulario_cebv.media_filiacion.domain;

public class MediaFiliacionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetComplexiones()
    {
        var request = await Client.GetAsync("api/complexiones");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetColoresPieles()
    {
        var request = await Client.GetAsync("api/colores-pieles");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetFormasCaras()
    {
        var request = await Client.GetAsync("api/formas-caras");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Ojos
     */
    public static async Task<ObservableCollection<Catalogo>> GetColoresOjos()
    {
        var request = await Client.GetAsync("api/colores-ojos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetFormasOjos()
    {
        var request = await Client.GetAsync("api/formas-ojos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTamanosOjos()
    {
        var request = await Client.GetAsync("api/tamanos-ojos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Cabello
     */
    public static async Task<ObservableCollection<Catalogo>> GetTiposCalvicies()
    {
        var request = await Client.GetAsync("api/tipos-calvicies");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetColoresCabellos()
    {
        var request = await Client.GetAsync("api/colores-cabellos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTamanosCabellos()
    {
        var request = await Client.GetAsync("api/tamanos-cabellos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTiposCabellos()
    {
        var request = await Client.GetAsync("api/tipos-cabellos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Vello facial
     */
    public static async Task<ObservableCollection<Catalogo>> GetTiposCejas()
    {
        var request = await Client.GetAsync("api/tipos-cejas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Nariz
     */
    public static async Task<ObservableCollection<Catalogo>> GetTiposNarices()
    {
        var request = await Client.GetAsync("api/tipos-narices");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Boca
     */
    public static async Task<ObservableCollection<Catalogo>> GetTamanosBocas()
    {
        var request = await Client.GetAsync("api/tamanos-bocas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTamanosLabios()
    {
        var request = await Client.GetAsync("api/tamanos-labios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    /**
     * Orejas
     */
    public static async Task<ObservableCollection<Catalogo>> GetTamanosOrejas()
    {
        var request = await Client.GetAsync("api/tamanos-orejas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetFormasOrejas()
    {
        var request = await Client.GetAsync("api/formas-orejas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    

}