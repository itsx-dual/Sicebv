using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.domain;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;

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
    private ReporteResponse? _reporte;
    private EstadoReporte _estadoActual = EstadoReporte.Indefinido;
    private int _reporte_id = -1;
    private int _reportante_id = -1;
    private int _desaparecido_id = -1;


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

    public async void SetReporteActualFromApi(int id)
    {
        _reporte = await ReporteServiceNetwork.ShowReporte(id);
    }

    public ReporteResponse ClearReporteActual()
    {
        _reporte = new ReporteResponse();
        _estadoActual = EstadoReporte.Nuevo;
        return _reporte;
    }

    public bool SendReporteActual()
    {
        throw new NotImplementedException();
    }

    public EstadoReporte GetStatusReporteActual()
    {
        return _estadoActual;
    }

    public void SetStatusReporteActual(EstadoReporte estado)
    {
        _estadoActual = estado;
    }

    public void SetReporteId(int id)
    {
        this._reporte_id = id;
    }

    public int GetReporteId()
    {
        if (!HayReporte()) return -1;
        return _reporte.Id;
    }

    public bool HayReporte()
    {
        return _estadoActual != EstadoReporte.Indefinido &&
               _estadoActual != EstadoReporte.Nuevo;
    }

    public void SetReportanteId(int id)
    {
        this._reportante_id = id;
    }

    public int GetReportanteId()
    {
        return _reporte.Reportantes.First().Id;
    }

    public bool SendInformacionInicio(InicioPostObject informacion)
    {
        if (!HayReporte())
        {
            ReporteServiceNetwork.PostInicioReporte(informacion);
            return true;
        }

        ReporteServiceNetwork.PutInicioReporte(_reporte.Id, informacion);
        return true;
    }
}