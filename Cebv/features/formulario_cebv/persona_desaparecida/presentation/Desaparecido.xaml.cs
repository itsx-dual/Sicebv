using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class Desaparecido : Page
{
    public Desaparecido()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<DesaparecidoViewModel>();
        TextBoxHelperMethod();
        //SubscribeTexBoxesEvents(this);
    }

    private void TextBoxHelperMethod()
    {
        NombresTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombresTb.LostFocus += TextBoxHelper.TrimmedText;
        ApellidoPTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoPTb.LostFocus += TextBoxHelper.TrimmedText;
        ApellidoMTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoMTb.LostFocus += TextBoxHelper.TrimmedText;
        IdentidadResguardadaTb.TextChanged += TextBoxHelper.UpperCaseText;
        IdentidadResguardadaTb.LostFocus += TextBoxHelper.TrimmedText;
        DesaparecidoPseudonimoNombreTb.TextChanged += TextBoxHelper.UpperCaseText;
        DesaparecidoPseudonimoNombreTb.LostFocus += TextBoxHelper.TrimmedText;
        DesaparecidoPseudonimoApellidoPTb.TextChanged += TextBoxHelper.UpperCaseText;
        DesaparecidoPseudonimoApellidoPTb.LostFocus += TextBoxHelper.TrimmedText;
        DesaparecidoPseudonimoApellidoMTb.TextChanged += TextBoxHelper.UpperCaseText;
        DesaparecidoPseudonimoApellidoMTb.LostFocus += TextBoxHelper.TrimmedText;
        AliasTb.TextChanged += TextBoxHelper.UpperCaseText;
        AliasTb.LostFocus += TextBoxHelper.TrimmedText;
        FechaNacimientoCEBVTb.TextChanged += TextBoxHelper.UpperCaseText;
        FechaNacimientoCEBVTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesFechaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesFechaTb.LostFocus += TextBoxHelper.TrimmedText;
        RFCTb.TextChanged += TextBoxHelper.UpperCaseText;
        RFCTb.LostFocus += TextBoxHelper.TrimmedText;
        CurpTb.TextChanged += TextBoxHelper.UpperCaseText;
        CurpTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesCurpTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesCurpTb.LostFocus += TextBoxHelper.TrimmedText;
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
        TelefonoMovilTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoMovilTb.LostFocus += TextBoxHelper.TrimmedText;
        ObservacionesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesTb.LostFocus += TextBoxHelper.TrimmedText;
        TelefonoFijoTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoFijoTb.LostFocus += TextBoxHelper.TrimmedText;
        Observaciones1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones1Tb.LostFocus += TextBoxHelper.TrimmedText;
        Observaciones2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones2Tb.LostFocus += TextBoxHelper.TrimmedText;
        Observaciones3Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones3Tb.LostFocus += TextBoxHelper.TrimmedText;
        DetallesOcupacionPrincipalTb.TextChanged += TextBoxHelper.UpperCaseText;
        DetallesOcupacionPrincipalTb.LostFocus += TextBoxHelper.TrimmedText;
        DetallesOcupacionSecundariaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DetallesOcupacionSecundariaTb.LostFocus += TextBoxHelper.TrimmedText;
        NombreParejaConyugeTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreParejaConyugeTb.LostFocus += TextBoxHelper.TrimmedText;
        UsuarioTb.TextChanged += TextBoxHelper.UpperCaseText;
        UsuarioTb.LostFocus += TextBoxHelper.TrimmedText;
        CorreoElectronicoTb.TextChanged += TextBoxHelper.UpperCaseText; 
        CorreoElectronicoTb.LostFocus += TextBoxHelper.TrimmedText;
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