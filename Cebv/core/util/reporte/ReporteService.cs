using Cebv.core.modules.reporte.data;
using Cebv.core.modules.reporte.domain;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.presentation;
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


    public ReporteResponse? GetReporteActual()
    {
        return _reporte ?? null;
    }

    public int GetReporteActualId()
    {
        if (_reporte == null) return -1;
        return _reporte.Id;
    }

    public Estado? UbicacionEstado { get; set; }
    public UbicacionViewModel? UbicacionHechos { get; set; }

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
        _reporte = new();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public int GetReportanteId()
    {
        var reportante = _reporte.Reportantes?.FirstOrDefault();
        if (reportante == null) return -1;
        return reportante.Id;
    }

    public int GetDesaparecidoId()
    {
        var desaparecido = _reporte.Desaparecidos?.FirstOrDefault();
        if (desaparecido == null) return -1;
        return desaparecido.Id;
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

    public bool SendInformacionInstrumentoJuridico(InstrumentoJuridicoPostObject informacion)
    {
        var desaparecido = _reporte.Desaparecidos?.FirstOrDefault();
        var carpetaInvestigacion  = desaparecido?.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "CI");
        var amparo = desaparecido?.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "AB");
        var recomendacion = desaparecido?.DocumentosLegales?.FirstOrDefault(x => x.TipoDocumento == "DH");

        if (desaparecido == null)
        {
            ReporteServiceNetwork.PostInstrumentoJuridico(informacion);
        }
        else
        {
            ReporteServiceNetwork.PutInstrumentoJuridico(informacion, desaparecido.Id);
        }

        if (carpetaInvestigacion == null)
        {
            ReporteServiceNetwork.PostCarpetaInvestigacion(informacion);
        }
        else
        {
            ReporteServiceNetwork.PutCarpetaInvestigacion(informacion, carpetaInvestigacion.Id);
        }
        
        if (amparo == null)
        {
            ReporteServiceNetwork.PostAmparo(informacion);
        }
        else
        {
            ReporteServiceNetwork.PutAmparo(informacion, amparo.Id);
        }

        if (recomendacion == null)
        {
            ReporteServiceNetwork.PostRecomendacionDerechosHumanos(informacion);
        }
        else
        {
            ReporteServiceNetwork.PutRecomendacionDerechosHumanos(informacion, recomendacion.Id);
        }
        
        return true;
    }
}