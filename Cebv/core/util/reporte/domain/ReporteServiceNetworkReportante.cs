using System.Net.Http;
using System.Text.Json;
using Cebv.core.modules.persona.data;
using Cebv.core.util.reporte.data;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
{   
    public static async void PostReportante(ReportantePostObject informacion)
    {
        int personaId = await PostPersona(informacion.Persona);
        
        var content = new Dictionary<string, string>
        {
            {"reporte_id", _reporteService.GetReporteActualId().ToString()},
            {"persona_id", personaId.ToString()},
            {"parentesco_id", informacion.Parentesco.ToString()!},
            {"denuncia_anonima", $"{NullableBoolToInt(informacion.DenunciaAnonima)}"},
            {"informacion_consentimiento", $"{NullableBoolToInt(informacion.InformacionConsentimiento)}"},
            {"informacion_exclusiva_busqueda", $"{NullableBoolToInt(informacion.InformacionExclusivaBusqueda)}"},
            {"publicacion_registro_nacional", $"{NullableBoolToInt(informacion.PublicacionRegistroNacional)}"},
            {"publicacion_boletin", $"{NullableBoolToInt(informacion.PublicacionBoletin)}"},
            {"pertenencia_colectivo", $"{NullableBoolToInt(informacion.PertenenciaColectivo)}"},
            {"nombre_colectivo", informacion.NombreColectivo!},
            {"informacion_relevante", informacion.InformacionRelevante!},
        };

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/reportantes", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(content)
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteActualId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }
    
    public static async void PutReportante(ReportantePostObject informacion, int id)
    {
        var persona = _reporteService.GetReporteActual().Reportantes?.FirstOrDefault()?.Persona;
        int personaId;
        
        if (persona == null)
        {
            personaId = await PostPersona(informacion.Persona);
        }
        else
        {
            personaId = await PutPersona(informacion.Persona, persona.Id);
        }
        
        var content = new Dictionary<string, string>
        {
            {"reporte_id", _reporteService.GetReporteActualId().ToString()},
            {"persona_id", personaId.ToString()},
            {"parentesco_id", informacion.Parentesco.ToString()!},
            {"denuncia_anonima", $"{NullableBoolToInt(informacion.DenunciaAnonima)}"},
            {"informacion_consentimiento", $"{NullableBoolToInt(informacion.InformacionConsentimiento)}"},
            {"informacion_exclusiva_busqueda", $"{NullableBoolToInt(informacion.InformacionExclusivaBusqueda)}"},
            {"publicacion_registro_nacional", $"{NullableBoolToInt(informacion.PublicacionRegistroNacional)}"},
            {"publicacion_boletin", $"{NullableBoolToInt(informacion.PublicacionBoletin)}"},
            {"pertenencia_colectivo", $"{NullableBoolToInt(informacion.PertenenciaColectivo)}"},
            {"nombre_colectivo", informacion.NombreColectivo!},
            {"informacion_relevante", informacion.InformacionRelevante!},
        };

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportantes/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(content)
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteActualId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }
}