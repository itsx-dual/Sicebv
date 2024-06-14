using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciasDesaparicionPage : Page
{
    public CircunstanciasDesaparicionPage()
    {
        InitializeComponent();
        //TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }
    
    private void TextBoxHelperMethod()
    {
        HoraDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText; 
        HoraDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        HoraPercatoTb.TextChanged += TextBoxHelper.UpperCaseText;
        HoraPercatoTb.LostFocus += TextBoxHelper.TrimmedText;
        AclaracionFechaHoraHechosTb.TextChanged += TextBoxHelper.UpperCaseText;
        AclaracionFechaHoraHechosTb.LostFocus += TextBoxHelper.TrimmedText;
        CalleTb.TextChanged += TextBoxHelper.UpperCaseText;
        CalleTb.LostFocus += TextBoxHelper.TrimmedText;
        NoExteriorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoExteriorTb.LostFocus += TextBoxHelper.TrimmedText;
        NoInteriorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoInteriorTb.LostFocus += TextBoxHelper.TrimmedText;
        ColoniaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ColoniaTb.LostFocus += TextBoxHelper.TrimmedText;
        CodigoPostalTb.TextChanged += TextBoxHelper.UpperCaseText;
        CodigoPostalTb.LostFocus += TextBoxHelper.TrimmedText;
        EntreCalle1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle1Tb.LostFocus += TextBoxHelper.TrimmedText;
        EntreCalle2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle2Tb.LostFocus += TextBoxHelper.TrimmedText;
        TramoCarreteroTb.TextChanged += TextBoxHelper.UpperCaseText;
        TramoCarreteroTb.LostFocus += TextBoxHelper.TrimmedText;
        ReferenciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionSituacionAmenazaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionSituacionAmenazaTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionSituacionPreviaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionSituacionPreviaTb.LostFocus += TextBoxHelper.TrimmedText;
        FoliosPreviosTb.TextChanged += TextBoxHelper.UpperCaseText;
        FoliosPreviosTb.LostFocus += TextBoxHelper.TrimmedText;
        DatoPersonaRelacionadaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DatoPersonaRelacionadaTb.LostFocus += TextBoxHelper.TrimmedText;
        DescripcionHechosDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionHechosDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        SintesisHechosDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        SintesisHechosDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        CircunstanciaInicial1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaInicial1Tb.LostFocus += TextBoxHelper.TrimmedText;
        CircunstanciaInicial2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaInicial2Tb.LostFocus += TextBoxHelper.TrimmedText;
        AreaCodificaSitioInicialTb.TextChanged += TextBoxHelper.UpperCaseText;
        AreaCodificaSitioInicialTb.LostFocus += TextBoxHelper.TrimmedText;
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