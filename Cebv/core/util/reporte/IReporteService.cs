using Cebv.core.modules.reporte.data;

namespace Cebv.core.util.reporte;

public interface IReporteService
{
    void SetReporteActual(ReporteResponse? reporte);
    
    void SetReporteActualFromApi(int id);
    
    bool SendReporteActual();
    
    ReporteResponse ClearReporteActual();
    
    ReporteResponse? GetReporteActual();
    
    int GetReporteActualId();

    int GetStatusReporteActual();
    
    void SetStatusReporteActual(int estado);
}