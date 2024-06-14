using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionPage : Page
{
    public MediaFiliacionPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        OtraExpecificacionOjosTb.TextChanged += TextBoxHelper.UpperCaseText;
        OtraExpecificacionOjosTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionCabelloTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionCabelloTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionCejasTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionCejasTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionBigoteTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionBigoteTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionBarbaTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionBarbaTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionNarizTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionNarizTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionOrejaTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionOrejaTb.LostFocus += TextBoxHelper.TrimmedText;
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