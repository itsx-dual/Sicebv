using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Wpf.Ui.Controls;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
{
    public static async void PostHechosdesaparicion(ModoTiempoLugarPost content)
    {
        var reporteRequest = new HttpRequestMessage
        {
            
            RequestUri = new Uri("/api/hechos-desapariciones", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "reporte_id", $"{content.ReporteId}" },
                { "fecha_desaparicion_cebv", content.FechaDesaparicionCebv! },
                { "fecha_percato", content.FechaDesaparicion.ToString()! },
                { "fecha_percato_cebv", content.FechaDesaparicionCebv! },
                { "aclaraciones_fecha_hechos", content.AclaracionHechos! },
                { "cambio_comportamiento", $"{NullableBoolToInt(content.AmenazaCambioComportamiento)}" },
                { "descripcion_cambio_comportamiento", content.AmenazaDescripcion! },
                { "contador_desapariciones", content.ContadorDesaparicion.ToString()! },
                { "situacion_previa", content.SituacionPreviaDescripcion! },
                { "informacion_relevante", content.DatosPersonasRealacionadas! },
                { "hechos_desaparicion", content.DescripcionHechosDesaparicion! },
                { "sintesis_desaparicion", content.SintesisHechosDesaparicion! }
                
            })
            
        };

        var query = await Client.SendAsync(reporteRequest);

        if (query.IsSuccessStatusCode)
        {
            _reporteService.SetHechosDesaparicionActual(true);
        }
        else
        {
            _reporteService.SetHechosDesaparicionActual(false);
            _snackbar.Show(
                "No se ha podido registrar la informacion de Modo, tiempo y lugar",
                "",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning24),
                new TimeSpan(0, 0, 5));
        }
    }

    public static async void PutHechosDesaparicion(int? id, ModoTiempoLugarPost content)
    {
        var reporteRequest = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/hechos-desapariciones/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "reporte_id", content.ReporteId.ToString()! },
                { "fecha_desaparicion", content.FechaDesaparicion.ToString()! },
                { "fecha_desaparicion_cebv", content.FechaDesaparicionCebv! },
                { "fecha_percato", content.FechaDesaparicion.ToString()! },
                { "fecha_percato_cebv", content.FechaDesaparicionCebv! },
                { "aclaraciones_fecha_hechos", content.AclaracionHechos! },
                { "cambio_comportamiento", content.AmenazaCambioComportamiento.ToString()! },
                { "descripcion_cambio_comportamiento", content.AmenazaDescripcion! },
                { "contador_desapariciones", content.ContadorDesaparicion.ToString()! },
                { "situacion_previa", content.SituacionPreviaDescripcion! },
                { "informacion_relevante", content.DatosPersonasRealacionadas! },
                { "hechos_desaparicion", content.DescripcionHechosDesaparicion! },
                { "sintesis_desaparicion", content.SintesisHechosDesaparicion! }
            })
        };

        var query = await Client.SendAsync(reporteRequest);
        var json = await query.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<HechoDesaparicionQueryResponse>(json);

        if (query.IsSuccessStatusCode)
        {
            _reporteService.SetHechosDesaparicionActual(true);
        }
        else
        {
            _snackbar.Show(
                "No se ha podido registrar la informacion de Modo, tiempo y lugar",
                "",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning24),
                new TimeSpan(0, 0, 5));
        }
        
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }
}