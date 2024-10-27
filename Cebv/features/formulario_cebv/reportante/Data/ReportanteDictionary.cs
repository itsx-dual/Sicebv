using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.reportante.presentation;

namespace Cebv.features.formulario_cebv.reportante.Data;

public class ReportanteDictionary
{
    public static Dictionary<string, object?> GetReportante(Reportante reportante, Reporte reporte, ReportanteViewModel reportanteViewModel)
    {
        return new Dictionary<string, object?>
        {
            {"Denuncia anonima", reportante.DenunciaAnonima},
            {"Nombre", reportante.Persona.Nombre},
            {"Apellido paterno", reportante.Persona.ApellidoPaterno},
            {"Apellido materno", reportante.Persona.ApellidoMaterno},
            {"Parentesco con la persona desaparecida", reportante.Parentesco},
            {"Sexo", reportante.Persona.Sexo},
            {"Genero", reportante.Persona.Genero},
            {"Religion", reportante.Persona.Religion},
            {"Lengua", reporte.Reportantes[0].Persona.Lengua},
            {"Fecha nacimiento", reportante.Persona.FechaNacimiento},
            {"Edad aproximada", reportanteViewModel.EdadAproxmida},
            {"Lugar nacimiento", reportante.Persona.LugarNacimiento},
            {"Nacionalidad", reportante.Persona.Nacionalidades},
            {"RFC", reportante.Persona.Rfc},
            {"CURP", reportante.Persona.Curp},
            {"Telefono movil", reportanteViewModel.NoTelefonoMovil},
            {"Observaciones movil", reportanteViewModel.ObservacionesMovil},
            {"Telefono fijo", reportanteViewModel.NoTelefonoFijo},
            {"Observaciones telefono fijo", reportanteViewModel.ObservacionesFijo},
            {"Correo electronico", reportanteViewModel.NombreContacto},
            {"Observaciones correo", reportanteViewModel.ObservacionesContacto},
            {"Calle", reportante.Persona.Direcciones[0].Calle},
            {"No. Exterior", reportante.Persona.Direcciones},
            {"No. Interior", reportante.Persona.Direcciones},
            {"Colonia", reportante.Persona.Direcciones[0].Colonia},
            {"Código postal", reportante.Persona.Direcciones},
            {"Estado", reportanteViewModel.EstadoSelected},
            {"Municipio", reportanteViewModel.MunicipioSelected},
            {"Localidad", reportante.Persona.Direcciones[0].Asentamiento},
            {"Entre calle 1", reportante.Persona.Direcciones[0].Calle1},
            {"Entre calle 2", reportante.Persona.Direcciones[0].Calle2},
            {"Tramo carretero", reportante.Persona.Direcciones[0].TramoCarretero},
            {"Referencia", reportante.Persona.Direcciones[0].Referencia},
            {"Nivel escolaridad", reportante.Persona.Estudios?.Escolaridad},
            {"Estatus escolaridad", reportante.Persona.Estudios?.EstatusEscolaridad},
            {"Estado conyugal", reportante.Persona.ContextoFamiliar?.EstadoConyugal},
            {"Pertenencia grupo de poblacion vulnerable", reportanteViewModel.GrupoVulnerableSelected},
            {"Informacion relevante", reportante.InformacionRelevante},
            {"Búsquedas con anterioridad", reportante.ParticipacionPreviaBusquedas},
            {"En donde", reportante.DescripcionParticipacionBusquedas},
            {"Pertenencia colectiva", reportante.PertenenciaColectivo},
            {"Nombre colectivo", reportante.Colectivo},
            {"Victima extorsión o fraude", reportante.VictimaExtorsionFraude},
            {"Descripcion situacion", reportante.DescripcionExtorsionFraude},
            {"¿Ha sido amenazado?", reportante.RecibioAmenazas},
            {"De donde proviene", reportante.DescripcionOrigenAmenazas},
            {"¿Habla español?", reportante.Persona.HablaEspanhol},
            {"¿Sabe leer y escribir?", reportante.Persona.Estudios?.SabeLeerEscribir},
            {"Nombre pareja conyugal", reportante.Persona.ContextoFamiliar?.NombreParejaConyugue}
        };
    }
}