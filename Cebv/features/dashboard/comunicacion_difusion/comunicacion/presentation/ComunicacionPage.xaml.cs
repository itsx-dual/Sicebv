using System.Windows.Controls;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.dashboard.comunicacion_difusion.comunicacion.presentation;

public partial class ComunicacionPage : Page
{
    public ComunicacionPage()
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