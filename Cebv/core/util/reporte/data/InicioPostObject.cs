namespace Cebv.core.util.reporte.data;

public class InicioPostObject
{
    public int TipoMedio { get; set; }
    public int Medio { get; set; }
    public string DependenciaOrigen { get; set; }
    public int TipoReporte { get; set; }
    public string Estado { get; set; }
    public bool? InformacionExclusivaBusqueda { get; set; }
    public bool? PublicacionInformacion { get; set; }
}