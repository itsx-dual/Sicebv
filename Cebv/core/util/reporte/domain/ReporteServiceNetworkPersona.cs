using System.IO;
using System.Net.Http;
using System.Text.Json;
using Cebv.core.modules.persona.data;
using Cebv.core.util.reporte.data;

namespace Cebv.core.util.reporte.domain;

public partial class ReporteServiceNetwork
{
    public static async Task<int> PostPersona(PersonaPostObject informacion)
    {
        var content = new Dictionary<string, string>
        {
            //{ "lugar_nacimiento_id", informacion.LugarNacimientoId },
            { "nombre", informacion.Nombre },
            { "apellido_paterno", informacion.ApellidoPaterno },
            { "apellido_materno", informacion.ApellidoMaterno },
            { "pseudonimo_nombre", informacion.PseudonimoNombre },
            { "pseudonimo_apellido_paterno", informacion.PseudonimoApellidoPaterno },
            { "pseudonimo_apellido_materno", informacion.PseudonimoApellidoMaterno },
            { "fecha_nacimiento", informacion.FechaNacimiento?.ToString("s") },
            { "curp", informacion.Curp },
            { "observaciones_curp", informacion.ObservacionesCurp },
            { "rfc", informacion.Rfc },
            { "ocupacion", informacion.Ocupacion },
            { "sexo_id", informacion.Sexo.ToString() },
            { "genero_id", informacion.Genero.ToString() }
        };
        
        content = (from kv in content
            where kv.Value != null
            select kv).ToDictionary(kv => kv.Key, kv => kv.Value);

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/personas", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(content)
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        var persona = JsonSerializer.Deserialize<PersonaWrapped>(json)?.Data;

        if (persona?.Id != null) return persona.Id;
        return -1;
    }

    public static async Task<int> PutPersona(PersonaPostObject informacion, int id)
    {
        var content = new Dictionary<string, string>
        {
            //{ "lugar_nacimiento_id", informacion.LugarNacimientoId.ToString()! },
            { "nombre", informacion.Nombre },
            { "apellido_paterno", informacion.ApellidoPaterno },
            { "apellido_materno", informacion.ApellidoMaterno },
            { "pseudonimo_nombre", informacion.PseudonimoNombre },
            { "pseudonimo_apellido_paterno", informacion.PseudonimoApellidoPaterno },
            { "pseudonimo_apellido_materno", informacion.PseudonimoApellidoMaterno },
            { "fecha_nacimiento", informacion.FechaNacimiento?.ToString("s") },
            { "curp", informacion.Curp },
            { "observaciones_curp", informacion.ObservacionesCurp },
            { "rfc", informacion.Rfc },
            { "ocupacion", informacion.Ocupacion },
            { "sexo_id", informacion.Sexo.ToString() },
            { "genero_id", informacion.Genero.ToString() }
        };

        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/personas/{id}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new FormUrlEncodedContent(content)
        };

        using var response = await Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        var persona = JsonSerializer.Deserialize<PersonaWrapped>(json)?.Data;

        if (persona?.Id != null) return persona.Id;
        return -1;
    }
}