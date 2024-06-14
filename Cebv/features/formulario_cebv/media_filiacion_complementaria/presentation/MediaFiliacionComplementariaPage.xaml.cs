using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;

public partial class MediaFiliacionComplementariaPage : Page
{
    public MediaFiliacionComplementariaPage()
    {
        InitializeComponent();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        EspecifiqueAsusenciaDentalTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecifiqueAsusenciaDentalTb.LostFocus += TextBoxHelper.TrimmedText;
        EspecifiqueTratamientoDentalTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecifiqueTratamientoDentalTb.LostFocus += TextBoxHelper.TrimmedText;
        CualquierOtraEspecificacionMentonTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualquierOtraEspecificacionMentonTb.LostFocus += TextBoxHelper.TrimmedText;
        EspecificacionDeformacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecificacionDeformacionTb.LostFocus += TextBoxHelper.TrimmedText;
        EspecificacionIntervencionQuirurgicaTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecificacionIntervencionQuirurgicaTb.LostFocus += TextBoxHelper.TrimmedText;
        EspecificacionEnfermedadPielTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecificacionEnfermedadPielTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesGeneralesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesGeneralesTb.LostFocus += TextBoxHelper.TrimmedText;
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