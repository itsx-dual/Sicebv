using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Persona = Cebv.core.util.reporte.viewmodels.Persona;
using TipoHipotesis = Cebv.core.util.reporte.viewmodels.TipoHipotesis;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;

class PersonaCall(ObservableCollection<Persona> data)
{
    public ObservableCollection<Persona> Data = data;
}

public class CircunstanciaDesaparicionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<TipoHipotesis>> GetTiposHipotesis()
    {
        var request = await Client.GetAsync("api/tipos-hipotesis");

        var response = await request.Content.ReadAsStringAsync();

        TiposHipotesisWrappedNewtonsoft jsonResponse =
            JsonSerializer.Deserialize<TiposHipotesisWrappedNewtonsoft>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetSitios()
    {
        var request = await Client.GetAsync("api/sitios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Persona>> SearchPersona(
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno
    )
    {
        var request = await Client.GetAsync($"api/personas?filter[nombre]={nombre}" +
                                            $"&filter[apellido_paterno]={apellidoPaterno}" +
                                            $"&filter[apellido_materno]={apellidoMaterno}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PersonaCall>(response)?.Data!;
    }
}