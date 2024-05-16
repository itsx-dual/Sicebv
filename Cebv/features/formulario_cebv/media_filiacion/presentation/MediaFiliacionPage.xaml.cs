using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionPage : Page
{
    public MediaFiliacionPage()
    {
        InitializeComponent();

        EstaturaTb.TextChanged += TextBoxHelper.UpperCaseText;
        EstaturaTb.TextChanged += TextBoxHelper.TrimmedText;
        
        PesoTb.TextChanged += TextBoxHelper.UpperCaseText;
        PesoTb.TextChanged += TextBoxHelper.TrimmedText;
        
        EspecificacionOjosTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecificacionOjosTb.TextChanged += TextBoxHelper.TrimmedText;
        
        EspecificacionCabelloTb.TextChanged += TextBoxHelper.UpperCaseText;
        EspecificacionCabelloTb.TextChanged += TextBoxHelper.TrimmedText;
    }
}