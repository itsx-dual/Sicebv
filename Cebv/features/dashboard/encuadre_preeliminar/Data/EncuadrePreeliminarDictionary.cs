using Cebv.core.util.enums;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.dashboard.encuadre_preeliminar.presentation;

namespace Cebv.features.dashboard.encuadre_preeliminar.Data;

public class EncuadrePreeliminarDictionary
{
    public static Dictionary<string, object?> GetEncuadrePreliminarDictionary(EncuadrePreeliminarViewModel encuadre, Reporte reporte,
        Reportante reportante, Desaparecido desaparecido)
    {
        return new Dictionary<string, object?>
        {
            {"Medio conocimiento generico", encuadre.TipoMedioSelected},
            {"Medio conocimiento especifico", reporte?.MedioConocimiento},
            {"Estado conocimiento", reporte?.Estado},
            {"Desea ser anonimo", reportante?.DenunciaAnonima},
            {"Nombre reportante", reportante?.Persona.Nombre},
            {"Apellido paterno reportante", reportante?.Persona.ApellidoPaterno},
            {"Apellido materno reportante", reportante?.Persona.ApellidoMaterno},
            {"Sexo reportante", reportante?.Persona.Sexo},
            {"Parentesco con la persona desaparecida", reportante?.Parentesco},
            {"Telefono movil reportante", encuadre.NoTelefonoReportante},
            {"Observaciones telefono movil reportante", encuadre.ObservacionesTelefonoReportante},
            {"Nombre desaparecido", desaparecido?.Persona.Nombre },
            {"Apellido paterno desaparecido", desaparecido?.Persona.ApellidoPaterno },
            {"Apellido materno desaparecido", desaparecido?.Persona.ApellidoMaterno },
            {"Sexo desaparecido", desaparecido?.Persona.Sexo },
            {"Nacionalidad desaparecido", desaparecido?.Persona.Nacionalidades},
            {"Fecha nacimiento especifica", desaparecido?.Persona.FechaNacimiento},
            {"Edad Años", encuadre.AnosDesaparecido},
            {"Edad Meses", encuadre.MesesDesaparecido},
            {"Edad Dias", encuadre.DiasDesaparecido},
            {"Fecha aproximada", encuadre.FechaNacimientoDesaparecido},
            {"Fecha nacimiento CEBV", desaparecido?.FechaNacimientoCebv},
            {"observaciones fecha", desaparecido?.ObservacionesFechaNacimiento},
            {"CURP", encuadre.Curp},
            {"Razon por la que no tiene CURP", encuadre.RazonesCurp},
            {"Telefono movil desaparecido", encuadre.NoTelefonoDesaparecido},
            {"Compañia telefono desaparecido", encuadre.CompañiaTelefonicaDesaparecidoSelected},
            {"Observaciones telefono", encuadre.ObservacionesTelefonoDesaparecido},
            {"Fecha desaparicion", encuadre.FechaDesaparicion},
            {"Aclaracion de la fecha y hpra de los hechos", reporte?.HechosDesaparicion?.AclaracionesFechaHechos},
            {"Fecha desaparición aproximada", encuadre.FechaDesaparicion},
            {"Fecha desaparicion CEBV", desaparecido?.FechaNacimientoCebv},
            {"Calle desaparecido", reporte?.HechosDesaparicion?.Direccion.Calle},
            {"Numero exterior desaparecido", reporte?.HechosDesaparicion?.Direccion.NumeroExterior},
            {"Numero interior desaparecido", reporte?.HechosDesaparicion?.Direccion.NumeroInterior},
            {"Colonia desaparecido", reporte?.HechosDesaparicion?.Direccion.Colonia},
            {"Codigo postal desaparecido", reporte?.HechosDesaparicion?.Direccion.CodigoPostal},
            {"Estado desaparecido", encuadre.EstadoSelected},
            {"Municipio desaparecido", encuadre.MunicipioSelected},
            {"Localidad desaparecido", reporte?.HechosDesaparicion?.Direccion.Asentamiento},
            {"Entre calle 1 desaparecido", reporte?.HechosDesaparicion?.Direccion.Calle1},
            {"Entre calle 2 desaparecido", reporte?.HechosDesaparicion?.Direccion.Calle2},
            {"Tramo carretero desaparecido", reporte?.HechosDesaparicion?.Direccion.TramoCarretero},
            {"Referencia desaparecido", reporte?.HechosDesaparicion?.Direccion.Referencia},
            {"Descripcion hechos desaparicion", reporte?.HechosDesaparicion?.HechosDesaparicionNarracion},
            {"Personas en el mismo evento", reporte?.HechosDesaparicion?.PersonasMismoEvento},
            {"Estatura", desaparecido?.Persona.Salud?.EstaturaCentimetros},
            {"Peso", desaparecido?.Persona.Salud?.PesoKilogramos},
            {"Complexion", desaparecido?.Persona.Salud?.Complexion},
            {"Color piel", desaparecido?.Persona.Salud?.ColorPiel},
            {"Color ojos", desaparecido?.Persona.Ojos?.ColorOjos},
            {"Color cabello", desaparecido?.Persona.Cabello?.ColorCabello},
            {"Tamaño cabello", desaparecido?.Persona.Cabello?.TamanoCabello},
            {"Tipo cabello", desaparecido?.Persona.Cabello?.TipoCabello},
            {"Region Cuerpo", encuadre.RegionCuerpoSelected},
            {"Vista", encuadre.VistaSelected},
            {"Lado", encuadre.LadoSelected},
            {"Cantidad", encuadre.Cantidad},
            {"Tipo de seña", encuadre.TipoSelected},
            {"Descripcion", encuadre.Descripcion},
            {"Grupo pertenencia", encuadre.GrupoPerteneciaSelected},
            {"Pertenencia", encuadre.PerteneciaSelected},
            {"Color prenda", encuadre.ColorSelected},
            {"Marca", encuadre.CurrentMarca},
            {"Descripcion prenda", encuadre.CurrentPrendaDescripcion},
        };
    }

    public static Validaciones ValidateEncuadre(EncuadrePreeliminarViewModel encuadre, Reporte reporte,
        Reportante reportante, Desaparecido desaparecido)
    {
        // Verificar si las propiedades no son null antes de validarlas
        if (reportante is null || desaparecido is null || reporte is null) return Validaciones.HayInstanciasNulas;
        
        encuadre.Validate();
        reportante?.Persona?.Validar();
        desaparecido?.Persona?.Validar();
        reporte?.HechosDesaparicion?.Validar();
        reporte?.Validar("MedioConocimiento");
        
        var HayErrores= !reportante.Persona.HasErrors && !desaparecido.Persona.HasErrors && 
                                          !reporte.HechosDesaparicion.HasErrors && !reporte.HasErrors && !encuadre.HasErrors;
        
        return !HayErrores ? Validaciones.ExistenErrores : Validaciones.NoExisteError;
    }
}