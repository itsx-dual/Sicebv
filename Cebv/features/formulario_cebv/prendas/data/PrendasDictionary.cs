using Cebv.features.formulario_cebv.prendas.presentation;

namespace Cebv.features.formulario_cebv.prendas.data;

public class PrendasDictionary
{
    public static Dictionary<string, object?> GetPrendas(PrendasViewModel prendas)
    {
        return new Dictionary<string, object?>
        {
            {"Grupo pertenencia", prendas.GrupoPertenencia},
            {"Pertenencia", prendas.Pertenencia},
            {"Color", prendas.Color},
            {"Marca", prendas.Marca},
            {"Descripcion", prendas.Descripcion}
        };
    }
}