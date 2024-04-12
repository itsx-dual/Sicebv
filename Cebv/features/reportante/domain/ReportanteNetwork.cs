using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.core.data;
using Cebv.features.reporte.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;
using CommunityToolkit.Diagnostics;

namespace Cebv.features.reportante.domain;

public class ReportanteNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<Object> GetReporte(int reporteId)
    {
        Guard.IsGreaterThan(reporteId, 1);
        var request = await Client.GetAsync($"api/reportes/{reporteId}");
        var response = await request.Content.ReadAsStringAsync();

        ReporteById? jsonResponse = JsonSerializer.Deserialize<ReporteById>(response);

        Reporte reporte;

        Guard.IsNotNull(jsonResponse);

        reporte = jsonResponse.data;

        return reporte;
    }

    public static async Task<Object> GetParentescos()
    {
        var request = await Client.GetAsync("api/parentescos");

        var response = await request.Content.ReadAsStringAsync();

        CatalogoWrapped? jsonResponse = JsonSerializer.Deserialize<CatalogoWrapped>(response);

        ObservableCollection<Catalogo> parentescos = new ObservableCollection<Catalogo>();

        foreach (var parentesco in jsonResponse?.data!) parentescos.Add(parentesco);

        return parentescos;
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