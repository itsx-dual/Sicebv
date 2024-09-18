using Cebv.core.util.reporte.domain;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.core.util.reporte;

/// <summary>
/// Representa los posibles estados de un reporte.
/// </summary>
public enum EstadoReporte
{
    /// <summary>
    /// El estado inicial del reporte, antes de cualquier modificación.
    /// </summary>
    Indefinido,
    
    /// <summary>
    /// El reporte ha sido creado pero aún no se ha guardado.
    /// </summary>
    Nuevo,
    
    /// <summary>
    /// El reporte ha sido sincronizado exitosamente en el servicio web.
    /// </summary>
    Guardado,
    
    /// <summary>
    /// El reporte ha sido cargado desde una fuente de datos externa.
    /// </summary>
    Cargado,
    
    /// <summary>
    /// El reporte está en un estado de error en donde la sincorinizacion no fue exitosa.
    /// </summary>
    Error,
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
        var reporte = await ReporteServiceNetwork.Sync(_reporte);
        
        if (reporte is not null)
        {
            _reporte = reporte;
            _estadoActual = EstadoReporte.Guardado;
        }
        else
        {
            _estadoActual = EstadoReporte.Error;
        }
        
        return reporte;
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

    public async Task<bool> SetFolios()
    {
        if (_estadoActual == EstadoReporte.Guardado)
        {
            return await ReporteServiceNetwork.SetFolios(_reporte.Id);
        }

        return false;
    }

    public bool HayReporte()
    {
        return _estadoActual != EstadoReporte.Indefinido &&
               _estadoActual != EstadoReporte.Nuevo;
    }
}