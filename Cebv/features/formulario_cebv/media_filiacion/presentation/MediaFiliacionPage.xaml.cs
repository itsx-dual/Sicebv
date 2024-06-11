using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionPage : Page
{
    public MediaFiliacionPage()
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