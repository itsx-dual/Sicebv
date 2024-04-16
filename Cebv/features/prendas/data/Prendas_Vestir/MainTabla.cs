namespace Cebv.features.Prendas.data.Prendas_Vestir;

public class MainTabla
{
    public String grupoPertenencias { get; set; }
    public String pertenencia { get; set; }
    public String material { get; set; }
    public String pertenenciaColor { get; set; }
    public String marca { get; set; }
    public String descripcion { get; set; }

    public MainTabla
    (
        String grupo,
        String pertenencia,
        String material,
        String color,
        String marca,
        String descripcion
    )
    {
        this.grupoPertenencias = grupo;
        this.pertenencia = pertenencia;
        this.material = material;
        this.pertenenciaColor = color;
        this.marca = marca;
        this.descripcion = descripcion;
    }
}