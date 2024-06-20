using System.Net.Http;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.data;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
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