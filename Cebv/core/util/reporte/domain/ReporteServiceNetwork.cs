using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using Cebv.core.domain;
using Cebv.core.domain.paginated_resource;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wpf.Ui.Controls;

namespace Cebv.core.util.reporte.domain;

public abstract class ReporteServiceNetwork
{
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;

    private static ObservableCollection<string> _fotosDesaparecido = new();
    private static HttpClient Client => CebvClientHandler.SharedClient;

    public static async Task<Reporte> ShowReporte(int id)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/reportes/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        };

        var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PaginatedResource<Reporte>>(json)?.Data!;
    }

    public static async Task<Reporte> Sync(Reporte reporte)
    {
        var json = JsonConvert.SerializeObject(reporte);
        Console.Write($"Request: {JObject.Parse(json).ToString(Formatting.Indented)}\n \n");

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/actualizar/reporte", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await Client.SendAsync(request);
        json = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response: {JObject.Parse(json).ToString(Formatting.Indented)}");
        return JsonConvert.DeserializeObject<PaginatedResource<Reporte>>(json)?.Data!;
    }

    public static async Task<bool> SetFolios(int reporteId)
    {
        var response = await Client.GetAsync($"api/reportes/asignar_folio/{reporteId}");
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {JObject.Parse(json).ToString(Formatting.Indented)}");

        return response.IsSuccessStatusCode;
    }

    public static async Task SubirFotosDesaparecido(int desaparecidoId, List<BitmapImage> imagenes,
        BitmapImage? imagenBoletin)
    {
        if (imagenBoletin == null)
        {
            _snackBarService.Show(
                "Advertencia",
                "No se ha seleccionado una imagen para el boletín.",
                ControlAppearance.Primary,
                new SymbolIcon(SymbolRegular.Warning20),
                new TimeSpan(0, 0, 5));

        }
        var form = new MultipartFormDataContent();
        var count = 0;
        //Esto borra los archivos por lo que permite borrar archivos desde el gui y evita duplicados
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/desaparecidos/fotos/{desaparecidoId}", UriKind.Relative),
            Method = HttpMethod.Delete
        };
        await Client.SendAsync(request);
        foreach (var imagen in imagenes)
        {
                var content = new StreamContent(ImageUtils.BitmapImageToStream(imagen));
                var filename = $"Foto_Desaparecido_{count + 1}.jpg";
                
              if (EsBoletin(imagen,imagenBoletin))
              {
                  form.Add(content, "boletin", filename);
                  Console.WriteLine("BOLETIN DETECTADO");
              }
            form.Add(content, $"file_{count + 1}", filename);
            count++;
        }
        request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/desaparecidos/fotos/{desaparecidoId}", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = form
        };
        await Client.SendAsync(request);
    }
    public static async Task<ObservableCollection<string>> GetImagenesDesaparecidos(int? desaparecido_id)
    {
        if (desaparecido_id == null || desaparecido_id <= 0) return null;
        var response = await Client.GetAsync($"/api/desaparecidos/fotos/{desaparecido_id}");
        if (!response.IsSuccessStatusCode) return null;
        var responseContent = await response.Content.ReadAsStringAsync();
        _fotosDesaparecido = JsonConvert.DeserializeObject<ObservableCollection<string>>(responseContent);
        return _fotosDesaparecido;
    }





    public static async Task<BitmapImage?> GetImage(int senaId)
    {
        if (senaId <= 0) return null;
        var response = await Client.GetAsync($"/api/senas-particulares/foto/{senaId}");
        if (!response.IsSuccessStatusCode) return null;
        var base64 = await response.Content.ReadAsStringAsync();
        return ImageUtils.Base64StringToBitmapImage(base64);
    }

    private static byte[] ImageToByteArray(BitmapImage image)
    {
        // Usamos un MemoryStream para convertir la imagen en bytes
        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(image));

        using (MemoryStream ms = new MemoryStream())
        {
            encoder.Save(ms);
            return ms.ToArray();
        }
    }
    //Ua disculpa pero no me queda de otra, se ve hasta grosero pero fue la unica forma
    public static bool EsBoletin(BitmapImage img1, BitmapImage img2)
    {
        byte[] imagen1 = ImageToByteArray(img1);
        byte[] imagen2 = ImageToByteArray(img2);
        
        if (imagen1.Length != imagen2.Length)
            return false;
        
        for (int i = 0; i < imagen1.Length; i++)
        {
            if (imagen1[i] != imagen2[i])
                return false;
        }

        return true;
    }

    
    
}