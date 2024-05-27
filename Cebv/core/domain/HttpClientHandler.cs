using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace Cebv.core.domain;

public class HttpClientHandler
{
    public static HttpClient SharedClientHandler = new()
    {
        // La uri se obtiene de App.config.               Podemos poner un fallback uri      ↓
        BaseAddress = new Uri( ConfigurationManager.AppSettings.Get("api_uri") ?? string.Empty ),
        DefaultRequestHeaders =
        {
            Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
        }
    };
}