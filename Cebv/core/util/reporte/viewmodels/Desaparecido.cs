using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

public partial class Desaparecido : ObservableObject
{
    [JsonConstructor]
    public Desaparecido(
        int id,
        string? reporte_id,
        Persona? persona,
        EstatusPersona? estatus_rpdno,
        EstatusPersona? estatus_cebv,
        ObservableCollection<DocumentoLegal>? documentos_legales,
        string? clasificacion_persona,
        bool? habla_espanhol,
        bool? sabe_leer,
        bool? sabe_escribir,
        string? url_boletin,
        bool? declaracion_especial_ausencia,
        bool? accion_urgente,
        bool? dictamen,
        bool? ci_nivel_federal,
        string? otro_derecho_humano,
        string? folio_cebv,
        DateTime? created_at,
        DateTime? updated_at)
    {
        Id = id;
        ReporteId = reporte_id;
        Persona = persona;
        EstatusRpdno = estatus_rpdno;
        EstatusCebv = estatus_cebv;
        DocumentosLegales = documentos_legales;
        ClasificacionPersona = clasificacion_persona;
        HablaEspanhol = habla_espanhol;
        SabeLeer = sabe_leer;
        SabeEscribir = sabe_escribir;
        UrlBoletin = url_boletin;
        DeclaracionEspecialAusencia = declaracion_especial_ausencia;
        AccionUrgente = accion_urgente;
        Dictamen = dictamen;
        CiNivelFederal = ci_nivel_federal;
        OtroDerechoHumano = otro_derecho_humano;
        FolioCebv = folio_cebv;
        CreatedAt = created_at;
        UpdatedAt = updated_at;
    }

    [ObservableProperty] private int _id;

    [ObservableProperty] private string? _reporteId;

    [ObservableProperty] private Persona? _persona;

    [ObservableProperty] private EstatusPersona? _estatusRpdno;

    [ObservableProperty] private EstatusPersona? _estatusCebv;

    [ObservableProperty] private ObservableCollection<DocumentoLegal>? _documentosLegales;

    [ObservableProperty] private string? _clasificacionPersona;

    [ObservableProperty] private bool? _hablaEspanhol;

    [ObservableProperty] private bool? _sabeLeer;

    [ObservableProperty] private bool? _sabeEscribir;

    [ObservableProperty] private string? _urlBoletin;

    [ObservableProperty] private bool? _declaracionEspecialAusencia;

    [ObservableProperty] private bool? _accionUrgente;

    [ObservableProperty] private bool? _dictamen;

    [ObservableProperty] private bool? _ciNivelFederal;

    [ObservableProperty] private string? _otroDerechoHumano;

    [ObservableProperty] private string? _folioCebv;

    [ObservableProperty] private DateTime? _createdAt;

    [ObservableProperty] private DateTime? _updatedAt;
}