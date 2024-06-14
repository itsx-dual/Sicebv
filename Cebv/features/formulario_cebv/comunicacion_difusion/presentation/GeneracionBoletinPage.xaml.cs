using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.comunicacion_difusion.presentation;

public partial class GeneracionBoletinPage : Page
{
    public GeneracionBoletinPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        EjemploSeñaParticular1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular1Tb.LostFocus += TextBoxHelper.TrimmedText;
        EjemploSeñaParticular2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular2Tb.LostFocus += TextBoxHelper.TrimmedText;
        EjemploSeñaParticular3Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular3Tb.LostFocus += TextBoxHelper.TrimmedText;
        EjemploSeñaParticular4Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular4Tb.LostFocus += TextBoxHelper.TrimmedText;
        EjemploSeñaParticular5Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular5Tb.LostFocus += TextBoxHelper.TrimmedText;
        EjemploSeñaParticular6Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EjemploSeñaParticular6Tb.LostFocus += TextBoxHelper.TrimmedText;
        SeñasparticularesBoletinTb.TextChanged += TextBoxHelper.UpperCaseText;
        SeñasparticularesBoletinTb.LostFocus += TextBoxHelper.TrimmedText;
        InformacionAdicionalTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionAdicionalTb.LostFocus += TextBoxHelper.TrimmedText;
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