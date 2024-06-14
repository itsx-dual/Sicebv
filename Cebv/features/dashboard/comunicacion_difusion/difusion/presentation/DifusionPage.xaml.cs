using System.Windows.Controls;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.dashboard.comunicacion_difusion.difusion.presentation;

public partial class DifusionPage : Page
{
    public DifusionPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        NombreColectivoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreColectivoTb.LostFocus += TextBoxHelper.TrimmedText;
        IdentificadorDifusionTb.TextChanged += TextBoxHelper.UpperCaseText;
        IdentificadorDifusionTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
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