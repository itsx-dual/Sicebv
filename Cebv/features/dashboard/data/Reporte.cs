using System.Globalization;

namespace Cebv.features.dashboard.data;

public class Reporte
{
    public int? folio_id { get; set; }
    public int? desaparecido_id { get; set; }
    public int? persona_id { get; set; }
    public int? reportante_id { get; set; }
    public int? reporte_id { get; set; }
    public string? folio_cebv { get; set; }
    public string? nombre_desaparecido { get; set; }
    public string? reportante_nombre { get; set; }
    public string? curp_desaparecido { get; set; }
    public DateTime? fecha_desaparicion { get; set; }
}