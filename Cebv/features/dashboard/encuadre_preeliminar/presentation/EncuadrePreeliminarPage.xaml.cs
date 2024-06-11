using System.Windows.Controls;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarPage : Page
{
    public EncuadrePreeliminarPage()
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