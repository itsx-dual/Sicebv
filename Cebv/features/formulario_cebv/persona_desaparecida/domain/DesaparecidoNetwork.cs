using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.domain.paginated_resource;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.persona_desaparecida.domain;

public class DesaparecidoNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;
    
    public static async Task<ObservableCollection<Ocupacion>> OcupacionesDadoTipo(int? tipo_ocupacion_id)
    {
        var request = await Client.GetAsync($"/api/ocupaciones?search={tipo_ocupacion_id}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<Ocupacion>>>(response)?.Data ?? [];
    }
}