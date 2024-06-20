using System.Net.Http;
using System.Text.Json;
using Cebv.core.util.reporte.data;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
{
    public static async void PostCarpetaInvestigacion(InstrumentoJuridicoPostObject informacion)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/documentos-legales", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "CI" },
                { "numero_documento", informacion.NumeroCarpeta },
                { "donde_radica", informacion.DondeRadicaCarpeta },
                { "nombre_servidor_publico", informacion.ServidorPublicoCarpeta },
                { "fecha_recepcion", informacion.FechaRecepcionCarpeta?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PostAmparo(InstrumentoJuridicoPostObject informacion)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/documentos-legales", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "AB" },
                { "numero_documento", informacion.NumeroAmparo },
                { "donde_radica", informacion.DondeRadicaAmparo },
                { "nombre_servidor_publico", informacion.ServidorPublicoAmparo },
                { "fecha_recepcion", informacion.FechaRecepcionAmparo?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PostRecomendacionDerechosHumanos(InstrumentoJuridicoPostObject informacion)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/documentos-legales", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "DH" },
                { "numero_documento", informacion.NumeroRecomendacion },
                { "donde_radica", informacion.DondeRadicaRecomendacion },
                { "nombre_servidor_publico", informacion.ServidorPublicoRecomendacion },
                { "fecha_recepcion", informacion.FechaRecepcionRecomendacion?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PostInstrumentoJuridico(InstrumentoJuridicoPostObject informacion)
    {
        var requestDesaparecido = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/desaparecidos", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "declaracion_especial_ausencia", $"{NullableBoolToInt(informacion.DeclaracionAusencia)}" },
                { "accion_urgente", $"{NullableBoolToInt(informacion.AccionUrgente)}" },
                { "dictamen", $"{NullableBoolToInt(informacion.Dictamen)}" },
                { "ci_nivel_federal", $"{NullableBoolToInt(informacion.CarpetaFederal)}" },
                { "otro_derecho_humano", informacion.OtroDerecho },
                { "reporte_id", _reporteService.GetReporteId().ToString() },
            })
        };

        using var response = await Client.SendAsync(requestDesaparecido);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PutCarpetaInvestigacion(InstrumentoJuridicoPostObject informacion, int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/documentos-legales/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "CI" },
                { "numero_documento", informacion.NumeroCarpeta },
                { "donde_radica", informacion.DondeRadicaCarpeta },
                { "nombre_servidor_publico", informacion.ServidorPublicoCarpeta },
                { "fecha_recepcion", informacion.FechaRecepcionCarpeta?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PutAmparo(InstrumentoJuridicoPostObject informacion, int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/documentos-legales/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "AB" },
                { "numero_documento", informacion.NumeroAmparo },
                { "donde_radica", informacion.DondeRadicaAmparo },
                { "nombre_servidor_publico", informacion.ServidorPublicoAmparo },
                { "fecha_recepcion", informacion.FechaRecepcionAmparo?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PutRecomendacionDerechosHumanos(InstrumentoJuridicoPostObject informacion, int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/documentos-legales/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "desaparecido_id", _reporteService.GetDesaparecidoId().ToString() },
                { "tipo_documento", "DH" },
                { "numero_documento", informacion.NumeroRecomendacion },
                { "donde_radica", informacion.DondeRadicaRecomendacion },
                { "nombre_servidor_publico", informacion.ServidorPublicoRecomendacion },
                { "fecha_recepcion", informacion.FechaRecepcionRecomendacion?.ToString("s") },
            })
        };

        using var response = await Client.SendAsync(request);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }

    public static async void PutInstrumentoJuridico(InstrumentoJuridicoPostObject informacion, int id)
    {
        var content = new Dictionary<string, string>
        {
            { "declaracion_especial_ausencia", $"{NullableBoolToInt(informacion.DeclaracionAusencia)}" },
            { "accion_urgente", $"{NullableBoolToInt(informacion.AccionUrgente)}" },
            { "dictamen", $"{NullableBoolToInt(informacion.Dictamen)}" },
            { "ci_nivel_federal", $"{NullableBoolToInt(informacion.CarpetaFederal)}" },
            { "otro_derecho_humano", informacion.OtroDerecho },
            { "reporte_id", _reporteService.GetReporteId().ToString() },
        };

        var requestDesaparecido = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/desaparecidos/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(content)
        };

        using var response = await Client.SendAsync(requestDesaparecido);
        _reporteService.SetReporteActualFromApi(_reporteService.GetReporteId());
        _reporteService.SetStatusReporteActual(EstadoReporte.Guardado);
    }
}