using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;

public partial class MediaFiliacionComplementariaPage : Page
{
    public MediaFiliacionComplementariaPage()
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