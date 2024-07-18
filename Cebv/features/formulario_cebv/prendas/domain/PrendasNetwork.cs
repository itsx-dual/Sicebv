using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.prendas.domain;

[method: JsonConstructor]
class PertenenciaCall(ObservableCollection<Pertenencia> data)
{
    public ObservableCollection<Pertenencia> Data = data;
}

public class PrendasNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Pertenencia>> GetPertenencias(int pertenenciaId)
    {
        var request = await Client.GetAsync($"api/pertenencias?search={pertenenciaId}");
        var json = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PertenenciaCall>(json)?.Data!;
    }
}