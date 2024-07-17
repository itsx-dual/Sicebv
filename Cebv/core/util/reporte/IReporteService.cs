using Cebv.core.util.reporte.viewmodels;

namespace Cebv.core.util.reporte;

public interface IReporteService
{
    Reporte GetReporte();
    Task<Reporte> Sync();
    Task<Reporte> Reload(int id);
    Reporte ClearReporte();
    EstadoReporte GetStatusReporte();
    void SetStatusReporte(EstadoReporte estado);
    int GetReporteId();
    bool HayReporte();
}