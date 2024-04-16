using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.features.Prendas.data.Prendas_Vestir;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;

namespace Cebv.features.prendas.domain;

public class PrendasVestirNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;
    
    public static async Task<Dictionary<string, PrendaColor>> GetColor()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/color");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<PrendaColor> Prenda_color = JsonSerializer.Deserialize<List<PrendaColor>>(jsonResponse);
        Dictionary<string, PrendaColor> dic = new Dictionary<string, PrendaColor>();
        return dic;
    }
    
    public static async Task<Dictionary<string, Grupo_pertenencias>> GetGrupo_pertenencias()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/Grupo_pertenencias");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<Grupo_pertenencias> Grupopertenencia = JsonSerializer.Deserialize<List<Grupo_pertenencias>>(jsonResponse);
        Dictionary<string, Grupo_pertenencias> dic = new Dictionary<string, Grupo_pertenencias>();
        return dic;
    }
    
    public static async Task<Dictionary<string, Material>> GetGrupo_pertenencias()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/Grupo_pertenencias");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<Material> material = JsonSerializer.Deserialize<List<Material>>(jsonResponse);
        Dictionary<string, Material> dic = new Dictionary<string, Material>();
        return dic;
    }
    
    public static async Task<Dictionary<string, Pertenencias>> GetGrupo_pertenencias()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/Grupo_pertenencias");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<Pertenencias> pertenencias = JsonSerializer.Deserialize<List<Pertenencias>>(jsonResponse);
        Dictionary<string, Pertenencias> dic = new Dictionary<string, Pertenencias>();
        return dic;
    }
    
    public static async Task<Dictionary<string, Prendas_Vestir_Pertenencias>> GetPVP()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/Grupo_pertenencias");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<Prendas_Vestir_Pertenencias> pvp = JsonSerializer.Deserialize<List<Prendas_Vestir_Pertenencias>>(jsonResponse);
        Dictionary<string, Prendas_Vestir_Pertenencias> dic = new Dictionary<string, Prendas_Vestir_Pertenencias>();
        return dic;
    }
}