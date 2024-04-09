namespace Cebv.features.dashboard.data;

public class Reporte
{
    public string? tipo_reporte { get; set; }
    public string? area_atiende { get; set; }
    public string? tipo_de_medio { get; set; }
    public string? medio_conocimiento { get; set; }
    public string? zona_del_estado { get; set; }
    public List<string>? reportantes { get; set; }
    public List<string>? desaparecidos { get; set; }
    public DateTime? fecha_desaparicion { get; set; }
}