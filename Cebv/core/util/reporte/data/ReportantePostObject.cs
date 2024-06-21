namespace Cebv.core.util.reporte.data;

public class ReportantePostObject
{
     public PersonaPostObject? Persona { get; set; }
     public int? Parentesco { get; set; }
     public bool? DenunciaAnonima { get; set; }
     public bool? InformacionConsentimiento { get; set; } 
     public bool? InformacionExclusivaBusqueda { get; set; }
     public bool? PublicacionRegistroNacional { get; set; }
     public bool? PublicacionBoletin { get; set; }
     public bool? PertenenciaColectivo { get; set; }
     public string? NombreColectivo { get; set; }
     public string? InformacionRelevante { get; set; }
}