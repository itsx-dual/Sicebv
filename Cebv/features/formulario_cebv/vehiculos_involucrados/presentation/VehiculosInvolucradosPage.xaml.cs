using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.presentation;

public partial class VehiculosInvolucradosPage : Page
{
    public VehiculosInvolucradosPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }
    
    private void TextBoxHelperMethod()
    {
        SubMarcaTb.TextChanged += TextBoxHelper.UpperCaseText;
        SubMarcaTb.LostFocus += TextBoxHelper.TrimmedText;
        PlacaTb.TextChanged += TextBoxHelper.UpperCaseText;
        PlacaTb.LostFocus += TextBoxHelper.TrimmedText;
        ModeloTb.TextChanged += TextBoxHelper.UpperCaseText;
        ModeloTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroSerieTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroSerieTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroMotorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroMotorTb.LostFocus += TextBoxHelper.TrimmedText;
        NumeroPermisoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumeroPermisoTb.LostFocus += TextBoxHelper.TrimmedText;
        SeñasParticularesTb.TextChanged += TextBoxHelper.UpperCaseText;
        SeñasParticularesTb.LostFocus += TextBoxHelper.TrimmedText;
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