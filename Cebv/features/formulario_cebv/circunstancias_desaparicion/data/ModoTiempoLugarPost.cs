using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public class ModoTiempoLugarPost
{
    public int ReporteId { get; set; }
    public DateTime? FechaDesaparicion { get; set; }
    public string? FechaDesaparicionCebv { get; set; }
    public string? HoraDesaparicion { get; set; }
    public DateTime? FechaPercato { get; set; }
    public string? FechaPercatoCebv { get; set; }
    public string? HoraPercato { get; set; }
    public string? AclaracionHechos;
    public UbicacionViewModel? Ubicacion;
    public bool? AmenazaCambioComportamiento = false;
    public string? AmenazaDescripcion;
    public int ContadorDesaparicion;
    public string? SituacionPreviaDescripcion { get; set; }
    public string? DatosPersonasRealacionadas { get; set; }
    public string? DescripcionHechosDesaparicion { get; set; }
    public string? SintesisHechosDesaparicion { get; set; }
    public int? TipoHipotesisUno { get; set; }
    public int? TipoHipotesisDos { get; set; }
    public int? Sitio { get; set; }
    public string? AreaCodifica { get; set; }
    public bool? DesaparecioAcompanado = false;
    public int NumeroPersonasMismoEvento { get; set; }
    public ObservableCollection<Expediente>? ExpedientesDirectos { get; set; }
    public ObservableCollection<Expediente>? ExpedientesIndirectos { get; set; }
}