using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.features.formulario_cebv.senas_particulares.data;

namespace Cebv.features.formulario_cebv.senas_particulares.domain;

public class SenasParticularesNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<List<SenasParticularesData>> GetSenasParticulares()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/region_cuerpo");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        SenasParticulares senasParticulares = JsonSerializer.Deserialize<SenasParticulares>(jsonResponse)!;
        if (response.IsSuccessStatusCode)
        {
            return senasParticulares.data;
        }

        return null;
    }

    public static async Task<Dictionary<string, RegionCuerpo>> GetRegionCuerpo()
    {
        using HttpResponseMessage response = await Client.GetAsync("api/catalogos/region_cuerpo");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<RegionCuerpo> regiones_cuerpo = JsonSerializer.Deserialize<List<RegionCuerpo>>(jsonResponse)!;
        Dictionary<string, RegionCuerpo> dic = new Dictionary<string, RegionCuerpo>();

        foreach (var region in regiones_cuerpo)
        {
            dic.Add(region.color, region);
        }

        return dic;
    }
    
    public static async Task<List<TipoSenas>> GetTipo()
    {
        using HttpResponseMessage response = await Client.GetAsync($"api/catalogos/tipo");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<TipoSenas> tipo = JsonSerializer.Deserialize<List<TipoSenas>>(jsonResponse)!;
        return tipo;
    }
    
    public static async Task<Dictionary<string, LadoSenas>> GetLado()
    {
        using HttpResponseMessage response = await Client.GetAsync($"api/catalogos/lado");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<LadoSenas> lado = JsonSerializer.Deserialize<List<LadoSenas>>(jsonResponse)!;
        Dictionary<string, LadoSenas> dic_lado = new Dictionary<string, LadoSenas>();

        foreach (LadoSenas lad in lado)
        {
            dic_lado.Add(lad.color, lad);
        }

        return dic_lado;
    }
    
    public static async Task<List<VistaSenas>> GetVista()
    {
        using HttpResponseMessage response = await Client.GetAsync($"api/catalogos/vista");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        List<VistaSenas> vista = JsonSerializer.Deserialize<List<VistaSenas>>(jsonResponse)!;
        return vista;
    }
    
    public static async Task<bool> PostBulkSenasParticulares(List<SenasParticularesData> data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await Client.PostAsync($"api/bulk_insert/senas_particulares", content);
        return response.IsSuccessStatusCode;
    }
    
    public static async Task<bool> PostSenasParticulares(SenasParticularesData data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await Client.PostAsync($"api/senas_particulares", content);
        return response.IsSuccessStatusCode;
    }
}