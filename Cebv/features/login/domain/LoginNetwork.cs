using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Cebv.features.login.data;
using HttpClientHandler = Cebv.core.network.HttpClientHandler;

namespace Cebv.features.login.domain;

public class LoginNetwork
{
    private static HttpClient Client => HttpClientHandler.SharedClientHandler;

    public static async Task<Object> GetTokenRequest(string username, string password)
    {
        Client.DefaultRequestHeaders.Add("Accept", "application/json");
        using HttpResponseMessage response = await Client.GetAsync(
            $"api/token?email={username}&password={password}&token_name=escritorio.{System.Environment.MachineName}");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        switch ((int)response.StatusCode)
        {
            case 200:
                TokenWrapped token = JsonSerializer.Deserialize<TokenWrapped>(jsonResponse);
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.data.TokenText);
                return
                    token; // Devuelto por utilidad, ya que el token queda en los headers de la instancia estatica del manejador.

            case 401:
                //JSType.Error? error = JsonSerializer.Deserialize<JSType.Error>(jsonResponse);
                return $"{username} + {password}";

            case 422:
                // Implementa bien el tipo de retorno
                return $"{username} + {password}";

            default:
                return $"{username} + {password}";
        }
    }
}