using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoPage : Page
{
    public InstrumentoJuridicoPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        NumeroCarpetaTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroCarpetaTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeRadicaCarpetaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeRadicaCarpetaTb.LostFocus += TextBoxHelper.TrimmedText;
        NombreServidorPublicoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreServidorPublicoTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroAmparoBuscadorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroAmparoBuscadorTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeRadicaAmparoTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeRadicaAmparoTb.LostFocus += TextBoxHelper.TrimmedText;
        NombreServidorPublicoAmparoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreServidorPublicoAmparoTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroRecomendacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroRecomendacionTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeRadicaRecomendacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeRadicaRecomendacionTb.LostFocus += TextBoxHelper.TrimmedText;
        NombreServidorPublicoRecomendacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreServidorPublicoRecomendacionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        OtroTb.TextChanged += TextBoxHelper.UpperCaseText;
        OtroTb.LostFocus += TextBoxHelper.TrimmedText;
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