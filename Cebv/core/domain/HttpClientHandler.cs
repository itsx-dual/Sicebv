using System.Net.Http;
using System.Net.Http.Headers;

namespace Cebv.core.domain;

public class HttpClientHandler
{
    public static HttpClient SharedClientHandler = new()
    {
        BaseAddress = new Uri("http://187.251.212.146:18080/"),
        //BaseAddress = new Uri("http://localhost:8000/"),
        
        DefaultRequestHeaders =
        {
            Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
        }
    };
}