using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.contexto.presentation;

public partial class ContextoPage : Page
{
    public ContextoPage()
    {
        InitializeComponent();

        DondeTrabajaTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeTrabajaTb.LostFocus += TextBoxHelper.TrimmedText;
        
        DondeDeseaIrseTb.TextChanged += TextBoxHelper.UpperCaseText;
        DondeDeseaIrseTb.LostFocus += TextBoxHelper.TrimmedText;
        
        QuienDebeTb.TextChanged += TextBoxHelper.UpperCaseText;
        QuienDebeTb.LostFocus += TextBoxHelper.TrimmedText;
        
        PasatiemposTb.TextChanged += TextBoxHelper.UpperCaseText;
        PasatiemposTb.LostFocus += TextBoxHelper.TrimmedText;
        
        ClubesOrganizacionesPerteneceTb.TextChanged += TextBoxHelper.UpperCaseText;
        ClubesOrganizacionesPerteneceTb.LostFocus += TextBoxHelper.TrimmedText;
    }
}