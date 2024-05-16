using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosDeLocalizacionPage : Page
{
    public DatosDeLocalizacionPage()
    {
        InitializeComponent();

        SintesisLocalizacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        SintesisLocalizacionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        HipervinculoSintesisLocalizacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        HipervinculoSintesisLocalizacionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ClasificacionPersonaLocalizadaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ClasificacionPersonaLocalizadaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        AreaCodificaInicialTb.TextChanged += TextBoxHelper.UpperCaseText;
        AreaCodificaInicialTb.LostFocus += TextBoxHelper.TrimmedText;
        
        HipotesisTb.TextChanged += TextBoxHelper.UpperCaseText;
        HipotesisTb.LostFocus += TextBoxHelper.TrimmedText;
        
        StatusTb.TextChanged += TextBoxHelper.UpperCaseText;
        StatusTb.LostFocus += TextBoxHelper.TrimmedText;
        
        CircunstanciaTb.TextChanged += TextBoxHelper.UpperCaseText;
        CircunstanciaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ObservacionesActualizacionStatusTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesActualizacionStatusTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}