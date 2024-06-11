using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.presentation;

public partial class VehiculosInvolucradosPage : Page
{
    public VehiculosInvolucradosPage()
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