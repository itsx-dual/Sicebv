using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.Data;

public class CondicionesVulnerabilidadDictionary
{
    public static Dictionary<string, object?> GetCondicionesVulneravilidad(Desaparecido desaparecido, CondicionesVulnerabilidadViewModel condicionnes)
    {
        return new Dictionary<string, object?>
        {
            {"Tipo sanguineo", desaparecido.Persona.Salud?.TipoSangre},
            {"Factor Rhesus", desaparecido.Persona.Salud?.FactorRhesus},
            {"Condicion", condicionnes.CondicionSalud},
            {"Indole", condicionnes.IndoleSalud},
            {"Tratamiento", condicionnes.Tratamiento},
            {"Observaciones", condicionnes.Observaciones},
            {"En transito a EUA", desaparecido.Persona.ContextoSocial?.EstaTransitoEstadosUnidos},
            {"Situacion migratoria", desaparecido.Persona.ContextoSocial?.SituacionMigratoria},
            {"Descripcion proceso migratorio", desaparecido.Persona.ContextoSocial?.DescripcionProcesoMigratorio},
            {"Pertenencia grupal o etnica", desaparecido.Persona.EnfoqueDiferenciado?.PertenenciaGrupalEtnica},
            {"Enfoque diferenciado", condicionnes.EnfoqueDiferenciado},
            {"Descripcion vuneralidad", desaparecido.Persona.EnfoqueDiferenciado?.DescripcionVulnerabilidad},
            {"Informacion relevante busqueda", desaparecido.Persona.EnfoqueDiferenciado?.InformacionRelevanteBusqueda},
            {"¿Esta embarazada?", desaparecido.Persona.Embarazo?.EstaEmbarazada},
            {"Meses de embarazo", desaparecido.Persona.Embarazo?.Meses}
        };
    }
}