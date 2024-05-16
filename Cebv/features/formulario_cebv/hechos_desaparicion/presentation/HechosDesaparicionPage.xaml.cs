using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.hechos_desaparicion.presentation;

public partial class HechosDesaparicionPage : Page
{
    public HechosDesaparicionPage()
    {
        InitializeComponent();

        ReporteTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReporteTb.LostFocus += TextBoxHelper.TrimmedText;
        
        AclaracionFechaHoraDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        AclaracionFechaHoraDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CambioComportamientoTb.TextChanged += TextBoxHelper.UpperCaseText;
        CambioComportamientoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        DescripcionAmenazaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionAmenazaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ContadorDesaparicionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ContadorDesaparicionesTb.LostFocus += TextBoxHelper.TrimmedText;
        
        SituacionPreviaTb.TextChanged += TextBoxHelper.UpperCaseText;
        SituacionPreviaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        HechosDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        HechosDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        SintesisDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText; 
        SintesisDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        InformacionRelevanteTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionRelevanteTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CircunstanciaInicial2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaInicial2Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        AreaCodificaInicialTb.TextChanged += TextBoxHelper.UpperCaseText;
        AreaCodificaInicialTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NumPersonasMismoEventoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumPersonasMismoEventoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CircunstanciaDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ReferenciaPersonasTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaPersonasTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ExpedientesDirectosTb.TextChanged += TextBoxHelper.UpperCaseText;
        ExpedientesDirectosTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ExpedientesIndirectosTb.TextChanged += TextBoxHelper.UpperCaseText;
        ExpedientesIndirectosTb.LostFocus += TextBoxHelper.TrimmedText;
        
        HipervinculoFacebookBoletinTb.TextChanged += TextBoxHelper.UpperCaseText;
        HipervinculoFacebookBoletinTb.LostFocus += TextBoxHelper.TrimmedText;
        
        HipervinvuloCarpetaExpedienteDriveTb.TextChanged += TextBoxHelper.UpperCaseText;
        HipervinvuloCarpetaExpedienteDriveTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CualesTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualesTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}