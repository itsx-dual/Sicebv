using Cebv.features.formulario_cebv.senas_particulares.presentation;

namespace Cebv.features.formulario_cebv.senas_particulares.data;

public class SenasParticularesDictionary
{
    public static Dictionary<string, object?> GetSenaParticular(SenasParticularesViewModel senasParticulares)
    {
        return new Dictionary<string, object?>
        {
            {"Region Cuerpo", senasParticulares.RegionCuerpoSelected},
            {"Vista", senasParticulares.VistaSelected},
            {"Lado", senasParticulares.LadoSelected},
            {"Cantidad", senasParticulares.Cantidad},
            {"Tipo de seña", senasParticulares.TipoSelected},
            {"Descripcion", senasParticulares.Descripcion}
        };
    }
}