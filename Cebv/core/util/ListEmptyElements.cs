using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

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
            {"Nombre servidor(a) público (CI, IM FGE)", carpetaInvestigacion?.NombreServidorPublico},
            {"Fecha de recepción de CI, IM o reporte FGE", carpetaInvestigacion?.FechaRecepcion},
            {"Especifica si hay o existe amparo buscador", amparoBuscador?.EsOficial},
            {"Número de amparo buscador", amparoBuscador?.NumeroDocumento},
            {"Dónde radica el amparo buscador", amparoBuscador?.DondeRadica},
            {"Nombre del juez", amparoBuscador?.NombreServidorPublico},
            {"Fecha recepción", amparoBuscador?.FechaRecepcion},
            {"Especifica si hay o existe recomendación de derechos humanos", recomedacionDerechos?.EsOficial},
            {"Número de recomendación derechos humanos", recomedacionDerechos?.NumeroDocumento},
            {"Dónde radica la recomendación derechos humanos", recomedacionDerechos?.DondeRadica},
            {"Nombre servidor(a) público (Derechos humanos)", recomedacionDerechos?.NombreServidorPublico},
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
    
    // Método que recibe una colección de ObservableValidator y obtiene los mensajes de error
    public static string GetAllValidationMessages(IEnumerable<ObservableValidator> validators)
    {
        return string.Join(Environment.NewLine, 
            validators
                .Where(v => v != null) 
                .SelectMany(v => v.GetErrors().Select(e => e.ErrorMessage)));
    }
}