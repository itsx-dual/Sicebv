using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;

namespace Cebv.features.formulario_cebv.control_ogpi.domain;

public class ControlOgpiNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Catalogo>> GetEstatusPersonas()
    {
        var request = await Client.GetAsync("api/estatus-personas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }
}