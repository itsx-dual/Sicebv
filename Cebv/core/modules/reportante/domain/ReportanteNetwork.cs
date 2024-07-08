using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Newtonsoft.Json;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.core.modules.reportante.domain;

[method: JsonConstructor]
class CatalogoCall(ObservableCollection<Catalogo> data)
{
    public ObservableCollection<Catalogo> Data = data;
}

class EstadosCall(ObservableCollection<Estado> data)
{
    public ObservableCollection<Estado> Data = data;
}

class MunicipiosCall(ObservableCollection<Municipio> data)
{
    public ObservableCollection<Municipio> Data = data;
}

class AsentamientosCall(ObservableCollection<Asentamiento> data)
{
    public ObservableCollection<Asentamiento> Data = data;
}

public static class ReportanteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<Catalogo>> GetCatalogo(string catalogo)
    {
        var request = await Client.GetAsync($"/api/{catalogo}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CatalogoCall>(response)?.Data!;
    }
    public static async Task<ObservableCollection<Estado>> GetEstados()
    {
        var request = await Client.GetAsync("/api/estados");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<EstadosCall>(response)?.Data!;
    }
    
    public static async Task<ObservableCollection<Municipio>> GetMunicipiosDeEstado(string estado_id)
    {
        var request = await Client.GetAsync($"/api/municipios?search={estado_id}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<MunicipiosCall>(response)?.Data!;
    }
    
    public static async Task<ObservableCollection<Asentamiento>> GetAsentamientosDeMunicipio(string municipio_id)
    {
        var request = await Client.GetAsync($"/api/asentamientos?search={municipio_id}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AsentamientosCall>(response)?.Data!;
    }
}