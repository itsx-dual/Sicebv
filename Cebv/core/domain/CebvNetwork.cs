using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain.paginated_resource;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Cebv.features.login.data;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.core.domain;

public static class CebvNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<T>> GetRoute<T>(string endpoint)
    {
        var request = await Client.GetAsync($"/api/{endpoint}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)?.Data!;
    }

    public static async Task<ObservableCollection<T>> GetByFilter<T>(string endpoint, string filter, string value)
    {
        var request = await Client.GetAsync($"/api/{endpoint}?filter[{filter}]={value}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)!.Data;
    }
    
    public static async Task<ObservableCollection<T>> GetById<T>(string endpoint, string id)
    {
        var request = await Client.GetAsync($"/api/{endpoint}/{id}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)!.Data;
    }
    
    public static async Task<dynamic?> SetFolio(string reporteId)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/api/reportes/asignar_folio/{reporteId}", UriKind.Relative),
        };

        try
        {
            using var response = await Client.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return (int)response.StatusCode switch
            {
                200 => JsonSerializer.Deserialize<Success>(jsonResponse),
                401 => JsonSerializer.Deserialize<Error>(jsonResponse)!,
                422 => JsonSerializer.Deserialize<Error>(jsonResponse)!,
                _ => new Error { error = "Error al asignar folios" }
            };
        }
        catch (HttpRequestException)
        {
            return new Error { error = "Error al intentar asignar folios" };
        }
        catch (Exception)
        {
            return new Error { error = "Error desconocido" };
        }
    }
}