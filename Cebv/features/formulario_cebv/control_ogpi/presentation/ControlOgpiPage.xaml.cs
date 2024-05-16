using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.control_ogpi.presentation;

public partial class ControlOgpiPage : Page
{
    public ControlOgpiPage()
    {
        InitializeComponent();
        
        NombreCapturistaTb.TextChanged += TextBoxHelper.UpperCaseText;
        NombreCapturistaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ObservacionesGeneralesTb.TextChanged += TextBoxHelper.UpperCaseText;
        ObservacionesGeneralesTb.LostFocus += TextBoxHelper.TrimmedText;
        
        NoTarjetaTb.TextChanged += TextBoxHelper.UpperCaseText;
        NoTarjetaTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}