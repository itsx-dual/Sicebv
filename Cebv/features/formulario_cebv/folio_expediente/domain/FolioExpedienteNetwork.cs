using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.folio_expediente.data;

namespace Cebv.features.formulario_cebv.folio_expediente.domain;

public class FolioExpedienteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetTiposReportes()
    {
        var request = await Client.GetAsync("api/tipos-reportes");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetAreas()
    {
        var request = await Client.GetAsync("api/areas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Folio>> SetFolio(int reporteId)
    {
        var asignarFolio = await Client.GetAsync($"api/reportes/asignar_folio/{reporteId}");
        var test = await asignarFolio.Content.ReadAsStringAsync();
        Console.WriteLine(test);
        
        var consultarFolio = await Client.GetAsync($"api/reportes/ver_folio/{reporteId}");
        var response = await consultarFolio.Content.ReadAsStringAsync();
        Console.WriteLine(response);
        
        FoliosWrapped jsonResponse = JsonSerializer.Deserialize<FoliosWrapped>(response)!;
        return jsonResponse.Data;
    }
}