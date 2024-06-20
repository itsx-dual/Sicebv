using Cebv.core.modules.reporte.data;
using Cebv.core.util.reporte.data;

namespace Cebv.core.util.reporte;

public interface IReporteService
{
    void SetReporteActual(ReporteResponse? reporte);
    
    void SetReporteActualFromApi(int id);
    
    bool SendReporteActual();
    
    ReporteResponse ClearReporteActual();
    
    ReporteResponse GetReporteActual();
    
    int GetReporteActualId();

    EstadoReporte GetStatusReporteActual();

    bool HayReporte();
    
    void SetStatusReporteActual(EstadoReporte estado);

    void SetReporteId(int id);
    int GetReporteId();
    
    void SetReportanteId(int id);
    int GetReportanteId();
    int GetDesaparecidoId();

    bool SendInformacionInicio(InicioPostObject informacion);
    bool SendInformacionInstrumentoJuridico(InstrumentoJuridicoPostObject informacion);
}