using System.Net.Http;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.data;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util.reporte.domain;

public class ReporteServiceNetwork
{
    private static HttpClient Client = CebvClientHandler.SharedClient;
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>();
    private static ISnackbarService _snackbar = App.Current.Services.GetService<ISnackbarService>();

    private static int? NullableBoolToInt(bool? boolean)
    {
        return boolean switch
        {
            true => 1,
            false => 0,
            _ => null
        };
    }

    public static async void PostInicioReporte(InicioPostObject content)
    {
        var reporteRequest = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/reportes", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "estado_id", content.Estado },
                { "medio_conocimiento_id", content.Medio.ToString() },
                { "tipo_reporte_id", content.TipoReporte.ToString() }
            })
        };

        var reporteResponse = await Client.SendAsync(reporteRequest);
        var json = await reporteResponse.Content.ReadAsStringAsync();
        var reporte = JsonSerializer.Deserialize<ReporteQueryResponse>(json);
        
        _reporteService.SetReporteActual(reporte?.Data);
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);

        var reportanteRequest = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/reportantes", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "reporte_id", _reporteService.GetReporteId().ToString() },
                { "informacion_exclusiva_busqueda", $"{NullableBoolToInt(content.InformacionExclusivaBusqueda)}" },
                { "publicacion_boletin", $"{NullableBoolToInt(content.PublicacionInformacion)}" }
            })
        };
        
        var reportanteResponse = await Client.SendAsync(reportanteRequest);
        json = await reportanteResponse.Content.ReadAsStringAsync();
        
        if (reporteResponse.IsSuccessStatusCode && reportanteResponse.IsSuccessStatusCode)
        {
            _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        }
        else
        {
            _snackbar.Show(
                "No se ha podido registrar la informacion de inicio", 
                "",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning24),
                new TimeSpan( 0,0, 5));
        }
    }
    
    public static async void PutInicioReporte(int id, InicioPostObject content)
    {
        var reporteRequest = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "estado_id", content.Estado },
                { "medio_conocimiento_id", content.Medio.ToString() },
                { "tipo_reporte_id", content.TipoReporte.ToString() }
            })
        };

        var reporteResponse = await Client.SendAsync(reporteRequest);
        var json = await reporteResponse.Content.ReadAsStringAsync();
        var reporte = JsonSerializer.Deserialize<ReporteQueryResponse>(json);

        if (reporteResponse.IsSuccessStatusCode)
        {
            _reporteService.SetReporteActual(reporte?.Data);
            _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
        }
        else
        {
            _snackbar.Show(
                "No se ha podido registrar la informacion de inicio", 
                "",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning24),
                new TimeSpan( 0,0, 5));
        }
        
        var reportanteRequest = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportantes/{_reporteService.GetReportanteId()}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "reporte_id", _reporteService.GetReporteId().ToString() },
                { "informacion_exclusiva_busqueda", $"{NullableBoolToInt(content.InformacionExclusivaBusqueda)}" },
                { "publicacion_boletin", $"{NullableBoolToInt(content.PublicacionInformacion)}" }
            })
        };
        
        await Client.SendAsync(reportanteRequest);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
    }


    public static async Task<ReporteResponse> ShowReporte(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ReporteQueryResponse>(json).Data;
    }
}