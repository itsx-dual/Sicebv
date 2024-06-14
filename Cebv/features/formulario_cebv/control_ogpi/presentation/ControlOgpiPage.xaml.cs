using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Cebv.features.formulario_cebv.control_ogpi.presentation;

public partial class ControlOgpiPage : Page
{
    public ControlOgpiPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        NombreQuienCodificoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreQuienCodificoTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesGeneralesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesGeneralesTb.LostFocus += TextBoxHelper.TrimmedText;
        NoTarjetaOGPITb.TextChanged += TextBoxHelper.UpperCaseText;
        NoTarjetaOGPITb.LostFocus += TextBoxHelper.TrimmedText;
        FolioFubTb.TextChanged += TextBoxHelper.UpperCaseText;
        FolioFubTb.LostFocus += TextBoxHelper.TrimmedText;
        AutoridadIngresoFubTb.TextChanged += TextBoxHelper.UpperCaseText;
        AutoridadIngresoFubTb.LostFocus += TextBoxHelper.TrimmedText;
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