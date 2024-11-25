using Cebv.core.modules.persona.data;
using Cebv.core.util.enums;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;

namespace Cebv.features.formulario_cebv.persona_desaparecida.data;

public class PersonaDesaparecidaDictionary
{
    public static Dictionary<string, object?> GetDesaparecido(Desaparecido desaparecido,
        DesaparecidoViewModel desaparecidoViewModel, Direccion? direccion, Pseudonimo? pseudonimo, OcupacionPersona? ocupacionPrincipal,
        OcupacionPersona? ocupacionSecundaria)
    {
        return new Dictionary<string, object?>
        {
            {"Nombre", desaparecido.Persona.Nombre },
            {"Apellido paterno", desaparecido.Persona.ApellidoPaterno },
            {"Apellido materno", desaparecido.Persona.ApellidoMaterno },
            {"Identidad resguardada", desaparecido.IdentidadResguardada },
            {"Pseudónimo(s) nombre(s)", pseudonimo?.Nombre },
            {"Pseudónimo(s) primer apellido", pseudonimo?.ApellidoPaterno },
            {"Pseudónimo(s) segundo apellido", pseudonimo?.ApellidoMaterno },
            {"Apodo", desaparecido.Persona.Apodo },
            {"Sexo", desaparecido.Persona.Sexo },
            {"Genero", desaparecido.Persona.Genero },
            {"Fecha aproximada", desaparecidoViewModel.FechaAproximada },
            {"Fecha nacimiento", desaparecido.Persona.FechaNacimiento },
            {"Edad Años", desaparecidoViewModel.EdadAnos },
            {"Edad Meses", desaparecidoViewModel.EdadMeses },
            {"Edad Dias", desaparecidoViewModel.EdadDias },
            {"Lugar de nacimiento", desaparecido.Persona.LugarNacimiento },
            {"Nacionalidad", desaparecido.Persona.Nacionalidades},
            {"RFC", desaparecido.Persona.Rfc },
            {"CURP", desaparecido.Persona.Curp },
            {"Observaciones CURP", desaparecido.Persona.ObservacionesCurp },
            {"Mismo domicilio que la persona reportante", desaparecidoViewModel.EsMismoDomicilioReportante },
            {"Calle", direccion?.Calle },
            {"Numero exterior", direccion?.NumeroExterior },
            {"Numero interior", direccion?.NumeroInterior },
            {"Colonia", direccion?.Colonia },
            {"Codigo postal", direccion?.CodigoPostal },
            {"Estado", desaparecidoViewModel.EstadoSelected },
            {"Municipio", desaparecidoViewModel.MunicipioSelected },
            {"Localidad", direccion?.Asentamiento },
            {"Entre calle 1", direccion?.Calle1 },
            {"Entre calle 2", direccion?.Calle2 },
            {"Tramo carretero", direccion?.TramoCarretero },
            {"Referencia", direccion?.Referencia },
            {"Telefono movil", desaparecidoViewModel.NoTelefonoMovil },
            {"Compañia", desaparecidoViewModel.CompaniaTelefonicaSelected },
            {"Observaciones movil", desaparecidoViewModel.ObservacionesMovil },
            {"Telefono fijo", desaparecidoViewModel.NoTelefonoFijo},
            {"Observaciones telefono fijo", desaparecidoViewModel.ObservacionesFijo},
            {"Correo electronico", desaparecidoViewModel.UsuarioCorreo},
            {"Observaciones correo", desaparecidoViewModel.ObservacionesCorreo},
            {"Tipo red social", desaparecidoViewModel.TipoRedSocialSelected},
            {"Usuario", desaparecidoViewModel.UsuarioRedSocial},
            {"Observaciones red social", desaparecidoViewModel.ObservacionesRedSocial},
            {"Descripcion ocupacion principal", ocupacionPrincipal?.Observaciones},
            {"Codigo general principal", desaparecidoViewModel.TipoOcupacionPrincipal},
            {"Codigo especifico principal", ocupacionPrincipal?.Ocupacion},
            {"Descripcion ocupacion secundaria", ocupacionSecundaria?.Observaciones},
            {"Codigo general secundaria", desaparecidoViewModel.TipoOcupacionSecundaria},
            {"Codigo especifico secundaria", ocupacionSecundaria?.Ocupacion},
            {"¿Habla español?", desaparecido.Persona.HablaEspanhol },
            {"¿Sabe leer y escribir?", desaparecido.Persona.Estudios?.SabeLeerEscribir },
            {"Religion", desaparecido.Persona.Religion },
            {"Escolaridad", desaparecido.Persona.Estudios?.Escolaridad },
            {"Avance escolaridad", desaparecido.Persona.Estudios?.EstatusEscolaridad },
            {"Otras especificaciones ocupación", desaparecido.Persona.ContextoEconomico?.OtrasEspecificacionesOcupacion },
            {"Estado conyugal", desaparecido.Persona.ContextoFamiliar?.EstadoConyugal },
            {"Estado civil", desaparecido.Persona.ContextoFamiliar?.NombreParejaConyugue }
        };
    }
    
    public static Validaciones ValidateDesaparecido(DesaparecidoViewModel desaparecidoViewModel, Desaparecido desaparecido)
    {
        if (desaparecido is null) return Validaciones.HayInstanciasNulas;
        
        desaparecidoViewModel?.Validate();
        //desaparecido?.Persona?.Validar();
        
        var HayErrores = !desaparecidoViewModel.HasErrors && !desaparecido.Persona.HasErrors;
        
        return !HayErrores ? Validaciones.ExistenErrores : Validaciones.NoExisteError;
    }
}