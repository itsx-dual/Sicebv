using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class HechosDesaparicion : ObservableValidator
{
    [JsonConstructor]
    public HechosDesaparicion(
        int id,
        int reporteId,
        Direccion direccion,
        Catalogo? sitio,
        Catalogo? areaAsignaSitio,
        bool fechaDesaparicionDesconocida,
        DateTime? fechaDesaparicion,
        string? fechaDesaparicionCebv,
        string? horaDesaparicion,
        DateTime? fechaPercato,
        string? fechaPercatoCebv,
        string? horaPercato,
        string? aclaracionesFechaHechos,
        bool? amenazaCambioComportamiento,
        string? descripcionAmenazaCambioComportamiento,
        int? contadorDesapariciones,
        string? situacionPrevia,
        string? informacionRelevante,
        string? hechosDesaparicion,
        string? sintesisDesaparicion,
        bool? desaparecioAcompanado,
        int? personasMismoEvento,
        DateTime? createdAt,
        DateTime? updatedAt
    )
    {
        Id = id;
        ReporteId = reporteId;
        Direccion = direccion;
        Sitio = sitio;
        AreaAsignaSitio = areaAsignaSitio;
        FechaDesaparicionDesconocida = fechaDesaparicionDesconocida;
        FechaDesaparicion = fechaDesaparicion;
        FechaDesaparicionCebv = fechaDesaparicionCebv;
        HoraDesaparicion = horaDesaparicion;
        FechaPercato = fechaPercato;
        FechaPercatoCebv = fechaPercatoCebv;
        HoraPercato = horaPercato;
        AclaracionesFechaHechos = aclaracionesFechaHechos;
        AmenazaCambioComportamiento = amenazaCambioComportamiento;
        DescripcionAmenazaCambioComportamiento = descripcionAmenazaCambioComportamiento;
        ContadorDesapariciones = contadorDesapariciones;
        SituacionPrevia = situacionPrevia;
        InformacionRelevante = informacionRelevante;
        HechosDesaparicionNarracion = hechosDesaparicion;
        SintesisDesaparicion = sintesisDesaparicion;
        DesaparecioAcompanado = desaparecioAcompanado;
        PersonasMismoEvento = personasMismoEvento;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public HechosDesaparicion()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int _reporteId;

    [ObservableProperty, JsonProperty("direccion")]
    private Direccion _direccion = new();

    [ObservableProperty, JsonProperty("sitio")]
    private Catalogo? _sitio;

    [ObservableProperty, JsonProperty("area_asigna_sitio")]
    private Catalogo? _areaAsignaSitio;

    [ObservableProperty, JsonProperty("fecha_desaparicion_desconocida")]
    private bool _fechaDesaparicionDesconocida;

    [ObservableProperty, JsonProperty("fecha_desaparicion")]
    [CustomValidation(typeof(HechosDesaparicion), nameof(ValidateFechas))]
    private DateTime? _fechaDesaparicion;

    [ObservableProperty, JsonProperty("fecha_desaparicion_cebv")]
    [CustomValidation(typeof(HechosDesaparicion), nameof(ValidateFechas))]
    private string? _fechaDesaparicionCebv;

    [ObservableProperty, JsonProperty("hora_desaparicion")]
    private string? _horaDesaparicion;

    [ObservableProperty, JsonProperty("fecha_percato")]
    private DateTime? _fechaPercato;

    [ObservableProperty, JsonProperty("fecha_percato_cebv")]
    private string? _fechaPercatoCebv;

    [ObservableProperty, JsonProperty("hora_percato")]
    private string? _horaPercato;

    [ObservableProperty, JsonProperty("aclaraciones_fecha_hechos")]
    private string? _aclaracionesFechaHechos;

    [ObservableProperty, JsonProperty("amenaza_cambio_comportamiento")]
    private bool? _amenazaCambioComportamiento = false;

    [ObservableProperty, JsonProperty("descripcion_amenaza_cambio_comportamiento")]
    private string? _descripcionAmenazaCambioComportamiento;

    [ObservableProperty, JsonProperty("contador_desapariciones")]
    private int? _contadorDesapariciones = 0;

    [ObservableProperty, JsonProperty("situacion_previa")]
    private string? _situacionPrevia;

    [ObservableProperty, JsonProperty("informacion_relevante")]
    private string? _informacionRelevante;

    [ObservableProperty, JsonProperty("hechos_desaparicion")]
    [Required(ErrorMessage = "Hechos desaparicion es obligatorio")] [MinLength(2)]
    private string? _hechosDesaparicionNarracion;

    [ObservableProperty, JsonProperty("sintesis_desaparicion")]
    private string? _sintesisDesaparicion;

    [ObservableProperty, JsonProperty("desaparecio_acompanado")]
    private bool? _desaparecioAcompanado = false;

    [ObservableProperty, JsonProperty("personas_mismo_evento")]
    private int? _personasMismoEvento = 1;

    [ObservableProperty, JsonProperty("desaparecidos")]
    private ObservableCollection<Catalogo> _desaparecidos = [];

    [ObservableProperty, JsonProperty("created_at")]
    private DateTime? _createdAt;

    [ObservableProperty, JsonProperty("updated_at")]
    private DateTime? _updatedAt;

    partial void OnContadorDesaparicionesChanged(int? value)
    {
        if (value is < 1 or null) ContadorDesapariciones = 0;
    }


    partial void OnPersonasMismoEventoChanged(int? value)
    {
        if (value is < 1 or null) PersonasMismoEvento = 1;
    }
    
    public static ValidationResult? ValidateFechas(object? value, ValidationContext context)
    {
        var instance = (HechosDesaparicion)context.ObjectInstance;

        if (!instance.FechaDesaparicion.HasValue && string.IsNullOrWhiteSpace(instance.FechaDesaparicionCebv))
        {
            return new ValidationResult("Debes llenar al menos una de las dos fechas (fECHA DESAPARICION O " +
                                        "FECHA DESAPARICION APROXIMADA).");
        }

        return ValidationResult.Success;
    }
    
    public void Validar()
    {
        ValidateAllProperties();
    }
}