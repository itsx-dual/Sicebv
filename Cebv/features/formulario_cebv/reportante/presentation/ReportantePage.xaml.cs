using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();

        NombreTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreTb.LostFocus += TextBoxHelper.TrimmedText;
        
        PrimerApellidoTb.TextChanged += TextBoxHelper.UpperCaseText;
        PrimerApellidoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        SegundoApellidoTb.TextChanged += TextBoxHelper.UpperCaseText;
        SegundoApellidoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        RFCTb.TextChanged += TextBoxHelper.UpperCaseText;
        RFCTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CURPTb.TextChanged += TextBoxHelper.UpperCaseText;
        CURPTb.LostFocus += TextBoxHelper.TrimmedText;
        
        TelefonoMovilTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoMovilTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
        
        TelefonoFijoTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoFijoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ObserVaciones1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        ObserVaciones1Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        Observaciones2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones2Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        CalleTb.TextChanged += TextBoxHelper.UpperCaseText;
        CalleTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NoExteriorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoExteriorTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NoInteriorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoInteriorTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ColoniaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ColoniaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CodigpPostalTb.TextChanged += TextBoxHelper.UpperCaseText;
        CodigpPostalTb.LostFocus += TextBoxHelper.TrimmedText;
        
        EntreCalle1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle1Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        EntreCalle2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle2Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        TramoCarreteroTb.TextChanged += TextBoxHelper.UpperCaseText;
        TramoCarreteroTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ReferenciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}