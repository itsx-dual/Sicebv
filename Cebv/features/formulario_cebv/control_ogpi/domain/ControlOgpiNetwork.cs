using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Newtonsoft.Json;
using Catalogo = Cebv.core.data.Catalogo;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.features.formulario_cebv.control_ogpi.domain;

class EstatusPersonaCall(ObservableCollection<EstatusPersona> data)
{
    public ObservableCollection<EstatusPersona> Data = data;
}

public class ControlOgpiNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<EstatusPersona>> GetEstatusPersonas()
    {
        var request = await Client.GetAsync("/api/estatus-personas");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<EstatusPersonaCall>(response)?.Data!;
    }

    public static async Task SetFolioFub(FolioPretty folio)
    {
        HttpRequestMessage folioRequest = new()
        {
            RequestUri = new Uri($"api/folios/{folio.Id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var folioResponse = await Client.SendAsync(folioRequest);

        folioResponse.EnsureSuccessStatusCode();

        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                folio_fub = folio.FolioFub,
                autoridad_ingresa_fub = folio.AutoridadIngresaFub
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await Client.PatchAsync(
            $"api/folios/{folio.Id}",
            jsonContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
}