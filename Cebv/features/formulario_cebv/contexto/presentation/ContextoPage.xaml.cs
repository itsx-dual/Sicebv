using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.contexto.presentation;

public partial class ContextoPage : Page
{
    public ContextoPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        DondeTrabajaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeTrabajaTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeDesseaIrseTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeDesseaIrseTb.LostFocus += TextBoxHelper.TrimmedText;
        PorParteDeQuienTb.TextChanged += TextBoxHelper.UpperCaseText;
        PorParteDeQuienTb.LostFocus += TextBoxHelper.TrimmedText;
        QuienDebeTb.TextChanged += TextBoxHelper.UpperCaseText;
        QuienDebeTb.LostFocus += TextBoxHelper.TrimmedText;
        PasatiemposTb.TextChanged += TextBoxHelper.UpperCaseText;
        PasatiemposTb.LostFocus += TextBoxHelper.TrimmedText;
        ClubesOrganizacionesQuePerteneceTb.TextChanged += TextBoxHelper.UpperCaseText;
        ClubesOrganizacionesQuePerteneceTb.LostFocus += TextBoxHelper.TrimmedText;
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