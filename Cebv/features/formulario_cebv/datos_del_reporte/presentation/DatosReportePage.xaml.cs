using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.datos_del_reporte.presentation;

public partial class DatosReportePage : Page
{
    public DatosReportePage()
    {
        InitializeComponent();
        
        DependenciaOrigenTb.TextChanged += TextBoxHelper.UpperCaseText;
        DependenciaOrigenTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}