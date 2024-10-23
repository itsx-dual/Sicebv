using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.contexto.presentation;

namespace Cebv.features.formulario_cebv.contexto.data;

public class ContextoDictionary
{
    public static Dictionary<string, object?> GetContexto(Reporte reporte, Familiar familiar, Desaparecido desaparecido, 
        ContextoViewModel contexto, Amistad amistad)
    {
        return new Dictionary<string, object?>
        {
            {"Numero de personas con las que vive", reporte.Desaparecidos[0].Persona.ContextoFamiliar?.NumeroPersonasVive },
            {"Nombre pariente", familiar.Nombre},
            {"Parentesco", familiar.Parentesco},
            {"¿Ha ejercido violencia?", familiar.HaEjercidoViolencia},
            {"¿Es un familair cercano?", familiar.EsFamiliarCercano},
            {"Observaciones familiar", familiar.Observaciones},
            {"¿Donde trabaja?", desaparecido.Persona.ContextoEconomico?.DondeTrabaja},
            {"Años laborando", desaparecido.Persona.ContextoEconomico?.AntiguedadTrabajo},
            {"¿Le gusta su trabajo?", desaparecido.Persona.ContextoEconomico?.GustaTrabajo},
            {"¿Ha manifestado intención de trabajar fuera de la ciudad?", desaparecido.Persona.ContextoEconomico?.DeseaTrabajoForaneo},
            {"A donde", desaparecido.Persona.ContextoEconomico?.UbicacionTrabajoForaneo},
            {"Violencia por parte de algun compañero del trabajo", desaparecido.Persona.ContextoEconomico?.ViolenciaLaboral},
            {"Por parte de quien", desaparecido.Persona.ContextoEconomico?.ViolentadorLaboral},
            {"Deudas", desaparecido.Persona.ContextoEconomico?.TieneDeudas},
            {"Monto aproximado deuda", desaparecido.Persona.ContextoEconomico?.MontoDeuda},
            {"A quien le debe", desaparecido.Persona.ContextoEconomico?.DeudaCon},
            {"Pasatiempo", contexto.Pasatiempos},
            {"Amistad - Nombew", amistad.Nombre},
            {"Amistad - Apellido paterno", amistad.ApellidoPaterno},
            {"Amistad - Apellido materno", amistad.ApellidoMaterno},
            {"Amistad - Apodo", amistad.Apodo},
            {"Amistad - Telefono", amistad.Telefono},
            {"Amistad - Tipo red social", amistad.TipoRedSocial},
            {"Amistad - Red social", amistad.UsuarioRedSocial},
            {"Amistad - Observaciones red social", amistad.ObservacionesRedSocial}
        };
    }
}