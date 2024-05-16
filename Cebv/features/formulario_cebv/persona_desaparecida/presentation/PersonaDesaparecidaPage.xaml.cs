using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaPage : Page
{
    public PersonaDesaparecidaPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<PersonaDesaparecidaViewModel>();

        NombreTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreTb.TextChanged += TextBoxHelper.TrimmedText;
        
        ApellidoPaternoTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoPaternoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        ApellidoMaternoTb.TextChanged += TextBoxHelper.UpperCaseText;
        ApellidoMaternoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        IdentidadResguardadaTb.TextChanged += TextBoxHelper.UpperCaseText;
        IdentidadResguardadaTb.TextChanged += TextBoxHelper.TrimmedText;
        
        PseudonimoNombreTb.TextChanged += TextBoxHelper.UpperCaseText;
        PseudonimoNombreTb.TextChanged += TextBoxHelper.TrimmedText;
        
        PseudonimoPrimerApellidoTb.TextChanged += TextBoxHelper.UpperCaseText;
        PseudonimoPrimerApellidoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        PseudonimoSegundoApellidoTb.TextChanged += TextBoxHelper.UpperCaseText;
        PseudonimoSegundoApellidoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        AliasTb.TextChanged += TextBoxHelper.UpperCaseText;
        AliasTb.TextChanged += TextBoxHelper.TrimmedText;
        
        FechaNacimientoAproximadaTb.TextChanged += TextBoxHelper.UpperCaseText;
        FechaNacimientoAproximadaTb.TextChanged += TextBoxHelper.TrimmedText;
        
        EdadDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        EdadDesaparicionTb.TextChanged += TextBoxHelper.TrimmedText;
        
        EdadActualTb.TextChanged += TextBoxHelper.UpperCaseText;
        EdadActualTb.TextChanged += TextBoxHelper.TrimmedText;
        
        OtroProcesoRegulatorioMigracionTb.TextChanged += TextBoxHelper.UpperCaseText;
        OtroProcesoRegulatorioMigracionTb.TextChanged += TextBoxHelper.TrimmedText;
        
        RFCTb.TextChanged += TextBoxHelper.UpperCaseText;
        RFCTb.TextChanged += TextBoxHelper.TrimmedText;
        
        CURPTb.TextChanged += TextBoxHelper.UpperCaseText;
        CURPTb.TextChanged += TextBoxHelper.TrimmedText;
        
        ObservacionesCurpTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesCurpTb.TextChanged += TextBoxHelper.TrimmedText;
        
        CalleTb.TextChanged += TextBoxHelper.UpperCaseText;
        CalleTb.TextChanged += TextBoxHelper.TrimmedText;
        
        NoExteriorTb.TextChanged += TextBoxHelper.UpperCaseText; 
        NoExteriorTb.TextChanged += TextBoxHelper.TrimmedText;
        
        NoInteriorTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoInteriorTb.TextChanged += TextBoxHelper.TrimmedText;
        
        ColoniaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ColoniaTb.TextChanged += TextBoxHelper.TrimmedText;
        
        CodigoPostalTb.TextChanged += TextBoxHelper.UpperCaseText;
        CodigoPostalTb.TextChanged += TextBoxHelper.TrimmedText;
        
        EntreCalle1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle1Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        EntreCalle2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        EntreCalle2Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        TramoCarreteroTb.TextChanged += TextBoxHelper.UpperCaseText;
        TramoCarreteroTb.TextChanged += TextBoxHelper.TrimmedText;
        
        ReferenciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ReferenciaTb.TextChanged += TextBoxHelper.TrimmedText;
        
        TelefonoMovilTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoMovilTb.TextChanged += TextBoxHelper.TrimmedText;
        
        Observaciones0Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones0Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        TelefonoFijoTb.TextChanged += TextBoxHelper.UpperCaseText;
        TelefonoFijoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        Observaciones1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones1Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        Observaciones2Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observaciones2Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        Observsciones3Tb.TextChanged += TextBoxHelper.UpperCaseText;
        Observsciones3Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        DetallesOcupacion0Tb.TextChanged += TextBoxHelper.UpperCaseText;
        DetallesOcupacion0Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        DetallesOcupacion1Tb.TextChanged += TextBoxHelper.UpperCaseText;
        DetallesOcupacion1Tb.TextChanged += TextBoxHelper.TrimmedText;
        
        NombreParejaTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreParejaTb.TextChanged += TextBoxHelper.TrimmedText;
    }
}