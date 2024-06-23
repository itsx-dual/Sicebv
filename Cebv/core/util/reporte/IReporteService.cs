using Cebv.core.modules.reporte.data;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.reporte.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

namespace Cebv.core.util.reporte;

public interface IReporteService
{
    Estado? UbicacionEstado { get; set; }
    
    UbicacionViewModel? UbicacionHechos { get; set; }
    void SetReporteActual(ReporteResponse? reporte);
    
    void SetReporteActualFromApi(int id);
    
    bool SendReporteActual();
    
    ReporteResponse ClearReporteActual();
    
    ReporteResponse? GetReporteActual();
    
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
    bool SendModoTiempoLugar(ModoTiempoLugarPost informacion);
    bool SendInformacionInstrumentoJuridico(InstrumentoJuridicoPostObject informacion);

    bool SendReportante(ReportantePostObject informacion);
}