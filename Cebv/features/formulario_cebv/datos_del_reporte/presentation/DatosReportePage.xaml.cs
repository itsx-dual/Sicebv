using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReportePage : Page
{
    public DatosReportePage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        FechaInicioTb.TextChanged += TextBoxHelper.UpperCaseText;
        FechaInicioTb.LostFocus += TextBoxHelper.TrimmedText;
        HoraInicioTb.TextChanged += TextBoxHelper.UpperCaseText;
        HoraInicioTb.LostFocus += TextBoxHelper.TrimmedText;
        InstitucionOrigenTb.TextChanged += TextBoxHelper.UpperCaseText;
        InstitucionOrigenTb.LostFocus += TextBoxHelper.TrimmedText;
        
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