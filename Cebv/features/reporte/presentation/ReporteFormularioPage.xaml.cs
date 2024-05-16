using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.reporte.presentation;

public partial class ReporteFormularioPage : Page
{
    public ReporteFormularioPage()
    {
        InitializeComponent();

        FechsDesaparicionTb.TextChanged += TextBoxHelper.UpperCaseText;
        FechsDesaparicionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        SintesisLocalizacionTb.TextChanged += TextBoxHelper.UpperCaseText;
        SintesisLocalizacionTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ClasificacionPersonaTb.TextChanged += TextBoxHelper.UpperCaseText;
        ClasificacionPersonaTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}