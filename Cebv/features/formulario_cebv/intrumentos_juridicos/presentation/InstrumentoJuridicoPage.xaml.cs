using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;

public partial class InstrumentoJuridicoPage : Page
{
    public InstrumentoJuridicoPage()
    {
        InitializeComponent();
        SubscribeTexBoxesEvents(this);
    }
    
    private void SubscribeTexBoxesEvents(DependencyObject depObj)
    {
        foreach (TextBox textBox in HelperMethods.FindVisualChildren<TextBox>(depObj))
        {
            textBox.TextChanged += TextBoxHelper.UpperCaseText;
            textBox.LostFocus += TextBoxHelper.TrimmedText;
        }
    }
}