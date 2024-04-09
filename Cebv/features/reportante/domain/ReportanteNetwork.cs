using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cebv.core.data;
using Cebv.features.reporte.data;
using HttpClientHandler = Cebv.core.domain.HttpClientHandler;

namespace Cebv.features.reportante.domain;

public class ReportanteNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

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
        int? reporteId,
        int? personaId = null,
        int? parentescoId = null,
        bool? denunciaAnonima = null,
        bool? informacionConsentimiento = null,
        bool? informacionExclusivaBusqueda = null,
        bool? publicacionRegistroNacional = null,
        bool? publicacionBoletin = null,
        bool? pertenenciaColectivo = null,
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

    public static async Task<Object> GetOpcionesBooleanas()
    {
        Dictionary<bool, string> opcionesBooleanas = new Dictionary<bool, string>();

        opcionesBooleanas.Add(true, "Sí");
        opcionesBooleanas.Add(false, "No");

        return opcionesBooleanas;
    }
}