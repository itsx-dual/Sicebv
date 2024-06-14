using System.Windows.Controls;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.dashboard.comunicacion_difusion.comunicacion.presentation;

public partial class ComunicacionPage : Page
{
    public ComunicacionPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
    }
    
   /* private void SubscribeTexBoxesEvents(DependencyObject depObj)
    {
        foreach (TextBox textBox in HelperMethods.FindVisualChildren<TextBox>(depObj))
        {
            textBox.TextChanged += TextBoxHelper.UpperCaseText;
            textBox.LostFocus += TextBoxHelper.TrimmedText;
        }
    }*/
}