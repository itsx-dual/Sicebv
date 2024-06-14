using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

public partial class DesaparicionForzadaPage : Page
{
    public DesaparicionForzadaPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        DescribaSituacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescribaSituacionTb.LostFocus += TextBoxHelper.TrimmedText;
        DescribaSituacion1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        DescribaSituacion1Tb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesMetodoCapturaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesMetodoCapturaTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesDetecionLegalOExorsionTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesDetecionLegalOExorsionTb.LostFocus += TextBoxHelper.TrimmedText;
        DondeTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeTb.LostFocus += TextBoxHelper.TrimmedText;
        NombresApodosTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombresApodosTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionGrupoPerpetradorTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionGrupoPerpetradorTb.LostFocus += TextBoxHelper.TrimmedText;
        EspecifiqueCualesTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecifiqueCualesTb.LostFocus += TextBoxHelper.TrimmedText;
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