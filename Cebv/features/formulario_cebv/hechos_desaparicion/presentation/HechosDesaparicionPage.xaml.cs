using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.hechos_desaparicion.presentation;

public partial class HechosDesaparicionPage : Page
{
    public HechosDesaparicionPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        
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