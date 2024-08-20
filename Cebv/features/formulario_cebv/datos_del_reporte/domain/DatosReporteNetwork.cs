using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.domain.paginated_resource;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.datos_del_reporte.domain;

public class DatosReporteNetwork
{
    private static HttpClient Client = CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<MedioConocimiento>> GetMedios(int? id)
    {
        if (id == null) return null;
        using var response = await Client.GetAsync($"/api/medios?search={id}");
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<MedioConocimiento>>>(json)?.Data!;
    }
}