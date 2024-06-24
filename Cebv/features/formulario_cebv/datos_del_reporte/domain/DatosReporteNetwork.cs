using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.datos_del_reporte.domain;

[method: JsonConstructor]
public class MedioConocimientoCall(ObservableCollection<MedioConocimiento> data)
{
    public ObservableCollection<MedioConocimiento> Data = data;
}

public class TiposMediosCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

public class DatosReporteNetwork
{
    private static HttpClient Client = CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<MedioConocimiento>> GetMedios(int? id)
    {
        if (id == null) return null;
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/medios?search={id}", UriKind.Relative),
            Method = HttpMethod.Get
        };
        
        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<MedioConocimientoCall>(json)?.Data!;
    }
    
    public static async Task<ObservableCollection<Catalogo>> GetTiposMedios()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/tipos-medios", UriKind.Relative),
            Method = HttpMethod.Get
        };
        
        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TiposMediosCall>(json)?.Data!;
    }
}