using Cebv.core.modules.reporte.data;

namespace Cebv.core.util.reporte;

public class ReporteService : IReporteService
{
    private ReporteResponse? _reporte;
    private int _estadoActual =  -1;
    
    public enum EstadoReporte
    {
        Indefinido = -1,
        Nuevo = 0,
        Guardado = 1,
        Terminado = 2,
        Cargaddo = 3
    }

    public ReporteResponse? GetReporteActual()
    {
        return _reporte;
    }

    public int GetReporteActualId()
    {
        if (_reporte == null) return -1;
        return _reporte.Id;
    }

    public void SetReporteActual(ReporteResponse? reporte)
    {
        if (reporte == null) return;
        _reporte = reporte;
    }

    public void SetReporteActualFromApi(int id)
    {
        throw new NotImplementedException();
    }

    public ReporteResponse ClearReporteActual()
    {
        _reporte = new ReporteResponse();
        _estadoActual = (int) EstadoReporte.Nuevo;
        return _reporte;
    }

    public bool SendReporteActual()
    {
        throw new NotImplementedException();
    }

    public int GetStatusReporteActual()
    {
        return _estadoActual;
    }
    
    public void SetStatusReporteActual(int estado)
    {
        _estadoActual = estado;
    }
}