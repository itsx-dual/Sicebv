using System.Windows.Controls;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.dashboard.comunicacion_difusion.difusion.presentation;

public partial class DifusionPage : Page
{
    public DifusionPage()
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