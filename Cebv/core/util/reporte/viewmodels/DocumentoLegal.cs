using static Cebv.core.util.enums.TipoDocumentoLegal;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class DocumentoLegal : ObservableObject
{
    [JsonConstructor]
    public DocumentoLegal(
        int? id,
        bool? esOficial,
        string? tipoDocumento,
        string? numeroDocumento,
        string? dondeRadica,
        string? nombreServidorPublico,
        DateTime? fechaRecepcion,
        string? desaparecidoId)
    {
        Id = id;
        EsOficial = esOficial;
        TipoDocumento = tipoDocumento;
        NumeroDocumento = numeroDocumento;
        DondeRadica = dondeRadica;
        NombreServidorPublico = nombreServidorPublico;
        FechaRecepcion = fechaRecepcion;
        DesaparecidoId = desaparecidoId;
    }

    public DocumentoLegal()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types

        return Equals((DocumentoLegal)obj);
    }

    private bool Equals(DocumentoLegal documento)
    {
        return Id == documento.Id &&
               EsOficial == documento.EsOficial &&
               TipoDocumento == documento.TipoDocumento &&
               NumeroDocumento == documento.NumeroDocumento &&
               DondeRadica == documento.DondeRadica &&
               NombreServidorPublico == documento.NombreServidorPublico &&
               FechaRecepcion == documento.FechaRecepcion &&
               DesaparecidoId == documento.DesaparecidoId;
    }


    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;

    [ObservableProperty, JsonProperty("es_oficial")]
    private bool? _esOficial;

    [ObservableProperty, JsonProperty(PropertyName = "tipo_documento")]
    private string? _tipoDocumento;

    [ObservableProperty, JsonProperty(PropertyName = "numero_documento")]
    private string? _numeroDocumento;

    [ObservableProperty, JsonProperty(PropertyName = "donde_radica")]
    private string? _dondeRadica;

    [ObservableProperty, JsonProperty(PropertyName = "nombre_servidor_publico")]
    private string? _nombreServidorPublico;

    [ObservableProperty, JsonProperty(PropertyName = "fecha_recepcion")]
    private DateTime? _fechaRecepcion;

    [ObservableProperty, JsonProperty(PropertyName = "desaparecido_id")]
    private string? _desaparecidoId;

    // --------------------------------------------------------------
    // Parametros por defecto de los diferentes tipos de documentos
    // --------------------------------------------------------------

    public readonly Dictionary<string, object> ParametrosCarpeta = new()
    {
        { nameof(TipoDocumento), CarpetaInvestigacion },
        { nameof(EsOficial), false }
    };

    public readonly Dictionary<string, object> ParametrosAmparo = new()
    {
        { nameof(TipoDocumento), AmparoBuscador },
        { nameof(EsOficial), false }
    };

    public readonly Dictionary<string, object> ParametrosRecomendacion = new()
    {
        { nameof(TipoDocumento), RecomendacionDerechos },
        { nameof(EsOficial), false }
    };
}