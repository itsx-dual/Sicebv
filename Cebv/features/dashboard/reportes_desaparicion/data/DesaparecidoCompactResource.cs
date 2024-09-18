using Newtonsoft.Json;

namespace Cebv.features.dashboard.reportes_desaparicion.data;

[JsonObject(MemberSerialization.OptIn)]
public class DesaparecidoCompactResource
{
    [JsonConstructor]
    public DesaparecidoCompactResource(
            int id,
            string? nombre,
            string? apellido_paterno,
            string? apellido_materno,
            string? estatus_cebv,
            string? estatus_rnpdno,
            string? folio_cebv
        )
    {
        Id = id;
        Nombre = nombre;
        ApellidoPaterno = apellido_paterno;
        ApellidoMaterno = apellido_materno;
        EstatusCebv = estatus_cebv;
        EstatusRnpdno = estatus_rnpdno;
        FolioCebv = folio_cebv;
    }
    
    [JsonProperty(PropertyName = "id")] public int Id { get; set; }
    [JsonProperty(PropertyName = "nombre")] public string? Nombre { get; set; }
    [JsonProperty(PropertyName = "apellido_paterno")] public string? ApellidoPaterno { get; set; }
    [JsonProperty(PropertyName = "apellido_materno")] public string? ApellidoMaterno { get; set; }
    [JsonProperty(PropertyName = "estatus_cebv")] public string? EstatusCebv { get; set; }
    [JsonProperty(PropertyName = "estatus_rnpdno")] public string? EstatusRnpdno { get; set; }
    [JsonProperty(PropertyName = "folio_cebv")] public string? FolioCebv { get; set; }
}