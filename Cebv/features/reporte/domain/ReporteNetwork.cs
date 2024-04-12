using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Cebv.core.data;
using Cebv.features.reporte.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;

namespace Cebv.features.reporte.domain;

public class ReporteNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<ReporteWrapped?> GetReportes()
    {
        var request = await Client.GetAsync("api/reportes");

        var response = await request.Content.ReadAsStringAsync();

        ReporteWrapped? reportes = JsonSerializer.Deserialize<ReporteWrapped>(response);

        return reportes;
    }

    public static async Task<Object> GetTiposReportes()
    {
        var request = await Client.GetAsync("api/tipos-reportes");

        var response = await request.Content.ReadAsStringAsync();

        CatalogoWrapped? jsonResponse = JsonSerializer.Deserialize<CatalogoWrapped>(response);

        ObservableCollection<Catalogo> tiposReportes = new ObservableCollection<Catalogo>();

        foreach (var tipoReporte in jsonResponse?.data!) tiposReportes.Add(tipoReporte);

        return tiposReportes;
    }

    public static async Task<Object> GetAreas()
    {
        var request = await Client.GetAsync("api/areas");

        var response = await request.Content.ReadAsStringAsync();

        CatalogoWrapped? jsonResponse = JsonSerializer.Deserialize<CatalogoWrapped>(response);

        ObservableCollection<Catalogo> areas = new ObservableCollection<Catalogo>();

        foreach (var area in jsonResponse?.data!) areas.Add(area);

        return areas;
    }

    public static async Task<Object> GetTiposMedios()
    {
        var request = await Client.GetAsync("api/tipos-medios");

        var response = await request.Content.ReadAsStringAsync();

        CatalogoWrapped? jsonResponse = JsonSerializer.Deserialize<CatalogoWrapped>(response);

        ObservableCollection<Catalogo> tiposMedios = new ObservableCollection<Catalogo>();

        foreach (var tipoMedio in jsonResponse?.data!) tiposMedios.Add(tipoMedio);

        return tiposMedios;
    }

    public static async Task<Object> GetMedios()
    {
        var request = await Client.GetAsync("api/medios");

        var response = await request.Content.ReadAsStringAsync();

        MedioWrapped? jsonResponse = JsonSerializer.Deserialize<MedioWrapped>(response);

        ObservableCollection<Medio> medios = new ObservableCollection<Medio>();

        foreach (var medio in jsonResponse?.data!) medios.Add(medio);

        return medios;
    }

    public static async Task<Object> GetEstados()
    {
        var request = await Client.GetAsync("api/estados");

        var response = await request.Content.ReadAsStringAsync();

        EstadoWrapped? jsonResponse = JsonSerializer.Deserialize<EstadoWrapped>(response);

        ObservableCollection<Estado> estados = new ObservableCollection<Estado>();

        foreach (var estado in jsonResponse?.data!) estados.Add(estado);

        return estados;
    }

    public static async Task<Object> GetZonasEstados()
    {
        var request = await Client.GetAsync("api/zonas-estados");

        var response = await request.Content.ReadAsStringAsync();

        ZonaEstadoWrapped? jsonResponse = JsonSerializer.Deserialize<ZonaEstadoWrapped>(response);

        ObservableCollection<ZonaEstado> estados = new ObservableCollection<ZonaEstado>();

        foreach (var estado in jsonResponse?.data!) estados.Add(estado);

        return estados;
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

    public static async Task<Object> GetTiposDesapariciones()
    {
        Dictionary<string, string> tiposDesapariciones = new Dictionary<string, string>();

        tiposDesapariciones.Add("U", "Única");
        tiposDesapariciones.Add("M", "Múltiple");

        return tiposDesapariciones;
    }

    public static async Task<Object> PostReporte(int? tipoReporteId, string? tipoDesaparicion, int? areaId = null,
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

        ReporteById? reporteWrapped = JsonSerializer.Deserialize<ReporteById>(jsonResponse);

        Reporte reporte = new Reporte();

        reporte = reporteWrapped?.data!;

        return reporte;
    }
}