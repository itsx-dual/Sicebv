using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

[JsonObject(MemberSerialization.OptIn)]
public partial class DocumentoLegal : ObservableObject
{
    [JsonConstructor]
    public DocumentoLegal(
        int? id,
        string? tipo_documento,
        string? numero_documento,
        string? donde_radica,
        string? nombre_servidor_publico,
        DateTime? fecha_recepcion,
        string? desaparecido_id)
    {
        Id = id;
        TipoDocumento = tipo_documento;
        NumeroDocumento = numero_documento;
        DondeRadica = donde_radica;
        NombreServidorPublico = nombre_servidor_publico;
        FechaRecepcion = fecha_recepcion;
        DesaparecidoId = desaparecido_id;
    }

    public DocumentoLegal() { }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true; // Same object reference
        if (ReferenceEquals(obj, null)) return false; // Other object is null
        if (obj.GetType() != GetType()) return false; // Different types
        
        return Equals((DocumentoLegal) obj);
    }

    private bool Equals(DocumentoLegal documento)
    {
        return Id == documento.Id &&
               TipoDocumento == documento.TipoDocumento &&
               NumeroDocumento == documento.NumeroDocumento &&
               DondeRadica == documento.DondeRadica &&
               NombreServidorPublico == documento.NombreServidorPublico &&
               FechaRecepcion == documento.FechaRecepcion &&
               DesaparecidoId == documento.DesaparecidoId;
    }
    
    
    [ObservableProperty, JsonProperty(PropertyName = "id")]
    private int? _id;
    
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
}