using System.Text.Json.Serialization;

namespace Cebv.features.login.data;

public class TokenWrapped
{
    public Token data { get; set; }
}

public class Token
{
    [property: JsonPropertyName("plain_text_token")]
    public string? TokenText { get; set; }

    [property: JsonPropertyName("token_name")]
    public string? TokenName { get; set; }

    [property: JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    [property: JsonPropertyName("created_at")]
    public DateTime FechaCreacion { get; set; }
}