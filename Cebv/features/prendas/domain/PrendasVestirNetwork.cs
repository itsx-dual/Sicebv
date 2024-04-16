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
    
    
}