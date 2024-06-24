using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.core.util.reporte.viewmodels;

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
    
    [ObservableProperty]
    private int? _id;
    
    [ObservableProperty]
    private string? _tipoDocumento;
    
    [ObservableProperty]
    private string? _numeroDocumento;
    
    [ObservableProperty]
    private string? _dondeRadica;
    
    [ObservableProperty]
    private string? _nombreServidorPublico;
    
    [ObservableProperty]
    private DateTime? _fechaRecepcion;
    
    [ObservableProperty]
    private string? _desaparecidoId;
}