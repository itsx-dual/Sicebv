using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class Reportante : ObservableObject
{
    [JsonConstructor]
    public Reportante(int? id,
        int? reporte_id,
        Persona? persona,
        Catalogo? parentesco,
        bool denuncia_anonima,
        bool? informacion_consentimiento,
        bool? informacion_exclusiva_busqueda,
        bool? publicacion_registro_nacional,
        bool? publicacion_boletin,
        bool? pertenencia_colectivo,
        string? nombre_colectivo,
        string? informacion_relevante,
        int? edad_estimada,
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
        EdadEstimada = edad_estimada;
        CreatedAt = created_at;
        UpdatedAt = updated_at;
    }

    public Reportante() { }
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;
    
    [ObservableProperty, JsonProperty(PropertyName = "reporte_id")]
    private int? _reporteId;
    
    [ObservableProperty, JsonProperty(PropertyName = "persona")]
    private Persona? _persona = new();
    
    [ObservableProperty, JsonProperty(PropertyName = "parentesco")]
    private Catalogo? _parentesco;
    
    [ObservableProperty, JsonProperty(PropertyName = "denuncia_anonima")]
    private bool _denunciaAnonima;
    
    [ObservableProperty, JsonProperty(PropertyName = "informacion_consentimiento")]
    private bool? _informacionConsentimiento;
    
    [ObservableProperty, JsonProperty(PropertyName = "informacion_exclusiva_busqueda")]
    private bool? _informacionExclusivaBusqueda;
    
    [ObservableProperty, JsonProperty(PropertyName = "publicacion_registro_nacional")]
    private bool? _publicacionRegistroNacional;
    
    [ObservableProperty, JsonProperty(PropertyName = "publicacion_boletin")]
    private bool? _publicacionBoletin;
    
    [ObservableProperty, JsonProperty(PropertyName = "pertenencia_colectivo")]
    private bool? _pertenenciaColectivo;
    
    [ObservableProperty, JsonProperty(PropertyName = "nombre_colectivo")]
    private string? _nombreColectivo;
    
    [ObservableProperty, JsonProperty(PropertyName = "informacion_relevante")]
    private string? _informacionRelevante;
    
    [ObservableProperty, JsonProperty(PropertyName = "edad_estimada")]
    private int? _edadEstimada;
    
    [ObservableProperty, JsonProperty(PropertyName = "created_at")]
    private DateTime? _createdAt;
    
    [ObservableProperty, JsonProperty(PropertyName = "updated_at")]
    private DateTime? _updatedAt;
    
}