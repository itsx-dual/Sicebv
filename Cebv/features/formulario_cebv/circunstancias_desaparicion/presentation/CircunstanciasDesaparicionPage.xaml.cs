using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciasDesaparicionPage : Page
{
    public CircunstanciasDesaparicionPage()
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