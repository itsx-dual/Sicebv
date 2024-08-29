using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Cebv.core.domain;
using Cebv.features.login.data;

namespace Cebv.features.login.domain;

public static class LoginNetwork
{
    private static HttpClient Client => CebvClientHandler.SharedClient;

    private static TokenWrapped Token(string json)
    {
        var token = JsonSerializer.Deserialize<TokenWrapped>(json)!;
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.data.TokenText);
        return token;
    }

    public static async Task<dynamic?> Post(string username, string password)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/api/token", UriKind.Relative),
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", username },
                { "password", password },
                { "token_name", $"escritorio.{Environment.UserName}.{Environment.MachineName}" }
            })
        };

        try
        {
            using var response = await Client.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return (int)response.StatusCode switch
            {
                200 => Token(jsonResponse),
                401 => JsonSerializer.Deserialize<Error>(jsonResponse)!,
                422 => JsonSerializer.Deserialize<Error>(jsonResponse)!,
                _ => new Error() { error = "Error al intentar iniciar sesión" }
            };
        }
        catch (HttpRequestException ex)
        {
            return new Error() { error = "Error al intentar conectar con el servidor" };
        }
        catch (Exception ex)
        {
            return new Error() { error = "Error desconocido" };
        }
    }
}