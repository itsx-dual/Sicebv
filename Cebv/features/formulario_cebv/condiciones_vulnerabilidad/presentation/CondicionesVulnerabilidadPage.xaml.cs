using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public partial class CondicionesVulnerabilidadPage : Page
{
    public CondicionesVulnerabilidadPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        EspecifiqueProcesoRegularizacionMigratoriaTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecifiqueProcesoRegularizacionMigratoriaTb.LostFocus += TextBoxHelper.TrimmedText;
        CaracteristicaPersonalTb.TextChanged += TextBoxHelper.UpperCaseText;
        CaracteristicaPersonalTb.LostFocus += TextBoxHelper.TrimmedText;
        InformacionPersonalAdicionalTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionPersonalAdicionalTb.LostFocus += TextBoxHelper.TrimmedText;
        CaracteristiRiesgoTb.TextChanged += TextBoxHelper.UpperCaseText;
        CaracteristiRiesgoTb.LostFocus += TextBoxHelper.TrimmedText;
        InformacionRelevanteTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionRelevanteTb.LostFocus += TextBoxHelper.TrimmedText;
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