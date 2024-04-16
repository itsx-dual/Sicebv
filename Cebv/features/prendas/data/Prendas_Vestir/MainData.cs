namespace Cebv.features.Prendas.data.Prendas_Vestir;

public class PrendasData
{
    public int persona_id { get; set; }
    public int grupoPertenencias_id { get; set; }
    public int pertenencia_id { get; set; }
    public int material_id { get; set; }
    public int color_id { get; set; }
    public String marca { get; set; }
    public String descripcion { get; set; }

    public PrendasData
        (
            int persona,
            int grupo,
            int pertenencia,
            int material,
            int color,
            String marca,
            String descripcion
        )
    {
        this.persona_id = persona;
        this.grupoPertenencias_id = grupo;
        this.pertenencia_id = pertenencia;
        this.material_id = material;
        this.color_id = color;
        this.marca = marca;
        this.descripcion = descripcion;
    }
}

class MainData
{
    public List<PrendasData> data { get; set; }
}