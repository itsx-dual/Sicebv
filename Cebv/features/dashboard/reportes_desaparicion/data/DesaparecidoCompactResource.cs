using Newtonsoft.Json;

namespace Cebv.features.dashboard.reportes_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public class DesaparecidoCompactResource
{
    [JsonConstructor]
    public DesaparecidoCompactResource(
        int id,
        string? nombre,
        string? apellidoPaterno,
        string? apellidoMaterno,
        string? estatusPreliminar,
        string? estatusFormalizado,
        string? folioCebv
    )
    {
        Id = id;
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        EstatusPreliminar = estatusPreliminar;
        EstatusFormalizado = estatusFormalizado;
        FolioCebv = folioCebv;
    }

    [JsonProperty(PropertyName = "id")] public int Id { get; set; }

    [JsonProperty(PropertyName = "nombre")]
    public string? Nombre { get; set; }

    [JsonProperty(PropertyName = "apellido_paterno")]
    public string? ApellidoPaterno { get; set; }

    [JsonProperty(PropertyName = "apellido_materno")]
    public string? ApellidoMaterno { get; set; }

    [JsonProperty(PropertyName = "estatus_preliminar")]
    public string? EstatusPreliminar { get; set; }

    [JsonProperty(PropertyName = "estatus_formalizado")]
    public string? EstatusFormalizado { get; set; }

    [JsonProperty(PropertyName = "folio_cebv")]
    public string? FolioCebv { get; set; }
}