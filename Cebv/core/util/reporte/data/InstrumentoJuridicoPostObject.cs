namespace Cebv.core.util.reporte.data;

public class InstrumentoJuridicoPostObject
{
    public string NumeroCarpeta { get; set; }
    public string DondeRadicaCarpeta { get; set; }
    public string ServidorPublicoCarpeta { get; set; }
    public DateTime? FechaRecepcionCarpeta { get; set; }
    public string NumeroAmparo { get; set; }
    public string DondeRadicaAmparo { get; set; }
    public string ServidorPublicoAmparo { get; set; }
    public DateTime? FechaRecepcionAmparo { get; set; }
    public string NumeroRecomendacion { get; set; }
    public string DondeRadicaRecomendacion { get; set; }
    public string ServidorPublicoRecomendacion { get; set; }
    public DateTime? FechaRecepcionRecomendacion { get; set; }
    public bool DeclaracionAusencia { get; set; }
    public bool AccionUrgente { get; set; }
    public bool Dictamen { get; set; }
    public bool CarpetaFederal { get; set; }
    public string OtroDerecho { get; set; }
}