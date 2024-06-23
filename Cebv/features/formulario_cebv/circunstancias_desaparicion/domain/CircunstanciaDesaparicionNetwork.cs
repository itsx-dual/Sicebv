using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;

public class CircunstanciaDesaparicionNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<TipoHipotesis>> GetTiposHipotesis()
    {
        var request = await Client.GetAsync("api/tipos-hipotesis");

        var response = await request.Content.ReadAsStringAsync();

        TiposHipotesisWrapped jsonResponse = JsonSerializer.Deserialize<TiposHipotesisWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetSitios()
    {
        var request = await Client.GetAsync("api/sitios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task<ObservableCollection<Persona>> BuscarPersona(string? nombre, string? apellidoPaterno,
        string? apellidoMaterno)
    {
        var request = await Client.GetAsync($"api/personas?filter[nombre]={nombre}" +
                                            $"&filter[apellido_paterno]={apellidoPaterno}" +
                                            $"&filter[apellido_materno]={apellidoMaterno}");

        var response = await request.Content.ReadAsStringAsync();

        PersonasWrapped jsonResponse = JsonSerializer.Deserialize<PersonasWrapped>(response)!;

        return jsonResponse.Data!;
    }
    
    public static async Task<ObservableCollection<Persona>> BuscarPersona2(string? nombre, string? apellidoPaterno,
        string? apellidoMaterno)
    {
        var request = await Client.GetAsync($"api/personas?filter[nombre]={nombre}" +
                                            $"&filter[apellido_paterno]={apellidoPaterno}" +
                                            $"&filter[apellido_materno]={apellidoMaterno}");

        var response = await request.Content.ReadAsStringAsync();

        PersonasWrapped jsonResponse = JsonSerializer.Deserialize<PersonasWrapped>(response)!;

        return jsonResponse.Data!;
    }
}