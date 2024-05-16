using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public partial class CondicionesVulnerabilidadPage : Page
{
    public CondicionesVulnerabilidadPage()
    {
        InitializeComponent();
        
        CaracteristicasVilnerabilidadTb.TextChanged += TextBoxHelper.UpperCaseText;
        CaracteristicasVilnerabilidadTb.LostFocus += TextBoxHelper.TrimmedText;
        
        InformacionPersonalTb.TextChanged += TextBoxHelper.UpperCaseText;
        InformacionPersonalTb.LostFocus += TextBoxHelper.TrimmedText;
        
        MesesEmbarazoTb.TextChanged += TextBoxHelper.UpperCaseText;
        MesesEmbarazoTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}