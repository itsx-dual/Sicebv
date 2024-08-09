using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class DesaparicionForzada : ObservableObject
{
    [JsonConstructor]
    public DesaparicionForzada(
        int? id,
        int? reporteId,
        bool? desaparecioAutoridad,
        Catalogo? autoridad,
        string? descripcionAutoridad,
        bool? desaparecioParticular,
        Catalogo? particular,
        string? descripcionParticular,
        Catalogo? metodoCaptura,
        string? descripcionMetodoCaptura,
        Catalogo? medioCaptura,
        string? descripcionMedioCaptura,
        bool? detencionPreviaExtorsion,
        string? descripcionDetencionPreviaExtorsion,
        bool? haSidoAvistado,
        string? dondeHaSidoAvistado,
        bool? delitosDesaparicion,
        string? descripcionDelitosDesaparicion,
        string? descripcionGrupoPerpetrador
    )
    {
        Id = id;
        ReporteId = reporteId;
        DesaparecioAutoridad = desaparecioAutoridad;
        Autoridad = autoridad;
        DescripcionAutoridad = descripcionAutoridad;
        DesaparecioParticular = desaparecioParticular;
        Particular = particular;
        DescripcionParticular = descripcionParticular;
        MetodoCaptura = metodoCaptura;
        DescripcionMetodoCaptura = descripcionMetodoCaptura;
        MedioCaptura = medioCaptura;
        DescripcionMedioCaptura = descripcionMedioCaptura;
        DetencionPreviaExtorsion = detencionPreviaExtorsion;
        DescripcionDetencionPreviaExtorsion = descripcionDetencionPreviaExtorsion;
        HaSidoAvistado = haSidoAvistado;
        DondeHaSidoAvistado = dondeHaSidoAvistado;
        DelitosDesaparicion = delitosDesaparicion;
        DescripcionDelitosDesaparicion = descripcionDelitosDesaparicion;
        DescripcionGrupoPerpetrador = descripcionGrupoPerpetrador;
    }

    public DesaparicionForzada()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("desaparecio_autoridad")]
    private bool? _desaparecioAutoridad;

    [ObservableProperty, JsonProperty("autoridad")]
    private Catalogo? _autoridad;

    [ObservableProperty, JsonProperty("descripcion_autoridad")]
    private string? _descripcionAutoridad;

    [ObservableProperty, JsonProperty("desaparecio_particular")]
    private bool? _desaparecioParticular;

    [ObservableProperty, JsonProperty("particular")]
    private Catalogo? _particular;

    [ObservableProperty, JsonProperty("descripcion_particular")]
    private string? _descripcionParticular;

    [ObservableProperty, JsonProperty("metodo_captura")]
    private Catalogo? _metodoCaptura;

    [ObservableProperty, JsonProperty("descripcion_metodo_captura")]
    private string? _descripcionMetodoCaptura;

    [ObservableProperty, JsonProperty("medio_captura")]
    private Catalogo? _medioCaptura;

    [ObservableProperty, JsonProperty("descripcion_medio_captura")]
    private string? _descripcionMedioCaptura;

    [ObservableProperty, JsonProperty("detencion_previa_extorsion")]
    private bool? _detencionPreviaExtorsion;

    [ObservableProperty, JsonProperty("descripcion_detencion_previa_extorsion")]
    private string? _descripcionDetencionPreviaExtorsion;

    [ObservableProperty, JsonProperty("ha_sido_avistado")]
    private bool? _haSidoAvistado;

    [ObservableProperty, JsonProperty("donde_ha_sido_avistado")]
    private string? _dondeHaSidoAvistado;

    [ObservableProperty, JsonProperty("delitos_desaparicion")]
    private bool? _delitosDesaparicion;

    [ObservableProperty, JsonProperty("descripcion_delitos_desaparicion")]
    private string? _descripcionDelitosDesaparicion;
    
    [ObservableProperty, JsonProperty("descripcion_grupo_perpetrador")]
    private string? _descripcionGrupoPerpetrador;
}