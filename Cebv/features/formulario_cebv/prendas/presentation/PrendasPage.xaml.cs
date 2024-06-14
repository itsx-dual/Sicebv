using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendasPage : Page
{
    public PrendasPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        MarcaTb.TextChanged += TextBoxHelper.UpperCaseText;
        MarcaTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionTb.LostFocus += TextBoxHelper.TrimmedText;
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