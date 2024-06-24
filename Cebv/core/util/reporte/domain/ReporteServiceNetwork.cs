using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Cebv.core.domain;
using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Wpf.Ui;
using Wpf.Ui.Controls;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

    public static async Task<Reporte> ShowReporte(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Reportes>(json).Data;
    }

    public static async Task<ObservableCollection<HechosDesaparicionResponse>?> GetHechosDesaparicion(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/hechos-desapariciones?filter[reporte_id]={id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        var hechosDesaparicion = JsonSerializer.Deserialize<HechosDesaparicionQueryResponse>(json)!.Data;

        return hechosDesaparicion;
    }

    public static async void PostHechosDesaparicion(ModoTiempoLugarPost informacion)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/hechos-desapariciones", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "hechos_desaparicion", informacion.DescripcionHechosDesaparicion! },
                { "reporte_id", "1" },
            })
        };

        using var response = await Client.SendAsync(request);

        if (response.IsSuccessStatusCode) Console.WriteLine("Jalloooooo");
        else Console.WriteLine("No jalo");
    }
}