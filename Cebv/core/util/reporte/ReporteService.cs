using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.core.util.reporte;

public enum EstadoReporte
{
    Indefinido,
    Nuevo,
    Guardado,
    Terminado,
    Cargado,
}

public class ReporteService : IReporteService
{
    private Reporte _reporte = new();
    private EstadoReporte _estadoActual = EstadoReporte.Indefinido;
    
    public Reporte GetReporte()
    {
        return _reporte;
    }

    public async Task<Reporte> Sync()
    {
        _reporte = await ReporteServiceNetwork.Sync(_reporte);
        _estadoActual = EstadoReporte.Guardado;
        return _reporte;
    } 

    public async Task<Reporte> Reload(int id)
    {
        _reporte =  await ReporteServiceNetwork.ShowReporte(id);
        _estadoActual = EstadoReporte.Cargado;
        return _reporte;
    }

    public Reporte ClearReporte()
    {
        _reporte = new();
        _estadoActual = EstadoReporte.Nuevo;
        return _reporte;
    }

    public EstadoReporte GetStatusReporte()
    {
        return _estadoActual;
    }

    public void SetStatusReporte(EstadoReporte estado)
    {
        _estadoActual = estado;
    }

    public int GetReporteId()
    {
        return _reporte.Id;
    }

    public bool HayReporte()
    {
        return _estadoActual != EstadoReporte.Indefinido &&
               _estadoActual != EstadoReporte.Nuevo;
    }
}