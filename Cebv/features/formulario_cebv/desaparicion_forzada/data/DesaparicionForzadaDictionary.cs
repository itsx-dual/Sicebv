using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.data;

public class DesaparicionForzadaDictionary
{
    public static Dictionary<string, object?> GetDesaparicionForzada(Reporte reporte, DesaparicionForzadaViewModel desaparicionForzada)
    {
        return new Dictionary<string, object?>
        {
            {"¿Sufrio desaparicion forzada?", reporte.DesaparicionForzada?.DesaparecioAutoridad },
            {"Autoridad", reporte.DesaparicionForzada?.Autoridad},
            {"Describa la situacion", reporte.DesaparicionForzada?.DescripcionAutoridad},
            {"Sufrio desaparicion por particulares", reporte.DesaparicionForzada?.DesaparecioParticular},
            {"Particular", reporte.DesaparicionForzada?.Particular},
            {"Describa la situacion particular", reporte.DesaparicionForzada?.DescripcionParticular},
            {"Método de captura", reporte.DesaparicionForzada?.MetodoCaptura},
            {"Observaciones metodo captura", reporte.DesaparicionForzada?.DescripcionMetodoCaptura},
            {"Medio captura", reporte.DesaparicionForzada?.MedioCaptura},
            {"Observaciones medio captura", reporte.DesaparicionForzada?.DescripcionMedioCaptura},
            {"Detencion previa o extorsion", reporte.DesaparicionForzada?.DetencionPreviaExtorsion},
            {"Observaciones de la detencion", reporte.DesaparicionForzada?.DescripcionDetencionPreviaExtorsion},
            {"¿Ha sido avistado?", reporte.DesaparicionForzada?.HaSidoAvistado},
            {"¿Donde?", reporte.DesaparicionForzada?.DondeHaSidoAvistado},
            {"Nombre(s) / apodo(s)", desaparicionForzada.Nombre},
            {"Sexo", desaparicionForzada.Sexo},
            {"Ultimo status perpetrador", desaparicionForzada.EstatusPerpetrador},
            {"Descripcion perpetrador", desaparicionForzada.PerpetradorDescripcion},
            {"Descripcion grupo perpetrador", reporte.DesaparicionForzada?.DescripcionGrupoPerpetrador},
            {"¿Se suscitaron otros delitos antes o después de la desaparición?", reporte.DesaparicionForzada?.DelitosDesaparicion},
            {"Especifique cuales", reporte.DesaparicionForzada?.DescripcionDelitosDesaparicion}
        };
    }
}