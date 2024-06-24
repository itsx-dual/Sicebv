using System.Collections.ObjectModel;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

/// <summary>
/// Representacion del reporte dado por el enpoint de
/// "/api/reportes"
/// </summary>
public partial class Reporte : ObservableObject
{
    [JsonConstructor]
    public Reporte(
        int id,
        bool? esta_terminado,
        TipoReporte? tipo_reporte,
        MedioConocimiento? medio_conocimiento,
        Estado? estado,
        TipoHipotesis? hipotesis_oficial,
        string? tipo_desaparicion,
        DateTime? fecha_localizacion,
        bool? declaracion_especial_ausencia,
        bool? accion_urgente,
        bool? dictamen,
        bool? ci_nivel_federal,
        string? otro_derecho_humano,
        string? sintesis_localizacion,
        ObservableCollection<Reportante>? reportantes,
        ObservableCollection<Desaparecido>? desaparecidos,
        HechoDesaparicionQueryResponse? hechos_desaparicion,
        DateTime? fecha_creacion,
        DateTime? fecha_actualizacion
    )
    {
        Id = id;
        EstaTerminado = esta_terminado;
        TipoReporte = tipo_reporte;
        MedioConocimiento = medio_conocimiento;
        Estado = estado;
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
    }

    public Reporte() { }

    [ObservableProperty] private int _id;

    [ObservableProperty] private bool? _estaTerminado;

    [ObservableProperty] private TipoReporte? _tipoReporte;

    [ObservableProperty] private MedioConocimiento? _medioConocimiento;

    [ObservableProperty] private Estado? _estado;

    [ObservableProperty] private TipoHipotesis? _hipotesisOficial;

    [ObservableProperty] private string? _tipoDesaparicion;

    [ObservableProperty] private DateTime? _fechaLocalizacion;

    [ObservableProperty] private bool? _declaracionEspecialAusencia;

    [ObservableProperty] private bool? _accionUrgente;

    [ObservableProperty] private bool? _dictamen;

    [ObservableProperty] private bool? _ciNivelFederal;

    [ObservableProperty] private string? _otroDerechoHumano;

    [ObservableProperty] private string? _sintesisLocalizacion;

    [ObservableProperty] private ObservableCollection<Reportante>? _reportantes;

    [ObservableProperty] private ObservableCollection<Desaparecido>? _desaparecidos;
    
    [ObservableProperty] private HechoDesaparicionQueryResponse? _hechosDesaparicion;

    [ObservableProperty] private DateTime? _fechaCreacion;

    [ObservableProperty] private DateTime? _fechaActualizacion;
}