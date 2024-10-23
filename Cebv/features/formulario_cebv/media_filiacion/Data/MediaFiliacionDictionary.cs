using Cebv.core.util.reporte.viewmodels;

namespace Cebv.features.formulario_cebv.media_filiacion.Data;

public class MediaFiliacionDictionary
{
    public static Dictionary<string, object?> GetMediaFiliacion(Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Estatura", desaparecido.Persona.Salud?.EstaturaCentimetros},
            {"Peso", desaparecido.Persona.Salud?.PesoKilogramos},
            {"Complexion", desaparecido.Persona.Salud?.Complexion},
            {"Color piel", desaparecido.Persona.Salud?.ColorPiel},
            {"Forma cara", desaparecido.Persona.Salud?.FormaCara},
            {"Color ojos", desaparecido.Persona.Ojos?.ColorOjos},
            {"Forma ojos", desaparecido.Persona.Ojos?.FormaOjos},
            {"Tamaño ojos", desaparecido.Persona.Ojos?.TamanoOjos},
            {"Otra especificaciónojos", desaparecido.Persona.Ojos?.EspecificacionesOjos},
            {"Calvicie", desaparecido.Persona.Cabello?.Calvicie},
            {"Color cabello", desaparecido.Persona.Cabello?.ColorCabello},
            {"Tamaño cabello", desaparecido.Persona.Cabello?.TamanoCabello},
            {"Tipo cabello", desaparecido.Persona.Cabello?.TipoCabello},
            {"Cualquier otras especificacion cabello", desaparecido.Persona.Cabello?.EspecificacionesCabello},
            {"Cejas", desaparecido.Persona.VelloFacial?.Cejas},
            {"Cualquier otrsa especificacion cejas", desaparecido.Persona.VelloFacial?.EspecificacionesCejas},
            {"Bigote", desaparecido.Persona.VelloFacial?.TieneBigote},
            {"Cualquier otra especificacion bigote", desaparecido.Persona.VelloFacial?.EspescificacionesBigote},
            {"Barba", desaparecido.Persona.VelloFacial?.TieneBarba},
            {"Cualquier otra especificacion barba", desaparecido.Persona.VelloFacial?.EspecificacionesBarba},
            {"Forma nariz", desaparecido.Persona.Nariz?.TipoNariz},
            {"Cualquier otra especificacion nariz", desaparecido.Persona.Nariz?.EspesificacionesNariz},
            {"Tamaño boca", desaparecido.Persona.Boca?.TamanoBoca},
            {"Tamaño labios", desaparecido.Persona.Boca?.TamanoLabios},
            {"Cualquir otra especificacion boca", desaparecido.Persona.Boca?.EspecificacionesBoca},
            {"Tamaño orejas", desaparecido.Persona.Orejas?.TamanoOrejas},
            {"Forma orejas", desaparecido.Persona.Orejas?.FormaOrejas},
            {"Cualquier otra especificacion orejas", desaparecido.Persona.Orejas?.EspesificacionesOrejas}
        };
    }
}