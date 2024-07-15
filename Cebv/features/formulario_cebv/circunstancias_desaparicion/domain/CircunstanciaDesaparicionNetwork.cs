using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Catalogo = Cebv.core.data.Catalogo;
using Persona = Cebv.core.modules.persona.data.Persona;
using TipoHipotesis = Cebv.core.util.reporte.viewmodels.TipoHipotesis;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;

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

    public static async Task<ObservableCollection<Folio>> GetFoliosPrevios(int? personaId)
    {
        if (personaId is null) return new();
        
        var consultarFolio = await Client.GetAsync($"api/personas/{personaId}/folios");

        var response = await consultarFolio.Content.ReadAsStringAsync();

        Console.WriteLine(response);

        FoliosWrapped jsonResponse = JsonSerializer.Deserialize<FoliosWrapped>(response)!;

        return jsonResponse.Data;
    }
}