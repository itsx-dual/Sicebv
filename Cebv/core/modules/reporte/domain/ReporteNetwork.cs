using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;

namespace Cebv.core.modules.reporte.domain;

public class ReporteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<ObservableCollection<ReporteResponse>> GetReportes()
    {
        var request = await Client.GetAsync("api/reportes");
        var response = await request.Content.ReadAsStringAsync();
        ReportesQueryResponse reportes = JsonSerializer.Deserialize<ReportesQueryResponse>(response)!;
        return new ObservableCollection<ReporteResponse>(reportes.Data);
    }
    
    public static async Task<ReporteResponse?> GetReporte(string id)
    {
        HttpRequestMessage request = new()
        {
            RequestUri = new Uri($"api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };
        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ReporteQueryResponse>(json)?.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetTiposReportes()
    {
        var request = await Client.GetAsync("api/tipos-reportes");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return new ObservableCollection<Catalogo>(jsonResponse.Data);
    }

    public static async Task<ObservableCollection<Catalogo>> GetAreas()
    {
        var request = await Client.GetAsync("api/areas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped? jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response);

        return jsonResponse!.Data;
    }

    public static async Task<ObservableCollection<Catalogo>> GetTiposMedios()
    {
        var request = await Client.GetAsync("api/tipos-medios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return new ObservableCollection<Catalogo>(jsonResponse.Data);
    }
    
    public static async Task<ObservableCollection<Medio>> GetMedios(int? id)
    {
        if (id != null)
        {
            var request = await Client.GetAsync($"api/medios?search={id}");

            var response = await request.Content.ReadAsStringAsync();

            MediosWrapped jsonResponse = JsonSerializer.Deserialize<MediosWrapped>(response)!;
            return new ObservableCollection<Medio>(jsonResponse.Data);
        }

        return null;
    }

    public static async Task<Object> GetTiposHipotesis()
    {
        var request = await Client.GetAsync("api/tipos-hipotesis");

        var response = await request.Content.ReadAsStringAsync();

        TipoHipotesisWrapped? jsonResponse = JsonSerializer.Deserialize<TipoHipotesisWrapped>(response);

        ObservableCollection<TipoHipotesis> tiposHipotesis = new ObservableCollection<TipoHipotesis>();

        foreach (var tipoHipotesis in jsonResponse?.data!) tiposHipotesis.Add(tipoHipotesis);

        return tiposHipotesis;
    }

    public static async Task<ReporteResponse> PostReporte(int? tipoReporteId, string? tipoDesaparicion,
        int? areaId = null,
        int? medioId = null,
        int? zonaEstado = null, int? hipotesisId = null, string? fechaLocalizacion = null,
        string? sintesisLocalizacion = null,
        string? clasificacionPersona = null)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                tipo_reporte_id = tipoReporteId,
                tipo_desaparicion = tipoDesaparicion,
                area_atiende_id = areaId,
                medio_conocimiento_id = medioId,
                zona_estado_id = zonaEstado,
                hipotesis_oficial_id = hipotesisId,
                fecha_localizacion = fechaLocalizacion,
                sintesis_localizacion = sintesisLocalizacion,
                clasificacion_persona = clasificacionPersona
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await Client.PostAsync(
            "api/reportes",
            jsonContent);

        Console.WriteLine(response);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        ReporteQueryResponse? reporteWrapped = JsonSerializer.Deserialize<ReporteQueryResponse>(jsonResponse);

        return reporteWrapped?.Data!;
    }
}