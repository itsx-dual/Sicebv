using Cebv.core.util.reporte.viewmodels;

namespace Cebv.features.formulario_cebv.control_ogpi.Data;

public class ControlOgpiDictionary
{
    public static Dictionary<string, object?> GetControlOgpi(Reporte reporte)
    {
        return new Dictionary<string, object?>
        { 
            {"Fecha codificacion", reporte.ControlOgpi?.FechaCodificacion},
            {"Nombre de quien codifico", reporte.ControlOgpi?.NombreCodificador},
            {"Observaciones generales", reporte.ControlOgpi?.Observaciones},
            {"No. Tarjeta OGPI", reporte.ControlOgpi?.NumeroTarjeta},
            {"Folio FUB", reporte.ControlOgpi?.FolioFub},
            {"Autoridad que ingresa FUB", reporte.ControlOgpi?.AutoridadIngresaFub},
            {"Estatus de persona en RNPDNO", reporte.ControlOgpi?.EstatusRndpno}
        };
    }
}