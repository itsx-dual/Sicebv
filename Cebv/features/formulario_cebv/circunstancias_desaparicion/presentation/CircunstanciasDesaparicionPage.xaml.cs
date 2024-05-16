using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciasDesaparicionPage : Page
{
    public CircunstanciasDesaparicionPage()
    {
        InitializeComponent();
        
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
        
        LocalidadTb.TextChanged += TextBoxHelper.UpperCaseText;
        LocalidadTb.LostFocus += TextBoxHelper.TrimmedText;
        
        EntreCalleTb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalleTb.LostFocus += TextBoxHelper.TrimmedText;
        
        YCalleTb.TextChanged += TextBoxHelper.UpperCaseText;
        YCalleTb.LostFocus += TextBoxHelper.TrimmedText;
        
        TramoCarreteroTb.TextChanged += TextBoxHelper.UpperCaseText;
        TramoCarreteroTb.LostFocus += TextBoxHelper.TrimmedText;
        
        LongitudTb.TextChanged += TextBoxHelper.UpperCaseText;
        LongitudTb.LostFocus += TextBoxHelper.TrimmedText;
        
        LatitudTb.TextChanged += TextBoxHelper.UpperCaseText;
        LatitudTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ReferenciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        DescripcionSituacionAmenazasTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionSituacionAmenazasTb.LostFocus += TextBoxHelper.TrimmedText;
        
        DescripcionSituacionPreviaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DescripcionSituacionPreviaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CircunstanciaInicial1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaInicial1Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        CircunstanciaInicial2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaInicial2Tb.LostFocus += TextBoxHelper.TrimmedText;
        
        AreaCodificaInicialTb.TextChanged += TextBoxHelper.UpperCaseText;
        AreaCodificaInicialTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NumPersMismoEventoTb.TextChanged += TextBoxHelper.UpperCaseText;
        NumPersMismoEventoTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ExpedientesIndirectosTb.TextChanged += TextBoxHelper.UpperCaseText;
        ExpedientesIndirectosTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CualesTb.TextChanged += TextBoxHelper.UpperCaseText;
        CualesTb.LostFocus += TextBoxHelper.TrimmedText;
        
        OtraInformacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        OtraInformacionTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}