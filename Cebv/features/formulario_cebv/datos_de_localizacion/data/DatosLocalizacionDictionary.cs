using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.data;

public class DatosLocalizacionDictionary
{
    public static Dictionary<string, object?> GetDatosLocalizacion(DatosLocalizacionViewModel datosLocalizacion,
    Desaparecido desaparecido, Hipotesis? hipotesisPrimaria, Hipotesis? hipotesisSecundaria)
    {
        return new Dictionary<string, object?>
        {
            {"¿La persona fue localizada con vida", datosLocalizacion.LocalizadoVivo},
            {"Estatus preliminar", desaparecido.EstatusPreliminar},
            {"Fecha de estatus preliminar", desaparecido.FechaEstatusPreliminar},
            {"Fecha localizacion", desaparecido.Localizacion?.FechaLocalizacion},
            {"Estatus formalizado", desaparecido.EstatusFormalizado},
            {"Fecha de formalizacion del estatus", desaparecido.FechaEstatusFormalizado},
            {"Fecha de actualizacion en sistema", desaparecido.FechaCapturaEstatusFormalizado},
            {"Sintesis localizacion", desaparecido.Localizacion?.SintesisLocalizacion},
            {"Descripcion de la condicion psicofisica de lozalizacion de la persona", desaparecido.Localizacion?.DescripcionCondicionPersona},
            {"Fecha del hallazgo", desaparecido.Localizacion?.FechaHallazgo},
            {"Fecha identificacion del cuerpo", desaparecido.Localizacion?.FechaIdentificacionFamiliar},
            {"Descripcion causas fallecimiento", desaparecido.Localizacion?.DescripcionCausasFallecimiento},
            {"Estado", datosLocalizacion.EstadoSelected},
            {"Municipio", desaparecido.Localizacion?.MunicipioLocalizacion},
            {"Hipotesis localizacion 1", hipotesisPrimaria?.TipoHipotesis},
            {"Circunstancia final 1", hipotesisPrimaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Hipotesis localizacion 2", hipotesisSecundaria?.TipoHipotesis},
            {"Circunstancia final 2", hipotesisSecundaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Sitio final", desaparecido.Localizacion?.Sitio},
            {"Area que codifica", desaparecido.Localizacion?.Area},
            {"Hipotesis DEB", desaparecido.Localizacion?.HipotesisDeb},
            {"Observaciones sobre la actualización de estatus", desaparecido.ObservacionesActualizacionEstatus},
        };
    }
    
    public static bool ValidateDatosLocalizacion(DatosLocalizacionViewModel datosLocalizacion, Desaparecido desaparecido)
    {
        datosLocalizacion?.Validate();
        desaparecido?.Validar();
        desaparecido?.Localizacion?.Validar();
        
        return !(datosLocalizacion?.HasErrors ?? true) && 
               !(desaparecido?.HasErrors ?? true) &&
               !(desaparecido?.Localizacion?.HasErrors ?? true);
    }
}