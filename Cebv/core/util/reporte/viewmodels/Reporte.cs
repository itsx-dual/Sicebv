using System.Collections.ObjectModel;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.folio_expediente.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

/// <summary>
/// Representacion del reporte dado por el enpoint de
/// "/api/reportes"
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public partial class Reporte : ObservableObject
{
    [JsonConstructor]
    public Reporte(
        int id,
        bool? esta_terminado,
        Catalogo? tipo_reporte,
        MedioConocimiento? medio_conocimiento,
        Estado? estado,
        Catalogo? zona_estado,
        TipoHipotesis? hipotesis_oficial,
        string? tipo_desaparicion,
        string? institucion_origen,
        DateTime? fecha_localizacion,
        bool? declaracion_especial_ausencia,
        bool? accion_urgente,
        bool? dictamen,
        bool? ci_nivel_federal,
        string? otro_derecho_humano,
        string? sintesis_localizacion,
        ObservableCollection<Reportante>? reportantes,
        ObservableCollection<Desaparecido>? desaparecidos,
        HechosDesaparicionResponse? hechos_desaparicion,
        DateTime? fecha_creacion,
        DateTime? fecha_actualizacion
    )
    {
        Id = id;
        EstaTerminado = esta_terminado;
        TipoReporte = tipo_reporte;
        MedioConocimiento = medio_conocimiento;
        Estado = estado;
        ZonaEstado = zona_estado;
        HipotesisOficial = hipotesis_oficial;
        TipoDesaparicion = tipo_desaparicion;
        FechaLocalizacion = fecha_localizacion;
        DeclaracionEspecialAusencia = declaracion_especial_ausencia;
        AccionUrgente = accion_urgente;
        Dictamen = dictamen;
        CiNivelFederal = ci_nivel_federal;
        OtroDerechoHumano = otro_derecho_humano;
        SintesisLocalizacion = sintesis_localizacion;
        Reportantes = reportantes;
        Desaparecidos = desaparecidos;
        HechosDesaparicion = hechos_desaparicion;
        FechaCreacion = fecha_creacion;
        FechaActualizacion = fecha_actualizacion;
        InstitucionOrigen = institucion_origen;
    }

    public Reporte()
    {
        Id = -1;
    }

    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int _id;

    [ObservableProperty, JsonProperty(PropertyName = "esta_terminado")]
    private bool? _estaTerminado;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_reporte")]
    private Catalogo? _tipoReporte;

    [ObservableProperty, JsonProperty(PropertyName = "medio_conocimiento")]
    private MedioConocimiento? _medioConocimiento;

    [ObservableProperty, JsonProperty(PropertyName = "estado")]
    private Estado? _estado;
    
    [ObservableProperty, JsonProperty(PropertyName = "zona_estado")]
    private Catalogo? _zonaEstado;

    [ObservableProperty, JsonProperty(PropertyName = "hipotesis_oficial")]
    private TipoHipotesis? _hipotesisOficial;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_desaparicion")]
    private string? _tipoDesaparicion;

    [ObservableProperty, JsonProperty(PropertyName = "fecha_localizacion")]
    private DateTime? _fechaLocalizacion;

    [ObservableProperty, JsonProperty(PropertyName = "declaracion_especial_ausencia")]
    private bool? _declaracionEspecialAusencia;

    [ObservableProperty, JsonProperty(PropertyName = "accion_urgente")]
    private bool? _accionUrgente;

    [ObservableProperty, JsonProperty(PropertyName = "dictamen")]
    private bool? _dictamen;

    [ObservableProperty, JsonProperty(PropertyName = "ci_nivel_federal")]
    private bool? _ciNivelFederal;

    [ObservableProperty, JsonProperty(PropertyName = "otro_derecho_humano")]
    private string? _otroDerechoHumano;

    [ObservableProperty, JsonProperty(PropertyName = "sintesis_localizacion")]
    private string? _sintesisLocalizacion;

    [ObservableProperty, JsonProperty(PropertyName = "institucion_origen")]
    private string? _institucionOrigen;

    [ObservableProperty, JsonProperty(PropertyName = "reportantes")]
    private ObservableCollection<Reportante> _reportantes = [];

    [ObservableProperty, JsonProperty(PropertyName = "desaparecidos")]
    private ObservableCollection<Desaparecido> _desaparecidos = [];

    [ObservableProperty, JsonProperty(PropertyName = "hechos_desaparicion")]
    private HechosDesaparicionResponse? _hechosDesaparicion = new();

    [ObservableProperty, JsonProperty(PropertyName = "hipotesis")]
    private ObservableCollection<Hipotesis>? _hipotesis = new();

    [ObservableProperty, JsonProperty(PropertyName = "fecha_creacion")]
    private DateTime? _fechaCreacion;

    [ObservableProperty, JsonProperty(PropertyName = "fecha_actualizacion")]
    private DateTime? _fechaActualizacion;

    [ObservableProperty, JsonProperty("control_ogpi")]
    private ControlOgpi? _controlOgpi;

    [ObservableProperty, JsonProperty("folios")]
    private ObservableCollection<FolioPretty>? _folios = new();
}