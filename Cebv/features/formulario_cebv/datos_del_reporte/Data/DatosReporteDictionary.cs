using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;

namespace Cebv.features.formulario_cebv.datos_del_reporte.Data;

public class DatosReporteDictionary
{
    public static Dictionary<string, object?> GetDatosReporte(Reporte reporte, DatosReporteViewModel datosReporte, Reportante reportante)
    {
        return new Dictionary<string, object?>
        {
            {"Fecha de creacion", reporte?.FechaCreacion},
            {"Medio conocimineto generico", datosReporte.TipoMedio},
            {"Medio cnocimiento especifico", reporte?.MedioConocimiento},
            {"Estado de donde proviene el reporte", reporte?.Estado},
            {"Institucion origen", reporte?.InstitucionOrigen},
            {"Informacion exclusiva busqueda", reportante.InformacionExclusivaBusqueda},
            {"Publicacion boletin", reportante.PublicacionBoletin}
        };
    }
}