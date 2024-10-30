using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public class CircunstanciaDesaparicionDictionary
{
    public static Dictionary<string, object?> GetCircunstanciaDesaparicion(Reporte reporte, CircunstanciaDesaparicionViewModel circunstancia,
    Hipotesis? hipotesisPrimaria, Hipotesis? hipotesisSecundaria)
    {
        return new Dictionary<string, object?>
        {
            {"Fecha desaparicion", reporte.HechosDesaparicion?.FechaDesaparicion },
            {"Hora desaparicion", reporte.HechosDesaparicion?.HoraDesaparicion},
            {"Fecha percato", reporte.HechosDesaparicion?.FechaPercato},
            {"Hora percato", reporte.HechosDesaparicion?.HoraPercato},
            {"Aclaracion de la fecha y hpra de los hechos", reporte.HechosDesaparicion?.AclaracionesFechaHechos},
            {"Fecha desaparicion aproximada", reporte.HechosDesaparicion?.FechaDesaparicionCebv},
            {"Fecha de percato aproximada", reporte.HechosDesaparicion?.FechaPercatoCebv},
            {"Tipo domicilio", circunstancia.TiposDomicilio},
            {"Calle", reporte.HechosDesaparicion?.Direccion.Calle},
            {"Numero exterior", reporte.HechosDesaparicion?.Direccion.NumeroExterior},
            {"Numero interior", reporte.HechosDesaparicion?.Direccion.NumeroInterior},
            {"Colonia", reporte.HechosDesaparicion?.Direccion.Colonia},
            {"Codigo postal", reporte.HechosDesaparicion?.Direccion.CodigoPostal},
            {"Estado", circunstancia.EstadoSelected},
            {"Municipio", circunstancia.MunicipioSelected},
            {"Localidad", reporte.HechosDesaparicion?.Direccion.Asentamiento},
            {"Entre calle 1", reporte.HechosDesaparicion?.Direccion.Calle1},
            {"Entre calle 2", reporte.HechosDesaparicion?.Direccion.Calle2},
            {"Tramo carretero", reporte.HechosDesaparicion?.Direccion.TramoCarretero},
            {"Referencia", reporte.HechosDesaparicion?.Direccion.Referencia},
            {"Amenaza - Cambio comportamiento", reporte.HechosDesaparicion?.AmenazaCambioComportamiento},
            {"Descripcion de la situacion de amenaza", reporte.HechosDesaparicion?.DescripcionAmenazaCambioComportamiento},
            {"Contador desapariciones", reporte.HechosDesaparicion?.ContadorDesapariciones},
            {"Situacion previa", reporte.HechosDesaparicion?.SituacionPrevia},
            {"Folios previos", circunstancia.Folios},
            {"Informacion relevante", reporte.HechosDesaparicion?.InformacionRelevante},
            {"Descripcion hechos desaparicion", reporte.HechosDesaparicion?.HechosDesaparicionNarracion},
            {"Sintesis desaparicion", reporte.HechosDesaparicion?.SintesisDesaparicion},
            {"Posible hipotesis desaparicion 1", hipotesisPrimaria?.TipoHipotesis},
            {"Circunstancia inicial 1", hipotesisPrimaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Posible hipotesis desaparicion 2", hipotesisSecundaria?.TipoHipotesis},
            {"Circunstancia inicial 2", hipotesisSecundaria?.TipoHipotesis?.Circunstancia?.Id},
            {"Sitio inicial", reporte.HechosDesaparicion?.Sitio},
            {"Area que codifica", reporte.HechosDesaparicion?.AreaAsignaSitio},
            {"¿Desaparecio acompañado?", reporte.HechosDesaparicion?.DesaparecioAcompanado},
            {"Personas en el mismo evento", reporte.HechosDesaparicion?.PersonasMismoEvento},
            {"Expedientes", reporte.Expedientes.Count}
        };
    }
    
    public static bool ValidateCircunstanciaDesaparicion(Reporte reporte, CircunstanciaDesaparicionViewModel circunstancia)
    {
        circunstancia?.Validate();
        reporte?.HechosDesaparicion?.Validar(); 
        
        return !(circunstancia?.HasErrors ?? true) && 
               !(reporte?.HechosDesaparicion?.HasErrors ?? true);
    }
}