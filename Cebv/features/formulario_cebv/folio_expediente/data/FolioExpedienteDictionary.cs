using Cebv.core.util.enums;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.features.formulario_cebv.folio_expediente.data;

public class FolioExpedienteDictionary
{
    public static Dictionary<string, object?> GetFolioExpediente(Reporte reporte, Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Estado", reporte.Estado?.Nombre},
            {"Municipio", reporte.HechosDesaparicion?.Direccion.Asentamiento?.Municipio?.Nombre},
            {"Numero personas mismo evento", reporte.HechosDesaparicion?.PersonasMismoEvento},
            {"Fecha desaparicion", reporte.HechosDesaparicion?.FechaDesaparicion},
            {"Zona del estado", reporte.ZonaEstado?.Nombre},
            {"Terminación de la entidad (SC, SDF)", reporte.Estado?.AbreviaturaCebv},
            {"Tipo folio", reporte.TipoReporte},
            {"Tipo desaparicion", reporte.TipoDesaparicion},
            {"Area que atendera", reporte.AreaAtiende},
            {"Nombre de quien asigno folio", desaparecido.Folios?.User?.NombreCompleto},
            {"Area a la cambio el expediente", reporte.ExpedienteFisico?.AreaReceptora},
            {"Fecha cambio de area", reporte.ExpedienteFisico?.FechaCambioArea},
            {"Quien solicita el prestamo", reporte.ExpedienteFisico?.SolicitanteExpediente},
            {"Fecha prestamo", reporte.ExpedienteFisico?.FechaPrestamo},
            {"Fecha devolucion", reporte.ExpedienteFisico?.FechaDevolucion},
            {"Observaciones", reporte.ExpedienteFisico?.Observaciones}
        };
    }
    
    public static Validaciones ValidateFolioExpediente(Reporte reporte)
    {
        // Verificar si las propiedades no son null antes de validarlas
        if (reporte is null) return Validaciones.HayInstanciasNulas;
        
        //reporte.Validar("TipoReporte");
        
        var HayErrores = !reporte.HasErrors;
        
        return !HayErrores ? Validaciones.ExistenErrores : Validaciones.NoExisteError;
    }
}