namespace Cebv.features.Prendas.data.Prendas_Vestir;

public class MainTabla
{
    public string GrupoPertenencias { get; set; }
    public string Pertenencia { get; set; }
    public string Material { get; set; }
    public string PertenenciaColor { get; set; }
    public string Marca { get; set; }
    public string Descripcion { get; set; }

    public MainTabla(string grupo, string pertenencia, string material, string color, string marca, string descripcion)
    {
        this.GrupoPertenencias = grupo;
        this.Pertenencia = pertenencia;
        this.Material = material;
        this.PertenenciaColor = color;
        this.Marca = marca;
        this.Descripcion = descripcion;
    }
}