using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.Data;

public class MediaFiliacionComplementariaDictionary
{
    public static Dictionary<string, object?> GetMediaFiliacionComplementaria(Desaparecido desaparecido, 
        MediaFiliacionComplementariaViewModel mediaFiliacionComplementaria)
    {
        return new Dictionary<string, object?>
        {
            {"Ausencia dental", desaparecido.Persona.MediaFiliacionComplementaria?.TieneAusenciaDental},
            {"Especifique ausencia dental", desaparecido.Persona.MediaFiliacionComplementaria?.DescripcionAusenciaDental},
            {"Tratamiento dental", desaparecido.Persona.MediaFiliacionComplementaria?.TieneTratamientoDental},
            {"Especifique tratamiento dental", desaparecido.Persona.MediaFiliacionComplementaria?.DescripcionTratamientoDental},
            {"Tipo menton", desaparecido.Persona.MediaFiliacionComplementaria?.TipoMenton},
            {"Especificacion menton", desaparecido.Persona.MediaFiliacionComplementaria?.EspecificacionesMenton},
            {"Tipo intervencion", mediaFiliacionComplementaria.TipoIntervencion},
            {"Tipo intervencion descripcion", mediaFiliacionComplementaria.TipoIntervencionDescripcion},
            {"Enfermedad piel", mediaFiliacionComplementaria.EnfermedadPiel},
            {"Enfermedad piel descripcion", mediaFiliacionComplementaria.EnfermedadPielDescripcion},
            {"Observaciones", desaparecido.Persona.MediaFiliacionComplementaria?.ObservacionesGenerales}
        };
    }
}