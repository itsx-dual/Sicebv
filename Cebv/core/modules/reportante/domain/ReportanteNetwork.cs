using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using CommunityToolkit.Diagnostics;

namespace Cebv.core.modules.reportante.domain;

public static class ReportanteNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<Object> GetReporte(int reporteId)
    {
        Guard.IsGreaterThan(reporteId, 1);
        var request = await Client.GetAsync($"api/reportes/{reporteId}");
        var response = await request.Content.ReadAsStringAsync();

        ReporteQueryResponse? jsonResponse = JsonSerializer.Deserialize<ReporteQueryResponse>(response);

        ReporteResponse reporteResponse;

        Guard.IsNotNull(jsonResponse);

        reporteResponse = jsonResponse.Data;

        return reporteResponse;
    }

    public static async Task<ObservableCollection<Catalogo>> GetParentescos()
    {
        var request = await Client.GetAsync("api/parentescos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogosWrapped jsonResponse = JsonSerializer.Deserialize<CatalogosWrapped>(response)!;

        return jsonResponse.Data;
    }

    public static async Task PostReportante(
        int reporteId,
        int personaId,
        int parentescoId,
        bool denunciaAnonima,
        bool informacionConsentimiento,
        bool informacionExclusivaBusqueda,
        bool publicacionRegistroNacional,
        bool publicacionBoletin,
        bool pertenenciaColectivo,
        string? nombreColectivo = null,
        string? informacionRelevante = null)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                reporte_id = reporteId,
                persona_id = personaId,
                parentesco_id = parentescoId,
                denuncia_anonima = denunciaAnonima,
                informacion_consentimiento = informacionConsentimiento,
                informacion_exclusiva_busqueda = informacionExclusivaBusqueda,
                publicacion_registro_nacional = publicacionRegistroNacional,
                publicacion_boletin = publicacionBoletin,
                pertenencia_colectivo = pertenenciaColectivo,
                nombre_colectivo = nombreColectivo,
                informacion_relevante = informacionRelevante
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await Client.PostAsync(
            "api/reportantes",
            jsonContent);

        Console.WriteLine(response);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }

    public static async Task PostArea(string nombreArea)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                nombre = nombreArea,
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await Client.PostAsync(
            "api/areas",
            jsonContent);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
}