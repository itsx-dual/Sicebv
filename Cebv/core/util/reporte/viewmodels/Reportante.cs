using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class Reportante : ObservableObject
{
    [JsonConstructor]
    public Reportante(int? id,
        int? reporte_id,
        Persona? persona,
        (int? Id, string? Nombre)? parentesco,
        bool? denuncia_anonima,
        bool? informacion_consentimiento,
        bool? informacion_exclusiva_busqueda,
        bool? publicacion_registro_nacional,
        bool? publicacion_boletin,
        bool? pertenencia_colectivo,
        string? nombre_colectivo,
        string? informacion_relevante,
        DateTime? created_at,
        DateTime? updated_at)
    {
        Id = id;
        ReporteId = reporte_id;
        Persona = persona;
        Parentesco = parentesco;
        DenunciaAnonima = denuncia_anonima;
        InformacionConsentimiento = informacion_consentimiento;
        InformacionExclusivaBusqueda = informacion_exclusiva_busqueda;
        PublicacionRegistroNacional = publicacion_registro_nacional;
        PublicacionBoletin = publicacion_boletin;
        PertenenciaColectivo = pertenencia_colectivo;
        NombreColectivo = nombre_colectivo;
        InformacionRelevante = informacion_relevante;
        CreatedAt = created_at;
        UpdatedAt = updated_at;
    }

    [ObservableProperty]
    private int? _id;
    
    [ObservableProperty]
    private int? _reporteId;
    
    [ObservableProperty]
    private Persona? _persona;
    
    [ObservableProperty]
    private (int? Id, string? Nombre)? _parentesco;
    
    [ObservableProperty]
    private bool? _denunciaAnonima;
    
    [ObservableProperty]
    private bool? _informacionConsentimiento;
    
    [ObservableProperty]
    private bool? _informacionExclusivaBusqueda;
    
    [ObservableProperty]
    private bool? _publicacionRegistroNacional;
    
    [ObservableProperty]
    private bool? _publicacionBoletin;
    
    [ObservableProperty]
    private bool? _pertenenciaColectivo;
    
    [ObservableProperty]
    private string? _nombreColectivo;
    
    [ObservableProperty]
    private string? _informacionRelevante;
    
    [ObservableProperty]
    private DateTime? _createdAt;
    
    [ObservableProperty]
    private DateTime? _updatedAt;
    
}