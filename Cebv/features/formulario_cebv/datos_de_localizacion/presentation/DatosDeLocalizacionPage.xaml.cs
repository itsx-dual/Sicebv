using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosDeLocalizacionPage : Page
{
    public DatosDeLocalizacionPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        SintesisLocalizacion.TextChanged += TextBoxHelper.UpperCaseText;
        SintesisLocalizacion.LostFocus += TextBoxHelper.TrimmedText;
        DescribaCondicionPsicofisica.TextChanged += TextBoxHelper.UpperCaseText;
        DescribaCondicionPsicofisica.LostFocus += TextBoxHelper.TrimmedText;
        CircunstanciaFinal1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaFinal1Tb.LostFocus += TextBoxHelper.TrimmedText;
        CircunstanciaFinal2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaFinal2Tb.LostFocus += TextBoxHelper.TrimmedText;
        AreaCodificaInicialTb.TextChanged += TextBoxHelper.UpperCaseText;
        AreaCodificaInicialTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservaionesActualizacionStatusTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservaionesActualizacionStatusTb.LostFocus += TextBoxHelper.TrimmedText;
        DescribaCausasFallecimientoTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescribaCausasFallecimientoTb.LostFocus += TextBoxHelper.TrimmedText;
        SintesisLocalizacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        SintesisLocalizacionTb.LostFocus += TextBoxHelper.TrimmedText;
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