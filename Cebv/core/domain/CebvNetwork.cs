using System.Collections.ObjectModel;
using System.Net.Http;
using Cebv.core.domain.paginated_resource;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Cebv.features.login.data;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cebv.core.domain;

public static class CebvNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    /// <summary>
    /// Realiza una llamada asincrónica a un servicio web para obtener un catálogo completo de tipo <typeparamref name="T"/> 
    /// desde un endpoint específico de la API, sin aplicar filtros.
    /// </summary>
    /// <param name="endpoint">Cadena que representa el endpoint del servicio web sin el prefijo "api". 
    /// Este endpoint será utilizado para formar la URL de la solicitud y obtener los datos del catálogo completo.</param>
    /// <typeparam name="T">El tipo de los elementos del catálogo a recuperar. Generalmente puede ser un tipo como <see cref="Catalogo"/>, 
    /// pero puede ser cualquier tipo que se pueda deserializar a partir de la respuesta JSON del servicio web.</typeparam>
    /// <returns>
    /// Una tarea asincrónica que, al completarse, devuelve una colección observable de tipo <see cref="ObservableCollection{T}"/>, 
    /// que contiene todos los elementos del catálogo obtenido desde el servicio web sin aplicar ningún filtro.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Se lanza si la solicitud HTTP al servicio web no se puede completar correctamente, 
    /// como cuando no se puede acceder al endpoint o la respuesta no es válida.
    /// </exception>
    /// <exception cref="JsonSerializationException">
    /// Se lanza si la respuesta del servicio web no puede ser deserializada en el tipo esperado, 
    /// lo que puede ocurrir si la estructura JSON de la respuesta es diferente a la esperada.
    /// </exception>
    public static async Task<ObservableCollection<T>> GetRoute<T>(string endpoint)
    {
        var request = await Client.GetAsync($"/api/{endpoint}");
        var response = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<ObservableCollection<T>>>(response)?.Data!;
    }

    /// <summary>
    /// Realiza una llamada asincrónica a un servicio web para obtener un catálogo de tipo <typeparamref name="T"/> 
    /// filtrado por un campo y valor específicos desde un endpoint de la API.
    /// </summary>
    /// <param name="endpoint">Cadena que representa el endpoint del servicio web sin el prefijo "api". 
    /// Este endpoint será utilizado para formar la URL de la solicitud.</param>
    /// <param name="filter">Cadena que representa el nombre del campo por el cual se desea aplicar el filtro. 
    /// Comúnmente es un identificador como "_id", pero puede ser cualquier campo del catálogo.</param>
    /// <param name="value">Valor que se usará para aplicar el filtro. El valor será comparado con el campo especificado 
    /// en el parámetro <paramref name="filter"/> para filtrar los resultados.</param>
    /// <typeparam name="T">El tipo de los elementos del catálogo a recuperar. Generalmente es un tipo como <see cref="Catalogo"/>, 
    /// pero puede ser cualquier tipo que pueda ser deserializado desde la respuesta JSON del servicio web.</typeparam>
    /// <returns>
    /// Una tarea asincrónica que, al completarse, devuelve una colección observable de tipo <see cref="ObservableCollection{T}"/>, 
    /// que contiene los elementos del catálogo que cumplen con el filtro aplicado. Los elementos se recuperan desde el servicio 
    /// web de acuerdo con el endpoint, filtro y valor proporcionados.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Se lanza si la solicitud HTTP al servicio web no se puede completar correctamente, 
    /// como cuando no se puede acceder al endpoint o la respuesta no es válida.
    /// </exception>
    /// <exception cref="JsonSerializationException">
    /// Se lanza si la respuesta del servicio web no puede ser deserializada en el tipo esperado, 
    /// lo que puede ocurrir si la estructura JSON de la respuesta es diferente a la esperada.
    /// </exception>
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