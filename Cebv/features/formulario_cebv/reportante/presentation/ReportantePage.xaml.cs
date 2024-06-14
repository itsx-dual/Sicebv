using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);

        NombreTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };

        ApellidoPaternoTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };

        ApellidoMaternoTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };
    }

    private void TextBoxHelperMethod()
    {
        NombreTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreTb.LostFocus += TextBoxHelper.TrimmedText;
        ApellidoPaternoTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoPaternoTb.LostFocus += TextBoxHelper.TrimmedText;
        ApellidoMaternoTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoMaternoTb.LostFocus += TextBoxHelper.TrimmedText;
        RFCTb.LostFocus += TextBoxHelper.TrimmedText;
        RFCTb.TextChanged += TextBoxHelper.UpperCaseText;
        CurpTb.LostFocus += TextBoxHelper.TrimmedText;
        CurpTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoMovilTb.LostFocus += TextBoxHelper.TrimmedText;
        TelefonoMovilTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
        TelefonoFijoTb.LostFocus += TextBoxHelper.TrimmedText;
        TelefonoFijoTb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones1Tb.LostFocus += TextBoxHelper.TrimmedText;
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
        CodigoPostalTb.TextChanged += TextBoxHelper.UpperCaseText;
        CodigoPostalTb.LostFocus += TextBoxHelper.TrimmedText;
        EntreCalle1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle1Tb.LostFocus += TextBoxHelper.TrimmedText;
        EntreCalle2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle2Tb.LostFocus += TextBoxHelper.TrimmedText;
        TramoCarreteroTb.TextChanged += TextBoxHelper.UpperCaseText;
        TramoCarreteroTb.LostFocus += TextBoxHelper.TrimmedText;
        ReferenciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaTb.LostFocus += TextBoxHelper.TrimmedText;
        InformacionRelevamteTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionRelevamteTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionSituacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionSituacionTb.LostFocus += TextBoxHelper.TrimmedText;
        DeDondeProvieneTb.TextChanged += TextBoxHelper.UpperCaseText;
        DeDondeProvieneTb.LostFocus += TextBoxHelper.TrimmedText;
        CorreoElectronicoTb.TextChanged += TextBoxHelper.UpperCaseText;
        CorreoElectronicoTb.LostFocus += TextBoxHelper.TrimmedText;
    }

    /*private void SubscribeTexBoxesEvents(DependencyObject depObj)
    {
        foreach (TextBox textBox in HelperMethods.FindVisualChildren<TextBox>(depObj))
        {
            textBox.TextChanged += TextBoxHelper.UpperCaseText;
            textBox.LostFocus += TextBoxHelper.TrimmedText;
        }
    }*/
}