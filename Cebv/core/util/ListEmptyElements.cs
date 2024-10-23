using Cebv.core.modules.persona.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;
using Cebv.features.formulario_cebv.contexto.data;
using Cebv.features.formulario_cebv.contexto.presentation;
using Cebv.features.formulario_cebv.datos_complementarios.presentation;
using Cebv.features.formulario_cebv.datos_de_localizacion.presentation;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;
using Cebv.features.formulario_cebv.desaparicion_forzada.presentation;
using Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;
using Cebv.features.formulario_cebv.prendas.presentation;
using Cebv.features.formulario_cebv.reportante.presentation;
using Cebv.features.formulario_cebv.senas_particulares.presentation;
using Cebv.features.formulario_cebv.vehiculos_involucrados.data;

namespace Cebv.core.util;

public class ListEmptyElements
{
    public static Dictionary<string, object?> GetInstrumentoJuridico(DocumentoLegal? carpetaInvestigacion, DocumentoLegal? amparoBuscador, 
        DocumentoLegal? recomedacionDerechos, Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Especifica si hay o existe carpeta de investigación (CI).", carpetaInvestigacion?.EsOficial},
            {"Número de CI, IM o reporte FGE", carpetaInvestigacion?.NumeroDocumento},
            {"Dónde radica la CI, IM o reporte FGE", carpetaInvestigacion?.DondeRadica},
            {"Nombre servidor(a) público", carpetaInvestigacion?.NombreServidorPublico},
            {"Fecha de recepción de CI, IM o reporte FGE", carpetaInvestigacion?.FechaRecepcion},
            {"Especifica si hay o existe amparo buscador", amparoBuscador?.EsOficial},
            {"Número de amparo buscador", amparoBuscador?.NumeroDocumento},
            {"Dónde radica el amparo buscador", amparoBuscador?.DondeRadica},
            {"Nombre del juez", amparoBuscador?.NombreServidorPublico},
            {"Fecha recepción", amparoBuscador?.FechaRecepcion},
            {"Especifica si hay o existe recomendación de derechos humanos", recomedacionDerechos?.EsOficial},
            {"Número de recomendación derechos humanos", recomedacionDerechos?.NumeroDocumento},
            {"Dónde radica la recomendación derechos humanos", recomedacionDerechos?.DondeRadica},
            {"Nombre servidor(a) público", recomedacionDerechos?.NombreServidorPublico},
            {"Fecha recepción de recomendación DH", recomedacionDerechos?.FechaRecepcion},
            {"Declaracion especial ausencia", desaparecido.DeclaracionEspecialAusencia},
            {"Accion urgente", desaparecido.AccionUrgente},
            {"Dictamen", desaparecido.Dictamen},
            {"Carpeta de investigacion nivel federal", desaparecido.CiNivelFederal},
            {"Otro derecho humano", desaparecido.OtroDerechoHumano}
        };
    }
    
    public static List<string> GetEmptyElements(Dictionary<string, object?> propertyDictionary)
    {
        return propertyDictionary
            .Where(kv => kv.Value is null || string.IsNullOrWhiteSpace(kv.Value.ToString()) || (kv.Value is int intValue && intValue == 0))
            .Select(kv => kv.Key)
            .ToList();
    }
}